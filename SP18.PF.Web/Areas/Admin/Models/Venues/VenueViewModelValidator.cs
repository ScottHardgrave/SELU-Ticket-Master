using FluentValidation;
using SP18.PF.Core.Features.Shared;

namespace SP18.PF.Web.Areas.Admin.Models.Venues
{
    public class VenueViewModelValidator : AbstractValidator<VenueViewModel>
    {
        public VenueViewModelValidator(IValidator<Address> addressValidator)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(1, 64);

            RuleFor(x => x.Description)
                .NotEmpty();

            RuleFor(x => x.Capacity)
                .GreaterThan(0);

            RuleFor(x => x.PhysicalAddress)
                .SetValidator(addressValidator);
        }
    }
}