using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModernMarketTM.Data;
using ModernMarketTM.Models;

namespace ModernMarketTM.Web.Areas.Supplier.Pages.Supply
{
    [Authorize(Roles = "Supplier")]
    public class AddModel : PageModel
    {
        public List<CategoryInstance> Items { get; set; } = new List<CategoryInstance>();

        public ModernMarketTmDbContext Context { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public int Quantity { get; set; }

            public int CategoryId { get; set; }
        }

        public AddModel(ModernMarketTmDbContext context)
        {
            this.Context = context;
        }

        public void OnGet()
        {
            Items = Context.CategoryInstances.ToList();
        }

        public IActionResult OnPost()
        {
            var instance = Context.CategoryInstances.Find(Input.CategoryId);
            instance.Quantity += Input.Quantity;

            Context.CategoryInstances.Update(instance);
            Context.SaveChanges();

            return RedirectToPage("/Supply/Add");
        }
    }
}