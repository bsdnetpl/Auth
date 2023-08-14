using Auth.DB;
using Auth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly ConnectDB _connectDB;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSttetings _authenticationSttetings;

        public AuthServices(ConnectDB connectDB, IPasswordHasher<User> passwordHasher, AuthenticationSttetings authenticationSttetings)
        {
            _connectDB = connectDB;
            _passwordHasher = passwordHasher;
            _authenticationSttetings = authenticationSttetings;
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

        public string GenerateJwt(LoginDto loginDto)
        {
            var user = _connectDB.users
                .Include(u => u.role)
                .FirstOrDefault(u => u.email == loginDto.email);
            if ((user is null))
            {
                return "Invalid username or password";
            }
           var result =  _passwordHasher.VerifyHashedPassword(user, user.Password, loginDto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                return "Invalid username or password";
            }
            var claims = new List<Claim>()
            {
             new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
             new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
             new Claim(ClaimTypes.Role,  $"{user.role.Name}"),
             new Claim("DateCreation",  user.dateTimeCreate.Value.ToString("yyyy-MM-dd"))
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSttetings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_authenticationSttetings.JwtExpireDays));
            var token = new JwtSecurityToken(
                _authenticationSttetings.JwtIssuer,
                _authenticationSttetings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
