using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModernMarketTM.Web.Areas.Admin.Controllers;
using ModernMarketTM.Web.Constants;

namespace ModernMarketTM.Tests.Controllers.Admin.DiscountsControllerTests
{
    [TestClass]
    public class ConfigureTests
    {
        [TestMethod]
        public void Configure_DateTest()
        {
            var controller = new DiscountsController();

            controller.Configure(DateTime.ParseExact("01-01-2018", "dd-MM-yyyy", CultureInfo.InvariantCulture));

            var date = DiscountsContants.DiscountDate;

            if (date != "1 януари.")
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void Configure_DateTestParsed()
        {
            var controller = new DiscountsController();

            controller.Configure(DateTime.ParseExact("01-01-2018", "dd-MM-yyyy", CultureInfo.InvariantCulture));

            var date = DiscountsContants.DiscountDateToBeParsed;

            if (date != "1-1-2018")
            {
                Assert.Fail();
            }
        }
    }
}
