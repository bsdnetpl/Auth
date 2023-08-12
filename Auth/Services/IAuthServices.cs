﻿using Auth.Models;

namespace Auth.Services
{
    public interface IAuthServices
    {
        Task<bool> AddRolesAsync(Role rule);
        Task<User> AddUserAsync(UserDto userDto);
    }
}