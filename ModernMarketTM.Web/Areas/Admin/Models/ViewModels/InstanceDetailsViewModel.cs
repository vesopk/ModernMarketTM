namespace ModernMarketTM.Web.Areas.Admin.Models.ViewModels
{
    public class InstanceDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public string PictureUrl { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }
    }
}
