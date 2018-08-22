using System.Collections.Generic;
using ModernMarketTM.Models;

namespace ModernMarketTM.Web.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            this.UsersCart = new Dictionary<string, List<CategoryInstance>>();
        }

        public Dictionary<string,List<CategoryInstance>> UsersCart { get; set; }
    }
}
