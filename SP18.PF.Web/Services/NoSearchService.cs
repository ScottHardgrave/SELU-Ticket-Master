using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace SP18.PF.Web.Services
{
    public class NoSearchService<TEntity, TDto> : SearchServiceBase<object, TEntity, TDto>
        where TEntity : class
    {
        public NoSearchService(IMapper mapper, DbContext dbContext) : base(mapper, dbContext)
        {
        }

        protected override IQueryable<TEntity> Filter(object search, IQueryable<TEntity> filterRows)
        {
            return filterRows;
        }
    }
}