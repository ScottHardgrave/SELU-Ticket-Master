using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SP18.PF.Web.Areas.Api.Models.Events;
using SP18.PF.Web.Services;

namespace SP18.PF.Web.Areas.Api.Controllers
{
    [Route("api/events")]
    public class EventsApiController : Controller
    {
        private readonly EventSearchService searchService;

        public EventsApiController(EventSearchService searchService)
        {
            this.searchService = searchService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(EventDto[]), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> List(EventSearchDto eventSearchDto)
        {
            var results = await searchService.SearchAll(eventSearchDto);
            return Ok(results);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(EventDto[]), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await searchService.SelectOne(x => x.Id == id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}