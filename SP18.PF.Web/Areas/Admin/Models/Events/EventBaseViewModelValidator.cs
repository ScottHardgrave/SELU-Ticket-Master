using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SP18.PF.Core.Features.Events;

namespace SP18.PF.Web.Areas.Admin.Models.Events
{
    public class EventBaseViewModelValidator : AbstractValidator<EventBaseViewModel>
    {
        private readonly DbContext context;

        public EventBaseViewModelValidator(DbContext context)
        {
            this.context = context;
            RuleFor(x => x.TicketPrice)
                .NotNull()
                .GreaterThan(0)
                .LessThan(10000)
                .Must(x => (int)(x * 100) == x * 100)
                .WithMessage("Be in dollars and cents (no partial pennies)");

            RuleFor(x => x.EventStart)
                .NotNull()
                .GreaterThan(DateTimeOffset.Now.AddDays(1))
                .WithMessage("Events must be planned at least 24 hours in advance.");

            RuleFor(x => x.EventEnd)
                .NotNull()
                .GreaterThanOrEqualTo(x => x.EventStart.GetValueOrDefault().AddHours(1))
                .WithMessage("Event should be at least an hour in duration.");

            RuleFor(x => x.VenueId)
                .MustAsync(VenueMustNotOverlapOtherEvents)
                .WithMessage("There are other events at this Venue within an hour of the start or end time.");

            RuleFor(x => x.TourId)
                .MustAsync(TourMustNotOverlapOtherEvents)
                .WithMessage("This Tour is playing at another venue during this time.");
        }

        private async Task<bool> VenueMustNotOverlapOtherEvents(EventBaseViewModel viewModel, int venueId, CancellationToken token)
        {
            var startDate = viewModel.EventStart?.AddHours(-1);
            var endDate = viewModel.EventEnd?.AddHours(1);

            if (startDate == null || endDate == null)
            {
                return true;
            }

            var conflicts = await context.Set<Event>().AnyAsync(x =>
                x.Id != viewModel.Id &&
                x.VenueId == venueId &&
                x.EventStart < endDate && startDate < x.EventEnd, token);

            return !conflicts;
        }

        private async Task<bool> TourMustNotOverlapOtherEvents(EventBaseViewModel viewModel, int tourId, CancellationToken token)
        {
            var startDate = viewModel.EventStart;
            var endDate = viewModel.EventEnd;

            if (startDate == null || endDate == null)
            {
                return true;
            }

            var conflicts = await context.Set<Event>().AnyAsync(x =>
                x.Id != viewModel.Id &&
                x.TourId == tourId &&
                x.EventStart < endDate && startDate < x.EventEnd, token);

            return !conflicts;
        }
    }
}