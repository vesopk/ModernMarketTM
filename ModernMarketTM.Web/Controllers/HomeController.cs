using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ModernMarketTM.Data;

namespace ModernMarketTM.Web.Controllers
{
    public class HomeController : Controller
    {
        public IMapper Mapper { get; set; }

        public ModernMarketTmDbContext Context { get; set; }

        public HomeController(IMapper mapper,
            ModernMarketTmDbContext context)
        {
            this.Context = context;
            this.Mapper = mapper;
        }

        public IActionResult Index()
        {
            return this.View();
        }
        
    }
}
