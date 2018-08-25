using System;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModernMarketTM.Data;
using ModernMarketTM.Models;
using ModernMarketTM.Web.Areas.Admin.Controllers;
using Moq;

namespace ModernMarketTM.Tests.Controllers.Admin.HomeControllerTests
{
    [TestClass]
    public class IndexTests
    {

        private Mock<UserManager<User>> GetMockUserManager()
        {
            var userStoreMock = new Mock<IUserStore<User>>();
            return new Mock<UserManager<User>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);
        }

        [TestMethod]
        public void Index_IsInAdminRole()
        {
            var userManager = GetMockUserManager();

            var controller = new HomeController(userManager.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.Role, "Admin")
                        }))
                    }
                }
            };

            Assert.IsTrue(controller.User.IsInRole("Admin"));
        }
    }
}
