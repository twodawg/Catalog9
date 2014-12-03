using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catalog1.Models;
using System.Linq;
using Catalog1.Controllers;
using System.Web.Mvc;

namespace CatalogTest
{
    [TestClass]
    public class ProductTest
    {
        TestContext context;

        [TestInitialize]
        public void CreateDefaultProductContext()
        {
            context = new TestContext();

            context.Products.Add(
                new Product
                {
                    ProductID = 1,
                    Name = "Widget",
                    Price = 45m,
                    Quantity = 20
                }
            );
        }
        [TestMethod]
        public void ProductInventoryLowShouldEmail()
        {
            // Arrange   using System.Linq;
            context.Products.First().Quantity = 5;

            var repository = new Repository(context);

            // Act
            var cart = repository.FindOrCreateCart("mtwohey2@yahoo.com");

            repository.AddShoppingCart(1, 1, cart);

            // Assert
            Assert.AreEqual(1, context.Emails.Count());
        }
        [TestMethod]
        public void ProductControllerDetails()
        {
            // Arrange
            var repository = new Repository(context);

            ProductController prodcontroller =
                new ProductController(repository);

            // Act
            ViewResult result = prodcontroller.Detail(1) as ViewResult;

            // Assert
            Assert.AreEqual("Widget", ((Product)result.Model).Name);
            Assert.AreEqual(45m, ((Product)result.Model).Price);
        }

        
    }
}
