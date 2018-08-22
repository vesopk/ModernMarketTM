using System;
using Microsoft.AspNetCore.Mvc;
using ModernMarketTM.Web.Constants;

namespace ModernMarketTM.Web.Areas.Admin.Controllers
{
    public class DiscountsController : AdminController
    {
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Configure(DateTime date)
        {
            DiscountsContants.DiscountDateToBeParsed = $"{date.Day}-{date.Month}-{date.Year}";

            var newDate = $"{date.Day}";

            switch (date.Month)
            {
                case 1: newDate += " януари.";
                    break;

                case 2:
                    newDate += " февруари.";
                    break;

                case 3:
                    newDate += " март.";
                    break;

                case 4:
                    newDate += " април.";
                    break;

                case 5:
                    newDate += " май.";
                    break;

                case 6:
                    newDate += " юни.";
                    break;

                case 7:
                    newDate += " юли.";
                    break;

                case 8:
                    newDate += " август.";
                    break;

                case 9:
                    newDate += " септември.";
                    break;

                case 10:
                    newDate += " октомври.";
                    break;

                case 11:
                    newDate += " ноември.";
                    break;

                case 12:
                    newDate += " декември.";
                    break;
            }

            DiscountsContants.DiscountDate = newDate;


            return this.RedirectToAction("Index", "Home", new {area = "Admin"});
        }
    }
}