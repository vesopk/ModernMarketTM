using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernMarketTM.Data;
using ModernMarketTM.Models;
using ModernMarketTM.Web.Areas.Admin.Models.BindingModels;
using ModernMarketTM.Web.Areas.Admin.Models.ViewModels;

namespace ModernMarketTM.Web.Controllers
{

    public class CategoriesController : Controller
    {
        public ModernMarketTmDbContext Context { get; set; }

        public IMapper Mapper { get; set; }

        public CategoriesController(ModernMarketTmDbContext context,IMapper mapper)
        {
            this.Context = context;
            this.Mapper = mapper;
        }

        public IActionResult Index()
        {
            var categories = Context.Categories.ToList();
            var model = Mapper.Map<IEnumerable<CategoriesViewModel>>(categories);
            return this.View(model);
        }
    }
}
