using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernMarketTM.Data;
using ModernMarketTM.Web.Areas.Admin.Models.ViewModels;

namespace ModernMarketTM.Web.Controllers
{
    public class TypesController : Controller
    {
        public ModernMarketTmDbContext Context { get; set; }

        public IMapper Mapper { get; set; }

        public TypesController(
            ModernMarketTmDbContext context,
            IMapper mapper)
        {
            this.Context = context;
            Mapper = mapper;
        }

        public IActionResult Index(int id)
        {
            var categories = Context.Categories.Where(c => c.TypeId == id).ToList();
            var model = Mapper.Map<IEnumerable<CategoriesViewModel>>(categories);

            this.ViewData["typeName"] = Context.Types.Find(id).Name; 

            return this.View(model);
        }
    }
}
