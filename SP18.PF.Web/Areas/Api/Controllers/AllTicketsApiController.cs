using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SP18.PF.Core.Features.Tickets;
using SP18.PF.Web.Areas.Api.Models.Tickets;
using SP18.PF.Web.Data;
using SP18.PF.Web.Services;

namespace SP18.PF.Web.Areas.Api.Controllers
{
    public class AllTicketsApiController : Controller
    {
        private readonly NoSearchService<Ticket, TicketDto> searchService;

        public AllTicketsApiController(NoSearchService<Ticket, TicketDto> searchService)
        {
            this.searchService = searchService;
        }

        [HttpGet("allTickets")]
        [ProducesResponseType(typeof(TicketDto[]), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> List()
        {
            var tickets = await searchService.SearchAll(null);
            return Ok(tickets);
        }

    }
}