using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Shared;

public static class TokenGenerator
{
    public static string GetJwtToken(string username, string role)
    {
        // Secret key
        var secretKey = "8Byu59pXNa4wRqJ@KxM#FgVz2Lc!1QWb";

        // Symmetric security key
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        // Signing credentials
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // Issuer and Audience
        var issuer = "test-issuer"; // The issuing authority of the token
        var audience = "test-audience"; // The intended recipient of the token

        // Claims (add your custom claims here)
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, username), // User identifier
            new Claim(ClaimTypes.Role, role),  // Role
        };

        // Token descriptor
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(24), // Token expiration time
            Issuer = issuer, // Specify Issuer
            Audience = audience, // Specify Audience
            SigningCredentials = credentials
        };

        // Token handler
        var tokenHandler = new JwtSecurityTokenHandler();

        // Create token
        var token = tokenHandler.CreateToken(tokenDescriptor);

        // Return the token string
        return tokenHandler.WriteToken(token);
    }
}