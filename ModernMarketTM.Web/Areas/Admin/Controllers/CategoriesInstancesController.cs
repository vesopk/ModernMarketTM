using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernMarketTM.Data;
using ModernMarketTM.Models;
using ModernMarketTM.Web.Areas.Admin.Models.BindingModels;
using ModernMarketTM.Web.Areas.Admin.Models.ViewModels;
using ModernMarketTM.Web.Models;

namespace ModernMarketTM.Web.Areas.Admin.Controllers
{
    public class CategoriesInstancesController : AdminController
    {
        public ModernMarketTmDbContext Context { get; set; }
        
        public IMapper Mapper { get; set; }

        public CategoriesInstancesController(
            ModernMarketTmDbContext context,
            IMapper mapper)
        {
            this.Context = context;
            this.Mapper = mapper;
        }

        public IActionResult Index(int id, string slug)
        {
            var categories = Context.CategoryInstances.Include(ci => ci.Category).Where(ci => ci.CategoryId == id).ToList();
            if (categories.Any(c => c.Category.Slug != slug))
            {
                return NotFound();
            }

            var model = new CategoryInstanceViewModel()
            {
                Id = id,
                Slug = slug,
                Instances = categories
            };
            return this.View(model);
        }

        public IActionResult Add(int id,string slug)
        {
            var category = Context.Categories.Find(id);
            if (category.Slug != slug)
            {
                return NotFound();
            }

            var model = new AddCategoryInstanceViewModel()
            {
                Id = id,
                Slug = slug,
                Name = category.Name
            };

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Add(AddInstanceCategoryBindingModel bm)
        {
            if (!ModelState.IsValid)
            {
                var category = Context.Categories.Find(bm.CategoryId);
                if (category.Slug != bm.CategorySlug)
                {
                    return NotFound();
                }

                var model = new AddCategoryInstanceViewModel()
                {
                    Id = bm.CategoryId,
                    Slug = bm.CategorySlug,
                    Name = category.Name
                };
                return this.View(model);
            }

            var instance = Mapper.Map<CategoryInstance>(bm);
            instance.Quantity = 10;

            Context.CategoryInstances.Add(instance);
            Context.SaveChanges();

            return this.RedirectToAction("Index",new{id=bm.CategoryId,slug=bm.CategorySlug});
        }

        public IActionResult Details(int id,string slug)
        {
            var instance = Context.CategoryInstances.Find(id);

            if (instance.Slug != slug)
            {
                return NotFound();
            }

            var model = Mapper.Map<InstanceDetailsViewModel>(instance);

            return this.View(model);
        }
    }
}
