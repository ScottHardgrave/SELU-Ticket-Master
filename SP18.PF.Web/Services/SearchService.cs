using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace SP18.PF.Web.Services
{
    /// <summary>
    /// this provides the ability to search and map entitites consistantly
    /// </summary>
    /// <typeparam name="TSearchDto">The class that holds the search terms</typeparam>
    /// <typeparam name="TEntity">The database table class (e.g. Event, Tour, User, etc)</typeparam>
    /// <typeparam name="TDto">The Dto we are mapping TEntity to</typeparam>
    public abstract class SearchServiceBase<TSearchDto, TEntity, TDto>
        where TEntity : class
    {
        private readonly IMapper mapper;
        private readonly DbContext dbContext;

        protected SearchServiceBase(IMapper mapper, DbContext dbContext)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        public Task<TDto> SelectOne(Expression<Func<TEntity, bool>> singleWhere)
        {
            var entities = dbContext.Set<TEntity>();
            var projection = entities.Where(singleWhere).ProjectTo<TDto>(mapper.ConfigurationProvider);
            return projection.SingleOrDefaultAsync();
        }

        public Task<TDto[]> SearchAll(TSearchDto searchTerms)
        {
            var entities = dbContext.Set<TEntity>();
            IQueryable<TEntity> filteredEntities;
            if (searchTerms != null)
            {
                filteredEntities = Filter(searchTerms, entities);
            }
            else
            {
                filteredEntities = entities.AsQueryable();
            }
            var results = filteredEntities.ProjectTo<TDto>(mapper.ConfigurationProvider);
            return results.ToArrayAsync();
        }

        protected abstract IQueryable<TEntity> Filter(TSearchDto search, IQueryable<TEntity> filterRows);
    }
}