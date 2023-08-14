using Auth.Models;
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
                return Ok(await _authServices.AddRolesAsync(role));
            }
            return BadRequest(result);
        }
        [HttpPost("AddUser")]
        public async Task<ActionResult<bool>> AddUser([FromBody] UserDto userDto)
        {
            ValidationResult result = await _validatorUserDto.ValidateAsync(userDto);
            if (result.IsValid)
            {
                return Ok(await _authServices.AddUserAsync(userDto));
            }
            return BadRequest(result);
        }
        [HttpPost("login")]
        public ActionResult login([FromBody] LoginDto loginDto)
        {
            string token = _authServices.GenerateJwt(loginDto);
            return Ok(token);
        }
    }
}
