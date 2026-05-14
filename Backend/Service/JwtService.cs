using Backend.Configuration;
using Backend.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class JwtService
{
    private readonly JwtSettings _jwt;

    public JwtService(IOptions<JwtSettings> jwt)
    {
        _jwt = jwt.Value;
    }

    public string CreateToken(User user)
    {
        // 1. dane które wkładamy do tokena
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

        // 2. klucz szyfrowania
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwt.Key));

        var creds = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        // 3. tworzenie tokena
        var token = new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwt.ExpiresMinutes),
            signingCredentials: creds
        );

        // 4. zamiana na string
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}