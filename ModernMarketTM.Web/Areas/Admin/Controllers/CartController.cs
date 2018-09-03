using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ModernMarketTM.Models;
using ModernMarketTM.Web.Models;

namespace ModernMarketTM.Web.Areas.Admin.Controllers
{
    public class CartController : AdminController
    {
        public ShoppingCart Cart { get; set; }

        public CartController(ShoppingCart cart)
        {
            this.Cart = cart;
        }

        public IActionResult ResetCart()
        {
            this.Cart.ReservedQuantity = new Dictionary<int, int>();
            this.Cart.UsersCart = new Dictionary<string, List<CategoryInstance>>();
            return this.View();
        }
    }
}
