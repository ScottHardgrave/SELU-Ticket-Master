using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace SP18.PF.Web.Services
{
    // CRUD that we don't want to duplicate everywhere
    public class CrudService<TEntity, TListing, TCreate, TEdit>
        where TEntity : class
    {
        private readonly IMapper mapper;
        private readonly DbContext dbContext;
        private readonly IValidator<TCreate> createValidator;
        private readonly IValidator<TEdit> editValidator;

        public CrudService(
            IMapper mapper,
            DbContext dbContext,
            IValidator<TCreate> createValidator,
            IValidator<TEdit> editValidator)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.createValidator = createValidator;
            this.editValidator = editValidator;
        }

        public Task<TEdit> GetEdit(Expression<Func<TEntity, bool>> singleWhere)
        {
            var entities = dbContext.Set<TEntity>();
            var projection = entities
                .Where(singleWhere)
                .ProjectTo<TEdit>(mapper.ConfigurationProvider);
            return projection.SingleOrDefaultAsync();
        }

        public Task<List<TListing>> SelectAll()
        {
            var entities = dbContext.Set<TEntity>();
            var projection = entities.ProjectTo<TListing>(mapper.ConfigurationProvider);
            return projection.ToListAsync();
        }

        public Task<TEdit> SelectOne(Expression<Func<TEntity, bool>> singleWhere)
        {
            var entities = dbContext.Set<TEntity>();
            var projection = entities.Where(singleWhere).ProjectTo<TEdit>(mapper.ConfigurationProvider);
            return projection.SingleOrDefaultAsync();
        }

        public async Task Create(TCreate item)
        {
            await createValidator.ValidateAndThrowAsync(item);
            var entities = dbContext.Set<TEntity>();
            var newEntity = mapper.Map<TEntity>(item);
            entities.Add(newEntity);
            await dbContext.SaveChangesAsync();
        }

        public async Task Edit(Expression<Func<TEntity, bool>> singleWhere, TEdit item)
        {
            await editValidator.ValidateAndThrowAsync(item);
            var entities = dbContext.Set<TEntity>();
            var entity = await entities.SingleAsync(singleWhere);
            mapper.Map(item, entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task Delete(Expression<Func<TEntity, bool>> singleWhere)
        {
            var entities = dbContext.Set<TEntity>();
            var entity = await entities.SingleOrDefaultAsync(singleWhere);
            entities.Remove(entity);
            await dbContext.SaveChangesAsync();
        }
    }
}