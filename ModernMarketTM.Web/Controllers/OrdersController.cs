using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernMarketTM.Data;
using ModernMarketTM.Models;
using ModernMarketTM.Web.Constants;
using ModernMarketTM.Web.Models;

namespace ModernMarketTM.Web.Controllers
{
    public class OrdersController : Controller
    {
        public ModernMarketTmDbContext Context { get; set; }

        public UserManager<User> UserManager { get; set; }

        public ShoppingCart Cart { get; set; }

        public OrdersController(
            ModernMarketTmDbContext context,
            UserManager<User> userManager,
            ShoppingCart cart)
        {
            this.Context = context;
            this.UserManager = userManager;
            this.Cart = cart;
        }

        public async Task<IActionResult> Index()
        {
            var user = await this.UserManager.GetUserAsync(User);
            var items = this.Cart.UsersCart[user.Id].ToList();

            var itemsWithId = new Dictionary<int,int>();

            foreach (var item in items)
            {
                if (!itemsWithId.ContainsKey(item.Id))
                {
                    itemsWithId[item.Id] = 0;
                }

                itemsWithId[item.Id]++;
            }

            foreach (var item in itemsWithId)
            {
                var instance = Context.CategoryInstances.Find(item.Key);
                instance.Quantity -= item.Value;
                Context.CategoryInstances.Update(instance);
                Context.SaveChanges();
            }

            var orders = new List<UserInstance>();

            

            var now = DateTime.UtcNow;
            var date = DiscountsContants.DiscountDateToBeParsed;
            var discoutDate = DateTime.ParseExact(date, "d-M-yyyy", CultureInfo.InvariantCulture);

            var price = 0M;

            if (now < discoutDate)
            {
                price = items.Sum(i => i.Price) * 0.9M;

                foreach (var item in items)
                {
                    var order = new UserInstance()
                    {
                        UserId = user.Id,
                        CategoryInstanceId = item.Id,
                        SoldPrice = item.Price * 0.9M,
                        RegisterDate = DateTime.UtcNow.Date
                    };

                    orders.Add(order);
                }
            }
            else
            {
                price = items.Sum(i => i.Price);
                foreach (var item in items)
                {
                    var order = new UserInstance()
                    {
                        UserId = user.Id,
                        CategoryInstanceId = item.Id,
                        SoldPrice = item.Price,
                        RegisterDate = DateTime.UtcNow.Date
                    };

                    orders.Add(order);
                }
            }


            Context.Orders.AddRange(orders);
            Context.SaveChanges();
            var model = new OrderFinishViewModel()
            {
                Address = user.Address,
                Price = price
            };

            this.Cart.UsersCart[user.Id].Clear();

            return this.View(model);
        }

        [Authorize]
        public async Task<IActionResult> Details()
        {
            var user = await UserManager.GetUserAsync(User);
            var orders = Context.Orders.Include(o => o.CategoryInstance).Where(o => o.UserId == user.Id).ToList();

            var models = new List<OrdersViewModel>();

            foreach (var order in orders)
            {
                var model = new OrdersViewModel()
                {
                    Id = order.Id,
                    CategoryId = order.CategoryInstanceId,
                    Name = order.CategoryInstance.Name,
                    Price = order.SoldPrice.Value,
                    Date = order.RegisterDate,
                    Slug = order.CategoryInstance.Slug
                };

                models.Add(model);
            }

            return this.View(models);
        }
    }
}