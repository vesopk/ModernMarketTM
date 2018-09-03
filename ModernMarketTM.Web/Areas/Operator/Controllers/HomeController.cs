using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ModernMarketTM.Models;
using ModernMarketTM.Web.Areas.Admin.Models.ViewModels;

namespace ModernMarketTM.Web.Areas.Operator.Controllers
{
    public class HomeController : OperatorController
    {
        public UserManager<User> UserManager { get; set; }

        public HomeController(UserManager<User> userManager)
        {
            this.UserManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await UserManager.GetUserAsync(User);
            var model = new NameViewModel()
            {
                Name = user.FullName
            };
            return View(model);
        }
    }
}