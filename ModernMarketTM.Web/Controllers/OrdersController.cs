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
using ModernMarketTM.Models.Enums;
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

            var itemsWithId = new Dictionary<int, int>();

            QuantityToBeSubtracted(items, itemsWithId);

            SubtractQuantity(itemsWithId);

            var orders = new List<UserInstance>();

            var now = DateTime.UtcNow;
            var date = DiscountsContants.DiscountDateToBeParsed;
            var discoutDate = DateTime.ParseExact(date, CommonConstants.DateTimeParseFormat, CultureInfo.InvariantCulture);

            var price = 0M;
            price = CalculatePrice(user, items, orders, now, discoutDate);

            var mainOrder = new Order()
            {
                Id = Guid.NewGuid().ToString(),
                Items = orders,
                RegisterDate = DateTime.UtcNow,
                Status = Status.Регистрирана,
                UserId = user.Id
            };

            Context.UserInstances.AddRange(orders);
            Context.Orders.Add(mainOrder);
            Context.SaveChanges();

            var model = new OrderFinishViewModel()
            {
                Address = user.Address,
                Price = price
            };

            this.Cart.UsersCart[user.Id].Clear();

            return this.View(model);
        }

        private static decimal CalculatePrice(User user, List<CategoryInstance> items, List<UserInstance> orders, DateTime now, DateTime discoutDate)
        {
            decimal price;
            if (now < discoutDate)
            {
                price = items.Sum(i => i.Price) * 0.9M;

                foreach (var item in items)
                {
                    var order = new UserInstance()
                    {
                        UserId = user.Id,
                        CategoryInstanceId = item.Id,
                        SoldPrice = item.Price * 0.9M
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
                        SoldPrice = item.Price
                    };

                    orders.Add(order);
                }
            }

            return price;
        }

        private void SubtractQuantity(Dictionary<int, int> itemsWithId)
        {
            foreach (var item in itemsWithId)
            {
                var instance = Context.CategoryInstances.Find(item.Key);
                instance.Quantity -= item.Value;
                Context.CategoryInstances.Update(instance);
                Context.SaveChanges();
            }
        }

        private static void QuantityToBeSubtracted(List<CategoryInstance> items, Dictionary<int, int> itemsWithId)
        {
            foreach (var item in items)
            {
                if (!itemsWithId.ContainsKey(item.Id))
                {
                    itemsWithId[item.Id] = 0;
                }

                itemsWithId[item.Id]++;
            }
        }

        [Authorize]
        public IActionResult Details()
        {
            var orders = Context.Orders.Include(o => o.Items).ToList();

            var models = new List<OrdersViewModel>();

            foreach (var order in orders)
            {
                var model = new OrdersViewModel()
                {
                    Id = order.Id,
                    Date = order.RegisterDate,
                    Status = order.Status,
                    Price = order.Items.Sum(i => i.SoldPrice).Value
                };

                models.Add(model);
            }

            return this.View(models);
        }

        [Authorize]
        public IActionResult About(string id)
        {
            var order = Context.Orders.Include(o => o.User).Include(o => o.Items).ThenInclude(i => i.CategoryInstance).FirstOrDefault(o => o.Id == id);

            var model = new OrderAboutViewModel()
            {
                Id = order.Id,
                Price = order.Items.Sum(o => o.SoldPrice).Value,
                Items = order.Items
            };

            return this.View(model);
        }
    }
}