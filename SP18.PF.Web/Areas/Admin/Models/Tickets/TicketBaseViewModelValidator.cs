using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;
using SP18.PF.Core.Features.Events;
using SP18.PF.Core.Features.Users;

namespace SP18.PF.Web.Areas.Admin.Models.Tickets
{
    public class TicketViewModelValidator : AbstractValidator<TicketViewModel>
    {
        private readonly DbContext context;

        public TicketViewModelValidator(DbContext context)
        {
            this.context = context;

            RuleFor(x => x.PurchasePrice)
                .NotNull()
                .GreaterThan(0)
                .LessThan(10000)
                .Must(x => (int)(x * 100) == x * 100)
                .WithMessage("Be in dollars and cents (no partial pennies)");

            RuleFor(x => x.UserId)
                .NotNull()
                .CustomAsync(BeAValidUser);

            RuleFor(x => x.EventId)
                .NotNull()
                .CustomAsync(BeValidEvent);
        }

        private async Task BeAValidUser(int userId, CustomContext e, CancellationToken token)
        {
            var matchingUsers = await context.Set<User>().AnyAsync(x => x.Id == userId, token);
            if (!matchingUsers)
            {
                e.AddFailure("No such User.");
            }
        }

        private async Task BeValidEvent(int eventId, CustomContext e, CancellationToken token)
        {
            var matchingEvents = await context.Set<Event>().AnyAsync(x => x.Id == eventId, token);
            if (!matchingEvents)
            {
                e.AddFailure("No such Event.");
            }
        }
    }
}