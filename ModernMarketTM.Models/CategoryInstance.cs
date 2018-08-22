using System.Collections.Generic;

namespace ModernMarketTM.Models
{
   public class CategoryInstance
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public string PictureUrl { get; set; }
        
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public IEnumerable<UserInstance> Users { get; set; } = new List<UserInstance>();
    }
}
