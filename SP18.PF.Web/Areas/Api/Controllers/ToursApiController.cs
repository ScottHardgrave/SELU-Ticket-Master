using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SP18.PF.Core.Features.Tours;
using SP18.PF.Web.Areas.Api.Models.Tours;
using SP18.PF.Web.Services;

namespace SP18.PF.Web.Areas.Api.Controllers
{
    [Route("api/tours")]
    public class ToursApiController : Controller
    {
        private readonly NoSearchService<Tour,TourDto> searchService;

        public ToursApiController(NoSearchService<Tour, TourDto> searchService)
        {
            this.searchService = searchService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(TourDto[]), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> List()
        {
            var results = await searchService.SearchAll(null);
            return Ok(results);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(TourDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await searchService.SelectOne(x=>x.Id == id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}