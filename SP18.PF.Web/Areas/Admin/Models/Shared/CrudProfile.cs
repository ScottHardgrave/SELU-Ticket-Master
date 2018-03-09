using AutoMapper;

namespace SP18.PF.Web.Areas.Admin.Models.Shared
{
    public abstract class CrudProfile<TEntity, TListing, TCreate, TEdit> : Profile
        where TEntity : class
    {
        protected CrudProfile()
        {
            CreateMap<TEntity, TListing>();
            CreateMap<TEntity, TEdit>();
            CreateMap<TCreate, TEntity>();
            CreateMap<TEdit, TEntity>();
        }
    }
}