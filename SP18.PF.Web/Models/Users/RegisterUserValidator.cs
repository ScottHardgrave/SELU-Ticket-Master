using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;
using SP18.PF.Core.Features.Shared;
using SP18.PF.Core.Features.Users;

namespace SP18.PF.Web.Models.Users
{
    public class RegisterUserValidator : AbstractValidator<IRegisterUser>
    {
        private readonly DbContext context;

        public RegisterUserValidator(DbContext context, IValidator<Address> addressValidator)
        {
            this.context = context;
            RuleFor(x => x.Email)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .EmailAddress()
                .CustomAsync(UniqueUser);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8);

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .WithMessage("'Password' and 'Confirm Password' must match.");

            RuleFor(x => x.BillingAddress)
                .NotNull()
                .SetValidator(addressValidator);
        }

        private async Task UniqueUser(string email, CustomContext e, CancellationToken token)
        {
            var matchingUsers = await context.Set<User>().AnyAsync(x => x.Email == email, token);
            if (matchingUsers)
            {
                e.AddFailure("User with matching email exists");
            }
        }

    }
}