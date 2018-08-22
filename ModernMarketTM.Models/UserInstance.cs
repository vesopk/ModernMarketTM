using System;

namespace ModernMarketTM.Models
{
    public class UserInstance
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public int CategoryInstanceId { get; set; }

        public decimal? SoldPrice { get; set; }

        public CategoryInstance CategoryInstance { get; set; }

        public DateTime RegisterDate { get; set; }
    }
}
