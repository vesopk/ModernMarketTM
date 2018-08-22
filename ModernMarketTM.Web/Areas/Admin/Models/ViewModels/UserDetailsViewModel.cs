using System.Collections.Generic;

namespace ModernMarketTM.Web.Areas.Admin.Models.ViewModels
{
    public class UserDetailsViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public bool IsBanned { get; set; }

        public IEnumerable<string> Roles { get; set; } = new List<string>();
    }
}
