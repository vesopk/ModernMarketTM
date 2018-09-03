using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernMarketTM.Data;
using ModernMarketTM.Models.Enums;
using ModernMarketTM.Web.Areas.Operator.Models.ViewModel;
using ModernMarketTM.Web.Models;

namespace ModernMarketTM.Web.Areas.Operator.Controllers
{
    public class OrdersController : OperatorController
    {
        public ModernMarketTmDbContext Context { get; set; }

        public OrdersController(ModernMarketTmDbContext context)
        {
            this.Context = context;
        }

        public IActionResult Index()
        {
            var orders = Context.Orders.Include(o => o.Items).ToList().OrderByDescending(o => o.RegisterDate);

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

        public IActionResult About(string id)
        {
            var order = Context.Orders.Include(o => o.User).Include(o => o.Items).ThenInclude(i => i.CategoryInstance)
                .FirstOrDefault(o => o.Id == id);

            var model = new OrderAboutOperatorViewModel()
            {
                Id = order.Id,
                UserId = order.UserId,
                Price = order.Items.Sum(o => o.SoldPrice).Value,
                Items = order.Items,
                FullName = order.User.FullName,
                UserName = order.User.UserName,
                Address = order.User.Address
            };

            if (order.Status == Status.Изпратена || order.Status == Status.Отхвърлена)
            {
                model.IsSentOrDenied = true;
            }

            return this.View(model);
        }
        
        public IActionResult ConfirmOrder(string id)
        {
            Context.Orders.Find(id).Status = Status.Изпратена;
            Context.SaveChanges();

            return RedirectToAction("Index","Orders",new {area = "Operator"});
        }

        public IActionResult DenyOrder(string id)
        {
            var order = Context.Orders.Include(o => o.Items).FirstOrDefault(o => o.Id == id);

            var mapQuantity = new Dictionary<int,int>();

            foreach (var item in order.Items)
            {
                if (!mapQuantity.ContainsKey(item.CategoryInstanceId))
                {
                    mapQuantity[item.CategoryInstanceId] = 0;
                }

                mapQuantity[item.CategoryInstanceId]++;
            }

            foreach (var items in mapQuantity)
            {
                Context.CategoryInstances.Find(items.Key).Quantity += items.Value;
            }

            order.Status = Status.Отхвърлена;
            Context.SaveChanges();
            return RedirectToAction("Index", "Orders", new { area = "Operator" });
        }
    }
}