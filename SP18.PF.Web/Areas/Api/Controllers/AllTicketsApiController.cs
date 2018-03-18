using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SP18.PF.Core.Features.Tickets;
using SP18.PF.Web.Data;

namespace SP18.PF.Web.Areas.Api.Controllers
{
    public class AllTicketsApiController : Controller
    {
        private readonly DataContext _dataContext;

        public AllTicketsApiController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("allTickets")]
        public IActionResult Get()
        {
            var tickets = _dataContext.Set<Ticket>();
            return Ok(tickets);
        }

    }
}