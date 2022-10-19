using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CinemarkTest.Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace CinemarkTest.Web.Token;

public static class ValidateUserToken
{
    public static string GetToken(User user, JwtSettings jwtSettings)
    {
        var claims = new[] {
            new Claim("UserName", user.UserName)
        };
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            jwtSettings.Issuer,
            jwtSettings.Audience,
            claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: signIn);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}