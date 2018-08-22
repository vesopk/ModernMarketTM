using System;

namespace ModernMarketTM.Web.Models
{
    public class OrdersViewModel
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public decimal Price { get; set; }

        public DateTime Date { get; set; }
    }
}
