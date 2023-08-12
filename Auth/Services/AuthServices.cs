using Auth.DB;
using Auth.Models;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace Auth.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly ConnectDB _connectDB;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthServices(ConnectDB connectDB, IPasswordHasher<User> passwordHasher = null)
        {
            _connectDB = connectDB;
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> AddRolesAsync(Role rule)
        {
            await _connectDB.roles.AddAsync(rule);
            await _connectDB.SaveChangesAsync();
            return true;
        }

        public async Task<User> AddUserAsync(UserDto userDto)
        {
            User user = new User();
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.RoleId = 1;
            user.email = userDto.email;
            user.Password = _passwordHasher.HashPassword(user, userDto.Password);
            user.dateTimeCreate = DateTime.Now;
            await _connectDB.users.AddAsync(user);
            await _connectDB.SaveChangesAsync();
            return user;
        }
    }
}
