using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catalog1.Controllers;
using System.Web.Mvc;

namespace CatalogTest
{
    [TestClass]
    public class HomeTest
    {
        [TestMethod]
        public void HomeIndex()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
