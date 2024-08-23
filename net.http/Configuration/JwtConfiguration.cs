using back.zone.core.Monads.OptionMonad;

namespace back.zone.net.http.Configuration;

public sealed record JwtConfiguration(
    string SecretKey,
    Option<string> Audience,
    Option<string> Issuer,
    long AccessTokenLifetime,
    long RefreshTokenLifetime
);