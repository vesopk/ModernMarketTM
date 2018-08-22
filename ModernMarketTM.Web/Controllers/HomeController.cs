using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ModernMarketTM.Data;
using ModernMarketTM.Web.Areas.Admin.Models.ViewModels;
using ModernMarketTM.Web.Constants;
using ModernMarketTM.Web.Models;

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
            var categories = Context.Categories.ToList();
            var types = Context.Types.ToList();
            var categoriesViewModels = Mapper.Map<IEnumerable<CategoriesViewModel>>(categories);
            var typesViewModels = Mapper.Map<IEnumerable<TypesViewModel>>(types);

            this.ViewData["discountDate"] = DiscountsContants.DiscountDate;
            this.ViewData["discountDateToBeParsed"] = DiscountsContants.DiscountDateToBeParsed;

            var model = new HomeViewModel()
            {
                CategoriesViewModels = categoriesViewModels,
                TypesViewModels = typesViewModels
            };
            return this.View(model);
        }
        
    }
}
