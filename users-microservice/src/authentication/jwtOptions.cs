using Microsoft.Extensions.Options;

namespace users_microservice.authentication;
public class JwtOptions
{
    public string Issuer { get; init; } = string.Empty;
    public string Audience { get; init; } = string.Empty;
    public string SecretKey { get; init; } = string.Empty;
}

public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
{
    private const string SectionName = "Jwt";
    private readonly IConfiguration _configuration;

    public JwtOptionsSetup(IConfiguration configuration) =>
        _configuration = configuration;
    public void Configure(JwtOptions options) =>
        _configuration.GetSection(SectionName).Bind(options);
}