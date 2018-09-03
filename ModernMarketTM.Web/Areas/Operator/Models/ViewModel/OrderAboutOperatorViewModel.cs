using System.Collections.Generic;
using ModernMarketTM.Models;

namespace ModernMarketTM.Web.Areas.Operator.Models.ViewModel
{
    public class OrderAboutOperatorViewModel
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public decimal Price { get; set; }

        public IEnumerable<UserInstance> Items { get; set; } = new List<UserInstance>();

        public string FullName { get; set; }

        public string Address { get; set; }

        public string UserName { get; set; }

        public bool IsSentOrDenied { get; set; }

    }
}
