using Application.Interfaces;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RegisterUserService
    {
        private readonly IUserRepository _userRepository;
        public RegisterUserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;

        }
        public async Task RegisterAsync(string email, string username, string password, UserRole userRole)
        {

            var user = await _userRepository.GetByEmailAsync(email);
            if (user != null)
                throw new Exception("Email is already registred");

            var newUser = new User();

            newUser.Email = email;
            newUser.UserName = username;
            newUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
            newUser.Role = userRole;

           await _userRepository.AddAsync(newUser);


    }

}
}
