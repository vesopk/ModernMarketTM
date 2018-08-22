using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernMarketTM.Data;
using ModernMarketTM.Models;
using ModernMarketTM.Web.Areas.Admin.Models.BindingModels;
using ModernMarketTM.Web.Areas.Admin.Models.ViewModels;

namespace ModernMarketTM.Web.Areas.Admin.Controllers
{

    public class CategoriesController : AdminController
    {
        public ModernMarketTmDbContext Context { get; set; }

        public IMapper Mapper { get; set; }

        public CategoriesController(ModernMarketTmDbContext context,IMapper mapper)
        {
            this.Context = context;
            this.Mapper = mapper;
        }

        public IActionResult Index()
        {
            var categories = Context.Categories.ToList();
            var model = Mapper.Map<IEnumerable<CategoriesViewModel>>(categories);
            return this.View(model);
        }


        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(AddCategorieBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }


            if (!Context.Types.Any(t => t.Name == model.TypeName))
            {
                var type = new Type()
                {
                    Name = model.TypeName
                };
                Context.Types.Add(type);
                Context.SaveChanges();
            }

            var category = Mapper.Map<Category>(model);
            category.Type = Context.Types.FirstOrDefault(t => t.Name == model.TypeName);

            Context.Categories.Add(category);
            Context.SaveChanges();

            return RedirectToAction("Index",new {area = "Admin"});
        }

        public IActionResult RemoveIndex()
        {
            var categories = Context.Categories.ToList();
            var model = Mapper.Map<IEnumerable<RemoveCategoriesViewModel>>(categories);
            return this.View(model);
        }
        
        public IActionResult Remove(int id, string slug)
        {
            var category = Context.Categories.Include(c => c.Instances).FirstOrDefault(c => c.Id == id);
            var instances = category.Instances;
            if (category.Slug != slug)
            {
                return NotFound();
            }
            Context.CategoryInstances.RemoveRange(instances);
            Context.Categories.Remove(category);
            Context.SaveChanges();

            return RedirectToAction("Index",new {area = "Admin"});
        }

    }
}
