using System.Linq;
using FluentValidation;
using SP18.PF.Core.Features.Users;
using SP18.PF.Web.Models.Users;

namespace SP18.PF.Web.Areas.Admin.Models.Users
{
    public class UserCreateViewModelValidator : AbstractValidator<UserCreateViewModel>
    {
        public UserCreateViewModelValidator(IValidator<IRegisterUser> newUserValidator)
        {
            RuleFor(x => x)
                .SetValidator(newUserValidator);

            RuleFor(x => x.Role)
                .Must(x => Roles.List.Contains(x))
                .WithMessage("Invalid Role.");
        }
    }
}