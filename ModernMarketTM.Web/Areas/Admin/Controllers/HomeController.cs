using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ModernMarketTM.Models;
using ModernMarketTM.Web.Areas.Admin.Models.ViewModels;

namespace ModernMarketTM.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminController
    {
        public UserManager<User> UserManager { get; set; }

        public HomeController(UserManager<User> userManager)
        {
            this.UserManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await UserManager.GetUserAsync(User);
            var model = new AdminNameViewModel()
            {
                Name = user.FullName
            };
            return View(model);
        }
    }
}