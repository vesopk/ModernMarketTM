using System.Collections.Generic;
using ModernMarketTM.Web.Areas.Admin.Models.ViewModels;

namespace ModernMarketTM.Web.Models
{
    public class HomeViewModel
    {
        public IEnumerable<CategoriesViewModel> CategoriesViewModels { get; set; } = new List<CategoriesViewModel>();

        public IEnumerable<TypesViewModel> TypesViewModels { get; set; } = new List<TypesViewModel>();
    }
}
