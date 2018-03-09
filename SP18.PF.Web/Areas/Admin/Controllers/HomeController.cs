using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SP18.PF.Core.Features.Roles;

namespace SP18.PF.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        [Authorize(Roles = RoleNames.Admin)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
