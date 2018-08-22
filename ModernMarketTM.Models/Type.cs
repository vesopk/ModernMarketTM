using System.Collections.Generic;

namespace ModernMarketTM.Models
{
    public class Type
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
