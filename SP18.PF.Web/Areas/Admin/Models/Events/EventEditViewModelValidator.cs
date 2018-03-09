using FluentValidation;

namespace SP18.PF.Web.Areas.Admin.Models.Events
{
    public class EventEditViewModelValidator : AbstractValidator<EventEditViewModel>
    {

        public EventEditViewModelValidator( IValidator<EventBaseViewModel> baseValidator)
        {
            RuleFor(x => x)
                .SetValidator(baseValidator);
        }
    }
}