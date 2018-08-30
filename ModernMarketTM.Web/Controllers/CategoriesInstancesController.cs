using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernMarketTM.Data;
using ModernMarketTM.Models;
using ModernMarketTM.Web.Areas.Admin.Models.BindingModels;
using ModernMarketTM.Web.Areas.Admin.Models.ViewModels;
using ModernMarketTM.Web.Models;

namespace ModernMarketTM.Web.Controllers
{
    public class CategoriesInstancesController : Controller
    {
        public ModernMarketTmDbContext Context { get; set; }

        public IMapper Mapper { get; set; }
        
        public UserManager<User> UserManager { get; set; }

        public ShoppingCart Cart { get; set; }

        public CategoriesInstancesController(
            ModernMarketTmDbContext context,
            IMapper mapper,
            UserManager<User> userManager,
            ShoppingCart cart)
        {
            this.Context = context;
            this.Mapper = mapper;
            this.UserManager = userManager;
            this.Cart = cart;
        }

        public IActionResult Index(int id, string slug)
        {
            var categories = Context.CategoryInstances.Include(ci => ci.Category).Where(ci => ci.CategoryId == id).ToList();
            if (categories.Any(c => c.Category.Slug != slug))
            {
                return NotFound();
            }


            var categoryName = Context.Categories.Find(id).Name;

            var model = new CategoryInstanceViewModel()
            {
                Id = id,
                Slug = slug,
                Instances = categories,
                CategoryName = categoryName
            };
            return this.View(model);
        }

        public IActionResult Details(int id, string slug)
        {
            var instance = Context.CategoryInstances.Find(id);

            if (instance.Slug != slug)
            {
                return NotFound();
            }

            var model = Mapper.Map<InstanceDetailsViewModel>(instance);

            return this.View(model);
        }

        [Authorize]
        public async Task<IActionResult> AddToCart(int id, string slug)
        {
            var item = Context.CategoryInstances.Include(ci => ci.Category).FirstOrDefault(ci => ci.Id == id);

            if (item.Slug != slug)
            {
                return NotFound();
            }

            var user = await this.UserManager.GetUserAsync(User);

            if (!Cart.UsersCart.ContainsKey(user.Id))
            {
                Cart.UsersCart[user.Id] = new List<CategoryInstance>();
            }

            if (!Cart.ReservedQuantity.ContainsKey(item.Id))
            {
                Cart.ReservedQuantity[item.Id] = 0;
            }

            if (item.Quantity - Cart.ReservedQuantity[item.Id] > 0)
            {
                Cart.UsersCart[user.Id].Add(item);
                Cart.ReservedQuantity[item.Id]++;
            }

            return this.RedirectToAction("Details",new{ id=item.Id,slug=item.Slug});
        }


        [Authorize]
        public async Task<IActionResult> RemoveFromCart(int id, string slug)
        {
            var instance = Context.CategoryInstances.Find(id);

            if (instance.Slug != slug)
            {
                return NotFound();
            }

            var user = await this.UserManager.GetUserAsync(User);
            var item = Cart.UsersCart[user.Id].FirstOrDefault(ci => ci.Id == id);
            Cart.UsersCart[user.Id].Remove(item);
            Cart.ReservedQuantity[instance.Id]--;

            return this.RedirectToAction("Index", "Cart");
        }
    }
}
