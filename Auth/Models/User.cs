using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace Auth.Models
{
    public class User
    {
        public int id { get; set; }
        public string email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password{ get; set; }
        public DateTime?  dateTimeCreate { get; set; }
        public int RoleId { get; set; }
        public virtual Role role { get; set; } 
    }

    public class UserValidation : AbstractValidator<User>
    {
        public UserValidation()
        {
            RuleFor(r => r.email)
                .EmailAddress()
                .NotNull()
                .NotEmpty()
                .WithMessage("Wronk email address");
            RuleFor(r => r.Password)
                .MinimumLength(8)
                .WithMessage("Minimum lenght Password is 8 character");
        }
    }
}
