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
        private readonly IValidator<Role> _validatorRole;

        public AuthController(IAuthServices authServices, IValidator<User> validatorUser, IValidator<Role> validatorRole)
        {
            _authServices = authServices;
            _validatorUser = validatorUser;
            _validatorRole = validatorRole;
        }
        [HttpPost("AddRoles")]
        public async Task<ActionResult<bool>> AddRoles(Role role)
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
    }
}
