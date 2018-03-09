using FluentValidation;

namespace SP18.PF.Web.Areas.Admin.Models.Tours
{
    public class TourViewModelValidator : AbstractValidator<TourViewModel>
    {
        public TourViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(1, 64);

            RuleFor(x => x.Description)
                .NotEmpty();
        }
    }
}