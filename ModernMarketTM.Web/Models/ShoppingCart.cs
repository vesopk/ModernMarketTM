using System;
using System.Collections.Generic;
using ModernMarketTM.Models;

namespace ModernMarketTM.Web.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            this.UsersCart = new Dictionary<string, List<CategoryInstance>>();
            this.ReservedQuantity = new Dictionary<int, int>();
        }

        public Dictionary<string,List<CategoryInstance>> UsersCart { get; set; }

        public Dictionary<int,int> ReservedQuantity { get; set; }
    }
}
