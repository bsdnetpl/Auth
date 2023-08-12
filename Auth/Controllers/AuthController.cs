﻿using Auth.Models;
using Auth.Services;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authServices;
        private readonly IValidator<User> _validatorUser;
        private readonly IValidator<UserDto> _validatorUserDto;
        private readonly IValidator<Role> _validatorRole;

        public AuthController(IAuthServices authServices, IValidator<User> validatorUser, IValidator<Role> validatorRole, IValidator<UserDto> validatorUserDto)
        {
            _authServices = authServices;
            _validatorUser = validatorUser;
            _validatorRole = validatorRole;
            _validatorUserDto = validatorUserDto;
        }
        [HttpPost("AddRoles")]
        public async Task<ActionResult<bool>> AddRoles([FromBody] Role role)
        {
            ValidationResult result = await _validatorRole.ValidateAsync(role);
            if (result.IsValid)
            {
                try
                {
                    return Ok(await _authServices.AddRolesAsync(role));
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }
            return BadRequest(result);
        }
        [HttpPost("AddUser")]
        public async Task<ActionResult<bool>> AddUser([FromBody] UserDto userDto)
        {
            ValidationResult result = await _validatorUserDto.ValidateAsync(userDto);
            if (result.IsValid)
            {
                try
                {
                    return Ok(await _authServices.AddUserAsync(userDto));
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }
            return BadRequest(result);
        }
    }
}
