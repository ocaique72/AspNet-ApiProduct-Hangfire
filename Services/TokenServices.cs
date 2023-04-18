using apiDesafio.Extensions;
using apiDesafio.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace apiDesafio.Services
{
    public class TokenServices
    {
        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
            var claims = user.GetClaims();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

// email: caiqueeee@teste.com
// senha: %KLOQd{0@4&9i{1{$e5Y
// Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImNhaXF1ZWVlZUB0ZXN0ZS5jb20iLCJuYmYiOjE2ODE4MTg4NDgsImV4cCI6MTY4MTg0NzY0OCwiaWF0IjoxNjgxODE4ODQ4fQ._Oozmp6oxS0AritUcrw2g0OopiXWBvqqrxy9Amq5zDU