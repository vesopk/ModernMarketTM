using System.Collections.Generic;

namespace ModernMarketTM.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public string PictureUrl { get; set; }

        public int TypeId { get; set; }

        public Type Type { get; set; }

        public ICollection<CategoryInstance> Instances { get; set; } = new List<CategoryInstance>();
    }
}
