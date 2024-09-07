using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using back.zone.core.Monads.OptionMonad;
using back.zone.core.Monads.TryMonad;
using back.zone.net.http.Configuration;
using back.zone.net.http.Models.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace back.zone.net.http.Services.Jwt;

/// <summary>
///     A service for generating and validating JWT tokens.
/// </summary>
public sealed class JwtService
{
    private static readonly JwtSecurityTokenHandler JwtSecurityTokenHandler = new();
    private readonly JwtConfiguration _configuration;
    private readonly byte[] _encodedSecretKey;

    public JwtService(JwtConfiguration configuration)
    {
        _configuration = configuration;
        _encodedSecretKey = Encoding.UTF8.GetBytes(_configuration.SecretKey);
    }

    /// <summary>
    ///     Generates a new set of credentials for a user.
    /// </summary>
    /// <param name="username">The username of the user.</param>
    /// <param name="password">The password of the user.</param>
    /// <returns>An IO monad containing the generated credentials.</returns>
    public static Try<Credentials> GenerateCredentials(string username, string password)
    {
        return Try.Succeed((username, password)).Map(Build);

        static Credentials Build((string username, string password) credentials)
        {
            using var hmacSha = new HMACSHA512(Encoding.UTF8.GetBytes(credentials.password));
            var salt = hmacSha.Key;
            var hash = hmacSha.ComputeHash(Encoding.UTF8.GetBytes(credentials.password));

            return new Credentials(credentials.username, salt, hash);
        }
    }

    /// <summary>
    ///     Generates a set of claims based on the provided session holder.
    /// </summary>
    /// <param name="sessionHolder">The session holder containing user information.</param>
    /// <returns>An IO monad containing the generated claims.</returns>
    public static Try<ImmutableArray<Claim>> GenerateClaims(SessionHolder sessionHolder)
    {
        return Try.Succeed(sessionHolder).Map(BuildClaims);

        static ImmutableArray<Claim> BuildClaims(SessionHolder sessionHolder)
        {
            var claims = ImmutableArray.CreateBuilder<Claim>();

            foreach (var role in sessionHolder.Roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            claims.AddRange(
                new Claim(ClaimTypes.NameIdentifier, sessionHolder.UserId),
                new Claim("session_id", sessionHolder.SessionId)
            );

            claims.Capacity = claims.Count;

            return claims.MoveToImmutable();
        }
    }

    /// <summary>
    ///     Generates a set of JWT tokens (access token and refresh token) based on the provided claims.
    /// </summary>
    /// <param name="claims">The claims to be included in the generated tokens.</param>
    /// <returns>An IO monad containing the generated JWT tokens.</returns>
    public Try<JwtTokens> GenerateJwtTokens(ImmutableArray<Claim> claims)
    {
        return GenerateAccessToken(claims).ZipWith(GenerateRefreshToken(), BuildJwtTokens);

        static JwtTokens BuildJwtTokens(string accessToken, string refreshToken)
        {
            return new JwtTokens(accessToken, refreshToken);
        }
    }

    /// <summary>
    ///     Generates an access token based on the provided claims.
    /// </summary>
    /// <param name="claims">The claims to be included in the access token.</param>
    /// <returns>An IO monad containing the generated access token.</returns>
    private Try<string> GenerateAccessToken(ImmutableArray<Claim> claims)
    {
        return Try
            .Succeed(claims)
            .Zip(Try.Succeed(TimeSpan.FromMilliseconds(_configuration.AccessTokenLifetime)))
            .ZipWith(Try.Succeed((_configuration, _encodedSecretKey)), BuildAccessToken);

        static string BuildAccessToken(
            (ImmutableArray<Claim> claims, TimeSpan timeSpan) parameters,
            (JwtConfiguration configuration, byte[] encodedSecretKey) instanceParameters
        )
        {
            var symmetricSecurityKey =
                new SymmetricSecurityKey(instanceParameters.encodedSecretKey);
            var signingCredentials =
                new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512Signature);
            var jwtSecurityToken = new JwtSecurityToken(
                claims: parameters.claims,
                expires: DateTime.UtcNow.Add(parameters.timeSpan),
                signingCredentials: signingCredentials,
                audience: instanceParameters.configuration.Audience.GetOrElse(string.Empty),
                issuer: instanceParameters.configuration.Issuer.GetOrElse(string.Empty),
                notBefore: DateTime.UtcNow
            );
            var jwtTokenString = JwtSecurityTokenHandler.WriteToken(jwtSecurityToken);

            return jwtTokenString;
        }
    }

    /// <summary>
    ///     Generates a refresh token for JWT authentication.
    /// </summary>
    /// <returns>An IO monad containing the generated refresh token.</returns>
    private Try<string> GenerateRefreshToken()
    {
        return Try
            .Succeed(TimeSpan.FromMilliseconds(_configuration.RefreshTokenLifetime))
            .ZipWith(Try.Succeed((_configuration, _encodedSecretKey)), BuildRefreshToken);

        static string BuildRefreshToken(
            TimeSpan timeSpan,
            (JwtConfiguration configuration, byte[] encodedSecretKey) instanceParameters
        )
        {
            var symmetricSecurityKey =
                new SymmetricSecurityKey(instanceParameters.encodedSecretKey);
            var signingCredentials =
                new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512Signature);
            var jwtSecurityToken = new JwtSecurityToken(
                expires: DateTime.UtcNow.Add(timeSpan),
                signingCredentials: signingCredentials,
                audience: instanceParameters.configuration.Audience.GetOrElse(string.Empty),
                issuer: instanceParameters.configuration.Issuer.GetOrElse(string.Empty),
                notBefore: DateTime.UtcNow
            );
            var jwtTokenString = JwtSecurityTokenHandler.WriteToken(jwtSecurityToken);

            return jwtTokenString;
        }
    }

    /// <summary>
    ///     Checks the validity of a provided password against the stored credentials.
    /// </summary>
    /// <param name="credentials">The stored credentials to be checked.</param>
    /// <param name="password">The password to be validated.</param>
    /// <returns>
    ///     An IO monad containing the validated credentials if the password is correct.
    ///     Throws an <see cref="InvalidDataException" /> with the message "#invalid_credentials" if the password is incorrect.
    /// </returns>
    public static Try<Credentials> CheckPassword(Credentials credentials, string password)
    {
        return Try.Succeed((credentials, password)).Map(SequenceEqual);

        static Credentials SequenceEqual((Credentials creds, string password) parameters)
        {
            using var hmacSha = new HMACSHA512(parameters.creds.Salt);
            var computedHash = hmacSha.ComputeHash(Encoding.UTF8.GetBytes(parameters.password));

            return computedHash.SequenceEqual(parameters.creds.Hash)
                ? parameters.creds
                : throw new InvalidDataException("#invalid_credentials");
        }
    }
}