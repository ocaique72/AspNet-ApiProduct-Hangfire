using apiDesafio.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace apiDesafio.Extensions
{
    //class extensao sao statics
    public static class RoleClaimsExtension
    {
        [Authorize("adm")]
        public static IEnumerable<Claim> GetClaims(this User user)
        {
            var result = new List<Claim>
        {
            new(ClaimTypes.Name, user.Email)
        };
            result.AddRange(
                user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Slug))
            );
            return result;
        }
    }
}
