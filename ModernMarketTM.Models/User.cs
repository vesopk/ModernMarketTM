using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ModernMarketTM.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }

        public string Address { get; set; }

        public IEnumerable<Order> Orders { get; set; } = new List<Order>();
    }
}
