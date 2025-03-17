using Dot_net_ModelViewController.Data;
using Dot_net_ModelViewController.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Dot_net_ModelViewController.Repository
{
    public class UserAuthRepository: IUserAuth
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext appDbContext;

        public UserAuthRepository(AppDbContext appDbContext, IConfiguration configuration)
        {
            this.appDbContext = appDbContext;
            _configuration = configuration;
        }

        public string Authentication(string username, string password)
        {

            //var User = appDbContext.User.FirstOrDefault(x => x.UserName == username && x.Password == password);
            //if (User != null)
            //{
            //    // generate token for user
            //    var token = GenerateAccessToken(username);
            //    return token;
            //}
            //return null;
            var token = GenerateAccessToken(username);
            return token;

        }

        private string GenerateAccessToken(string userName)
        {
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
