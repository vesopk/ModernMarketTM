using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModernMarketTM.Web.Constants;

namespace ModernMarketTM.Web.Areas.Operator.Controllers
{
    [Area(RolesConstants.Operator)]
    [Authorize(Roles = RolesConstants.Operator)]
    public class OperatorController : Controller
    {
    }
}
