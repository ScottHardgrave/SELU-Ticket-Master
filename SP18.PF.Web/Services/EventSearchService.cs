using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SP18.PF.Core.Features.Events;
using SP18.PF.Web.Areas.Api.Models.Events;

namespace SP18.PF.Web.Services
{
    public class EventSearchService : SearchServiceBase<EventSearchDto, Event,EventDto>
    {
        public EventSearchService(IMapper mapper, DbContext dbContext) : base(mapper, dbContext)
        {
        }

        protected override IQueryable<Event> Filter(EventSearchDto search, IQueryable<Event> filterRows)
        {
            var query = filterRows;
            if (!string.IsNullOrWhiteSpace(search.SearchTerm))
            {
                var searchTerm = search.SearchTerm.Trim();
                query = query.Where(x =>
                    x.Tour.Name.Contains(searchTerm) ||
                    x.Tour.Description.Contains(searchTerm) ||
                    x.Venue.Name.Contains(searchTerm) ||
                    x.Venue.Description.Contains(searchTerm) ||
                    x.Venue.PhysicalAddress.City.StartsWith(searchTerm) ||
                    x.Venue.PhysicalAddress.ZipCode.Equals(searchTerm));
            }
            
            if (search.VenueId.HasValue)
            {
                query = query.Where(x => x.VenueId == search.VenueId);
            }

            if (search.TourId.HasValue)
            {
                query = query.Where(x => x.TourId == search.TourId);
            }
            if (search.DateTime.HasValue)
            {
                var start = search.DateTime.Value.Date;
                var end = start.AddDays(1);
                // we are going to assume they want to find events that START on a day (e.g. "concert on May 5th" doesn't mean that the show stops before midnight)
                query = query.Where(x => start <= x.EventStart && x.EventStart < end);
            }

            return query;
        }
    }
}