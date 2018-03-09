using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SP18.PF.Web.Models;

namespace SP18.PF.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Error()
        {
            return Ok(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
