﻿namespace ModernMarketTM.Web.Models
{
    public class CartCategoryInstanceViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public string PictureUrl { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
