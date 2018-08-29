using System;
using System.Collections.Generic;
using ModernMarketTM.Models.Enums;

namespace ModernMarketTM.Models
{
    public class Order
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public Status Status { get; set; }

        public DateTime RegisterDate { get; set; }

        public IEnumerable<UserInstance> Items { get; set; } = new List<UserInstance>();
    }
}
