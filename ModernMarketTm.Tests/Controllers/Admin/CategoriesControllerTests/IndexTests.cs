using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModernMarketTM.Data;
using ModernMarketTM.Models;
using ModernMarketTM.Web.Areas.Admin.Controllers;
using Moq;

namespace ModernMarketTM.Tests.Controllers.Admin.CategoriesControllerTests
{
    [TestClass]
    public class IndexTests
    {
        [TestMethod]
        public void Index_ReturnsAllCategories()
        {
            //Arrange
            var mockRepository = new Mock<IMapper>();

            var builder = new DbContextOptionsBuilder<ModernMarketTmDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var db = new ModernMarketTmDbContext(builder);
            db.Categories.Add(new Category(){Id = 1, Name = "Обувки", PictureUrl = "https://images-na.ssl-images-amazon.com/images/I/71tYXYR6E-L._UL1500_.jpg" , Slug = "shoes" , Type = new Models.Type(){Id = 1, Name = "Дрехи"}});

            var controller = new CategoriesController(db, mockRepository.Object);
            

            //Act
            var result = controller.Index();

            //Assert
            Assert.IsInstanceOfType(result,typeof(ViewResult));
        }
    }
}
