using System;
using ModernMarketTM.Models.Enums;

namespace ModernMarketTM.Web.Models
{
    public class OrdersViewModel
    {
        public string Id { get; set; }

        public decimal Price { get; set; }

        public Status Status { get; set; }

        public DateTime Date { get; set; }
    }
}
