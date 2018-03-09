using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;
using SP18.PF.Core.Features.Tours;
using SP18.PF.Core.Features.Venues;

namespace SP18.PF.Web.Areas.Admin.Models.Events
{
    public class EventCreateViewModelValidator : AbstractValidator<EventCreateViewModel>
    {
        private readonly DbContext context;

        public EventCreateViewModelValidator(DbContext context, IValidator<EventBaseViewModel> baseValidator)
        {
            this.context = context;
            RuleFor(x => x)
                .SetValidator(baseValidator);
            RuleFor(x => x.TourId)
                .NotNull()
                .CustomAsync(BeValidTourId);
            RuleFor(x => x.VenueId)
                .NotNull()
                .CustomAsync(BeValidVenueId);
        }


        private async Task BeValidTourId(int tourId, CustomContext e, CancellationToken token)
        {
            var matchingTours = await context.Set<Tour>().AnyAsync(x => x.Id == tourId, token);
            if (!matchingTours)
            {
                e.AddFailure("No such tour record.");
            }
        }

        private async Task BeValidVenueId(int venueId, CustomContext e, CancellationToken token)
        {
            var matchingVenues = await context.Set<Venue>().AnyAsync(x => x.Id == venueId, token);
            if (!matchingVenues)
            {
                e.AddFailure("No such venue record.");
            }
        }
    }
}