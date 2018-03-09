using FluentValidation;
using SP18.PF.Core.Features.Shared;

namespace SP18.PF.Web.Areas.Admin.Models.Shared
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(x => x.AddressLine1)
                .NotEmpty()
                .Length(1, 64);
            RuleFor(x => x.AddressLine2)
                .Length(0, 64);
            RuleFor(x => x.City)
                .NotEmpty()
                .Length(1, 64);
            RuleFor(x => x.ZipCode)
                .NotEmpty()
                .Length(5);
            RuleFor(x => x.State)
                .NotEmpty()
                .Must(States.IsStateCode)
                .WithMessage("Invalid state, use the 2 letter state code. e.g. LA");
        }
    }
}