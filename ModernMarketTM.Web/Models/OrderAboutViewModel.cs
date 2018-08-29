using System.Collections.Generic;
using ModernMarketTM.Models;

namespace ModernMarketTM.Web.Models
{
    public class OrderAboutViewModel
    {
        public string Id { get; set; }
        
        public decimal Price { get; set; }

        public IEnumerable<UserInstance> Items { get; set; } = new List<UserInstance>();
    }
}
