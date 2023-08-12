using FluentValidation;

namespace Auth.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

    }

    public class RoleValidation : AbstractValidator<Role>
    {
        public RoleValidation()
        {
            RuleFor(r => r.Name)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .WithMessage("Name is empty or length is too small, minimum length of character is 3");
        }
    }
}
