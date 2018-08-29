using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ModernMarketTM.Data;
using ModernMarketTM.Web.Models;

namespace ModernMarketTM.Web.Controllers
{
    public class SearchingController : Controller
    {
        public ModernMarketTmDbContext Context { get; set; }

        public IMapper Mapper { get; set; }

        public SearchingController(
            ModernMarketTmDbContext context,
            IMapper mapper)
        {
            this.Context = context;
            this.Mapper = mapper;
        }

        public IActionResult Result(string searchTerm)
        {
            if (searchTerm == null)
            {
                return this.Redirect("/");
            }

            var items = Context.CategoryInstances.Where(ci => ci.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
            ViewData["searchTerm"] = searchTerm;

            var model = Mapper.Map<IEnumerable<SearchItemsViewModel>>(items);

            return this.View(model);
        }
    }
}
