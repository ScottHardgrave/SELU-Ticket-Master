using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SP18.PF.Core.Features.Venues;
using SP18.PF.Web.Areas.Api.Models.Venues;
using SP18.PF.Web.Services;

namespace SP18.PF.Web.Areas.Api.Controllers
{
    [Route("api/venues")]
    public class VenuesApiController : Controller
    {
        private readonly NoSearchService<Venue, VenueDto> searchService;

        public VenuesApiController(NoSearchService<Venue, VenueDto> searchService)
        {
            this.searchService = searchService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(VenueDto[]), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> List()
        {
            var result = await searchService.SearchAll(null);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(VenueDto), (int)HttpStatusCode.OK)]
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