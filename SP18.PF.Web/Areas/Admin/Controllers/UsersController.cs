using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SP18.PF.Core.Features.Roles;
using SP18.PF.Core.Features.Users;
using SP18.PF.Web.Areas.Admin.Models.Users;
using SP18.PF.Web.Extensions;
using SP18.PF.Web.Services;

namespace SP18.PF.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RoleNames.Admin)]
    public class UsersController : CrudController<User, UserListViewModel, UserCreateViewModel, UserEditViewModel>
    {
        private readonly IValidator<UserCreateViewModel> createValidator;

        public UsersController(
            CrudService<User, UserListViewModel, UserCreateViewModel, UserEditViewModel> crudService,
            IValidator<UserCreateViewModel> createValidator)
            : base(crudService)
        {
            this.createValidator = createValidator;
        }

        [HttpGet, HttpPost]
        public async Task<IActionResult> VerifyEmail(string email)
        {
            var result = await createValidator.ValidateAsync(new UserCreateViewModel
            {
                Email = email
            });
            var errorMessage = result.Errors
                .Where(x => x.PropertyName == nameof(UserCreateViewModel.Email))
                .Select(x => x.ErrorMessage)
                .FirstOrDefault();
            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                return Json(true);
            }

            return Json(errorMessage);
        }

        protected override Task<UserCreateViewModel> GetCreateViewModel(UserCreateViewModel vm)
        {
            return Task.FromResult(new UserCreateViewModel
            {
                Email = vm?.Email
            });
        }

        protected override Expression<Func<User, bool>> GetSelectSingle(string id)
        {
            var item = id.SafeParseInt();
            return x => x.Id == item;
        }
    }
}
