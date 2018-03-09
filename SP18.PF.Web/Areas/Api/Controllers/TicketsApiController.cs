using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SP18.PF.Web.Areas.Api.Models.Tickets;
using SP18.PF.Web.Services;

namespace SP18.PF.Web.Areas.Api.Controllers
{
    [Authorize]
    [Route("api/tickets")]
    public class TicketsApiController : Controller
    {
        private readonly TicketService ticketService;

        public TicketsApiController(TicketService ticketService)
        {
            this.ticketService = ticketService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(TicketDto[]), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetMyTickets()
        {
            var user = User;
            var result = await ticketService.GetUserTickets(user);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TicketDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> GetById(int id)
        {
            var user = User;
            var result = await ticketService.GetUserTickets(user, x=>x.Id == id);
            var record = result.SingleOrDefault();
            if (record == null)
            {
                return NotFound();
            }
            
            return Ok(record);
        }

        [HttpPost("purchase/{eventId}")]
        [ProducesResponseType(typeof(TicketDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Dictionary<string,string[]>),(int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Purchase(int eventId)
        {
            var user = User;
            var result = await ticketService.Purchase(user, eventId);
            if (result.Errors.Any())
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Data);
        }
    }
}
