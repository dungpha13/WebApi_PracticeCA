using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PracticeCA.Domain;

namespace PracticeCA.Application;

public class JwtHelper
{
    private static readonly IConfiguration _configuration;

    static JwtHelper()
    {
        // Initialize IConfiguration using ConfigurationBuilder
        _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
    }

    public static string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.UTF8.GetBytes(_configuration.GetSection("Secret:Key").Get<string>() ?? throw new Exception("Dont have key secret"));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Audience = _configuration.GetSection("Security.Bearer:Authority").Get<string>(),
            Issuer = _configuration.GetSection("Security.Bearer:Audience").Get<string>(),
            Subject = new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.Role, "Employee"),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
