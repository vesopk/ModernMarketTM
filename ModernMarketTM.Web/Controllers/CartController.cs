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

            var model = new List<CategoryInstance>();

            if (Cart.UsersCart.ContainsKey(user.Id))
            {
                model = Cart.UsersCart[user.Id].ToList();
            }

            this.ViewData["discountDateToBeParsed"] = DiscountsContants.DiscountDateToBeParsed;
            return this.View(model);
        }
    }
}
