using System.ComponentModel.DataAnnotations;

namespace ModernMarketTM.Web.Areas.Admin.Models.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }
        
        public bool IsAdmin { get; set; }
        
        public bool IsSupplier { get; set; }

        public bool IsBanned { get; set; }
    }
}
