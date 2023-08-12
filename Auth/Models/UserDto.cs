using FluentValidation;

namespace Auth.Models
{
    public class UserDto
    {
        public string email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }
    public class UserDtoValidation : AbstractValidator<UserDto>
    {
        public UserDtoValidation()
        {
            RuleFor(r => r.email)
                .EmailAddress()
                .NotNull()
                .NotEmpty()
                .WithMessage("Wronk email address");
            RuleFor(r => r.Password)
                .MinimumLength(8)
                .NotNull()
                .NotEmpty()
                .WithMessage("Minimum lenght Password is 8 character");
        }
    }
}
