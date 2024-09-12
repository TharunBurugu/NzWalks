using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NZWalksAPI.IRepository
{
    public class TokenRepository : ITokenRepositoty
    {
        // By using Iconfiguration we can access the appsetting
        private readonly IConfiguration configuration;

        public TokenRepository( IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // Here implementation of CreateJwtToken interface
        public string CreateJwtToken(IdentityUser user, List<string> roles)
        {
            // Create claims for the roles
            // Email claim
            var claim = new List<Claim>();

            claim.Add(new Claim(ClaimTypes.Email, user.Email));

            foreach (var role in roles)
            {
                claim.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:key"]));

            //algorithum SecurityAlgorithms.HmacSha256
            var credentials = new SigningCredentials(key , SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
               configuration["Jwt:issuer"],
               configuration["Jwt:Audience"],
               claim,
               expires: DateTime.Now.AddMinutes(15),
               signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
            
        }
    }
}
