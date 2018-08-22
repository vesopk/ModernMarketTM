using System.Collections.Generic;
using ModernMarketTM.Models;

namespace ModernMarketTM.Web.Areas.Admin.Models.ViewModels
{
    public class CategoryInstanceViewModel
    {
        public int Id { get; set; }

        public string Slug { get; set; }

        public string CategoryName { get; set; }

        public IEnumerable<CategoryInstance> Instances { get; set; } = new List<CategoryInstance>();
    }
}
