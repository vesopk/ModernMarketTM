using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModernMarketTM.Data;
using ModernMarketTM.Models;
using ModernMarketTM.Web.Areas.Admin.Controllers;
using ModernMarketTM.Web.Mapping;
using Moq;

namespace ModernMarketTM.Tests.Controllers.Admin.UsersControllerTests
{
    [TestClass]
    public class ChangePasswordTests
    {
        private Mock<UserManager<User>> GetMockUserManager()
        {
            var userStoreMock = new Mock<IUserStore<User>>();
            return new Mock<UserManager<User>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);
        }

        [TestMethod]
        public void ChangePassword()
        {
            var builder = new DbContextOptionsBuilder<ModernMarketTmDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var db = new ModernMarketTmDbContext(builder);
            User user = new User()
            {
                UserName = "admin",
                Email = "admin@example.com",
                FullName = "Admin",
                Address = "AdminHome N17"
            };

            Mapper.Initialize(config => config.AddProfile<MappingProfile>());

            var manager = GetMockUserManager().Object;
            db.Users.Add(user);
            db.SaveChanges();

            var controller = new UsersController(db,Mapper.Instance, manager);
            var dbUser = db.Users.FirstOrDefault(u => u.UserName == "admin");

            var action = controller.ChangePassword(dbUser.Id) as ViewResult;

            Assert.IsNotNull(action.Model);
        }
    }
}
