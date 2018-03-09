using System.Linq;
using FluentValidation;
using SP18.PF.Core.Features.Shared;
using SP18.PF.Core.Features.Users;

namespace SP18.PF.Web.Areas.Admin.Models.Users
{
    public class UserEditViewModelValidator : AbstractValidator<UserEditViewModel>
    {
        public UserEditViewModelValidator(IValidator<Address> addressValidator)
        {
            When(x=>!string.IsNullOrWhiteSpace(x.Password), () =>
            {
                RuleFor(x => x.Password)
                    .NotEmpty()
                    .MinimumLength(8);

                RuleFor(x => x.ConfirmPassword)
                    .Equal(x => x.Password)
                    .WithMessage("'Password' and 'Confirm Password' must match.");
            });

            RuleFor(x => x.BillingAddress)
                .NotNull()
                .SetValidator(addressValidator);

            RuleFor(x => x.Role)
                .Must(x => Roles.List.Contains(x))
                .WithMessage("Invalid Role.");
        }
    }
}