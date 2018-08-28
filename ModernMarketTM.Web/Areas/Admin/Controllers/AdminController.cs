using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModernMarketTM.Web.Constants;

namespace ModernMarketTM.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = RolesConstants.Admin)]
    [Area(RolesConstants.Admin)]
    public class AdminController : Controller
    {
    }
}
