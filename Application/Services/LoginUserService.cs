using Application.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class LoginUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly string _jwtSecret;
        public LoginUserService(IUserRepository userRepository ,string jwtSecret)
        {
            _userRepository = userRepository;
            _jwtSecret = jwtSecret;
           
        }

        public async Task<string> LoginAsync(string email, string password) {

            var user= await _userRepository.GetByEmailAsync(email);
            if (!(user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash)))
                throw new Exception("Invalid Credential");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id",user.Id.ToString()),
                    new Claim(ClaimTypes.Role,user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature),
            };

            var token=tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);
        }


    }
}
