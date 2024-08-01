using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using back.zone.monads.iomonad;
using back.zone.net.http.configuration.jwt;
using back.zone.net.http.models.jwt;
using back.zone.net.http.utils.collections;
using Microsoft.IdentityModel.Tokens;
using monads.iomonad;

namespace back.zone.net.http.services.jwt;

public sealed class JwtService
{
    private readonly JwtConfiguration _configuration;

    public JwtService(JwtConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IO<Credentials> GenerateCredentials(string username, string password)
    {
        return io.of(() =>
        {
            using var hmacSha = new HMACSHA512();
            var salt = hmacSha.Key;
            var hash = hmacSha.ComputeHash(Encoding.UTF8.GetBytes(password));

            return new Credentials(username, salt, hash);
        });
    }

    public IO<ImmutableArray<Claim>> GenerateClaims(SessionHolder sessionHolder)
    {
        return io.of(() =>
        {
            var rolesBuilder = ImmutableArray.CreateBuilder<Claim>();
            var metasBuilder = ImmutableArray.CreateBuilder<Claim>();

            foreach (var role in sessionHolder.Roles)
                rolesBuilder.Add(new Claim(ClaimTypes.Role, role));

            metasBuilder.AddRange(
                new Claim(ClaimTypes.NameIdentifier, sessionHolder.UserId),
                new Claim("session_id", sessionHolder.SessionId)
            );

            return Collections.Flatten(metasBuilder.ToImmutable(), rolesBuilder.ToImmutable());
        });
    }

    public IO<JwtSession> GenerateJwtSession(string sessionId, ImmutableArray<Claim> claims)
    {
        return GenerateAccessToken(claims).zipWith(GenerateRefreshToken(), BuildJwtSession);

        JwtSession BuildJwtSession(string accessToken, string refreshToken)
        {
            return new JwtSession(sessionId, accessToken, refreshToken);
        }
    }


    public IO<string> GenerateAccessToken(ImmutableArray<Claim> claims)
    {
        return
            io
                .of(() => TimeSpan.FromMilliseconds(_configuration.AccessTokenLifetime))
                .map(timeSpan =>
                {
                    var symmetricSecurityKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.SecretKey));
                    var signingCredentials =
                        new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512Signature);
                    var jwtSecurityToken = new JwtSecurityToken(
                        claims: claims,
                        expires: DateTime.UtcNow.Add(timeSpan),
                        signingCredentials: signingCredentials,
                        audience: _configuration.Audience,
                        issuer: _configuration.Issuer,
                        notBefore: DateTime.UtcNow
                    );
                    var jwtTokenString = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

                    return jwtTokenString;
                });
    }

    public IO<string> GenerateRefreshToken()
    {
        return
            io
                .of(() => TimeSpan.FromMilliseconds(_configuration.RefreshTokenLifetime))
                .map(timeSpan =>
                {
                    var symmetricSecurityKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.SecretKey));
                    var signingCredentials =
                        new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512Signature);
                    var jwtSecurityToken = new JwtSecurityToken(
                        expires: DateTime.UtcNow.Add(timeSpan),
                        signingCredentials: signingCredentials,
                        audience: _configuration.Audience,
                        issuer: _configuration.Issuer,
                        notBefore: DateTime.UtcNow
                    );
                    var jwtTokenString = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

                    return jwtTokenString;
                });
    }

    public IO<Credentials> CheckPassword(Credentials credentials, string password)
    {
        return io.of(() =>
        {
            using var hmacSha = new HMACSHA512(credentials.Salt);
            var computedHash = hmacSha.ComputeHash(Encoding.UTF8.GetBytes(password));

            return computedHash.SequenceEqual(credentials.Hash)
                ? credentials
                : throw new Exception("#invalid_credentials");
        });
    }
}