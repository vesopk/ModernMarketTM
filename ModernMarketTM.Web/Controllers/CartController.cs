using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ModernMarketTM.Data;
using ModernMarketTM.Models;
using ModernMarketTM.Web.Constants;
using ModernMarketTM.Web.Models;

namespace ModernMarketTM.Web.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        public ShoppingCart Cart { get; set; }

        public UserManager<User> UserManager { get; set; }

        public CartController(
            ShoppingCart cart,
            UserManager<User> userManager)
        {
            this.Cart = cart;
            this.UserManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await UserManager.GetUserAsync(User);

            var model = new List<CartCategoryInstanceViewModel>();
            var modelMap = new Dictionary<int,CartCategoryInstanceViewModel>();

            if (Cart.UsersCart.ContainsKey(user.Id))
            {
                var postModel = Cart.UsersCart[user.Id].ToList();

                foreach (var instance in postModel)
                {
                    if (!modelMap.ContainsKey(instance.Id))
                    {
                        modelMap[instance.Id] = new CartCategoryInstanceViewModel();
                    }
                    modelMap[instance.Id].Id = instance.Id;
                    modelMap[instance.Id].Description = instance.Description;
                    modelMap[instance.Id].Slug = instance.Slug;
                    modelMap[instance.Id].PictureUrl = instance.PictureUrl;
                    modelMap[instance.Id].Name = instance.Name;
                    modelMap[instance.Id].Price = instance.Price;
                    modelMap[instance.Id].Quantity++;
                }

                model = modelMap.Values.ToList();
            }
            
            return this.View(model);
        }
    }
}
