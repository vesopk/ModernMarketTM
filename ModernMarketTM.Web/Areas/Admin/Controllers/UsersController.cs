using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ModernMarketTM.Data;
using ModernMarketTM.Models;
using ModernMarketTM.Web.Areas.Admin.Models.BindingModels;
using ModernMarketTM.Web.Areas.Admin.Models.ViewModels;
using ModernMarketTM.Web.Constants;

namespace ModernMarketTM.Web.Areas.Admin.Controllers
{
    public class UsersController : AdminController
    {
        public ModernMarketTmDbContext Context { get; set; }

        public UserManager<User> UserManager { get; set; }

        public IMapper Mapper { get; set; }

        public UsersController(
            ModernMarketTmDbContext context, 
            IMapper mapper,
            UserManager<User> userManager)
        {
            this.Context = context;
            this.Mapper = mapper;
            this.UserManager = userManager;
        }

        public async Task<IActionResult> Configure()
        {
            var currentUser = await UserManager.GetUserAsync(User);

            var users = Context.Users
                .Where(u => u.Id != currentUser.Id)
                .ToList();

            var model = Mapper.Map<IEnumerable<UserViewModel>>(users);

            foreach (var userConsiseViewModel in model)
            {
                var user = users.FirstOrDefault(u => u.Id == userConsiseViewModel.Id);

                var roles = await UserManager.GetRolesAsync(user);
                SetRoles(userConsiseViewModel, user, roles);
            }

            return this.View(model);
        }

        private static void SetRoles(UserViewModel userConsiseViewModel, User user, IList<string> roles)
        {
            if (roles.Contains(RolesConstants.Supplier))
            {
                userConsiseViewModel.IsSupplier = true;
            }
            else if (roles.Contains(RolesConstants.Admin))
            {
                userConsiseViewModel.IsAdmin = true;
            }

            if (user.LockoutEnd != null)
            {
                userConsiseViewModel.IsBanned = true;
            }
        }

        public IActionResult ChangePassword(string id)
        {
            var user = Context.Users.Find(id);

            var model = Mapper.Map<ChangePasswordViewModel>(user);

            return this.View(model);
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordBindingModel model)
        {
            var user = Context.Users.Find(model.Id);
            var newPassword = UserManager.PasswordHasher.HashPassword(user,model.Password);
            user.PasswordHash = newPassword;
            Context.Users.Update(user);
            Context.SaveChanges();
            return this.RedirectToAction("Index", "Home", new {area = "Admin"});
        }

        public async Task<IActionResult> MakeSupplier(string id)
        {
            var user = Context.Users.Find(id);

            await UserManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, RolesConstants.Supplier));
            await UserManager.AddToRoleAsync(user, RolesConstants.Supplier);

            return this.RedirectToAction("Configure", "Users", new {area = "Admin"});
        }

        public IActionResult Ban(string id)
        {
            var user = Context.Users.Find(id);
            user.LockoutEnd = DateTimeOffset.UtcNow.AddYears(70);
            Context.Users.Update(user);
            Context.SaveChanges();

            return this.RedirectToAction("Configure", "Users", new { area = "Admin" });
        }

        public IActionResult UnBan(string id)
        {
            var user = Context.Users.Find(id);
            user.LockoutEnd = null;
            Context.Users.Update(user);
            Context.SaveChanges();

            return this.RedirectToAction("Configure", "Users", new { area = "Admin" });
        }

        public async Task<IActionResult> RemoveSupplier(string id)
        {
            var user = Context.Users.Find(id);

            await UserManager.RemoveClaimAsync(user, new Claim(ClaimTypes.Role, RolesConstants.Supplier));
            await UserManager.RemoveFromRoleAsync(user, RolesConstants.Supplier);

            return this.RedirectToAction("Configure", "Users", new { area = "Admin" });
        }

        public async Task<IActionResult> Details(string id)
        {
            var user = Context.Users.Find(id);
            var roles = await UserManager.GetRolesAsync(user);
            var model = Mapper.Map<UserDetailsViewModel>(user);
            model.Roles = roles;
            if (user.LockoutEnd != null)
            {
                model.IsBanned = true;
            }

            return this.View(model);
        }
    }
}
