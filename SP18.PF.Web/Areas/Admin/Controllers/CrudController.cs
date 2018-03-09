using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SP18.PF.Web.Services;

namespace SP18.PF.Web.Areas.Admin.Controllers
{
    public abstract class CrudController<TEntity, TListing, TCreate, TEdit> : Controller
        where TEntity : class
    {
        private readonly CrudService<TEntity, TListing, TCreate, TEdit> crudService;

        protected CrudController(CrudService<TEntity, TListing, TCreate, TEdit> crudService)
        {
            this.crudService = crudService;
        }

        protected abstract Expression<Func<TEntity, bool>> GetSelectSingle(string id);

        [HttpGet]
        public virtual async Task<IActionResult> Index()
        {
            return View(await crudService.SelectAll());
        }

        [HttpGet]
        public virtual async Task<IActionResult> Create()
        {
            var vm = await GetCreateViewModel(default(TCreate));
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Create(TCreate vm)
        {
            if (!ModelState.IsValid)
            {
                vm = await GetCreateViewModel(default(TCreate));
                return View(vm);
            }
            await crudService.Create(vm);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public virtual async Task<IActionResult> Edit(string id)
        {
            var item = await crudService.GetEdit(GetSelectSingle(id));
            if (item == null)
            {
                return new NotFoundResult();
            }
            var vm = await GetEditViewModel(item);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Edit(string id, TEdit vm)
        {
            if (!ModelState.IsValid)
            {
                vm = await GetEditViewModel(vm);
                return View(vm);
            }
            await crudService.Edit(GetSelectSingle(id), vm);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public virtual async Task<IActionResult> Details(string id)
        {
            var item = await crudService.SelectOne(GetSelectSingle(id));
            if (item == null)
            {
                return new NotFoundResult();
            }
            return View(item);
        }

        [HttpGet]
        public virtual Task<IActionResult> Delete(string id)
        {
            return Details(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Delete(string id, TEdit vm)
        {
            await crudService.Delete(GetSelectSingle(id));
            return RedirectToAction(nameof(Index));
        }

        protected virtual Task<TCreate> GetCreateViewModel(TCreate vm)
        {
            return Task.FromResult(vm);
        }

        protected virtual Task<TEdit> GetEditViewModel(TEdit vm)
        {
            return Task.FromResult(vm);
        }
    }
}