using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModernMarketTM.Models;
using ModernMarketTM.Web.Constants;

namespace ModernMarketTM.Web.Areas.Supplier.Pages.Supply
{
    [Authorize(Roles = RolesConstants.Supplier)]
    public class IndexModel : PageModel
    {
        public string FullName { get; set; }

        public UserManager<User> UserManager { get; set; }

        public IndexModel(UserManager<User> userManager)
        {
            this.UserManager = userManager;
        }

        public async Task<IActionResult> OnGet()
        {
            var user = await UserManager.GetUserAsync(User);
            this.FullName = user.FullName;

            return this.Page();
        }
    }
}