using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catalog1.Models;
using System.Collections.Generic;

namespace CatalogTest
{
    [TestClass]
    public class CartTest
    {
        [TestMethod]
        public void CartSubTotal()
        {
            // Arrange
            var cartItem = new CartItem { Price = 50.74m, Quantity = 4 };

            // Act
            var subtotal = cartItem.SubTotal;

            // Assert
            Assert.AreEqual(202.96m, subtotal);
        }
        [TestMethod]
        public void CartTotal()
        {
            // Arrange
            var cartItem = new CartItem { Price = 50.74m, Quantity = 4 };
            var cartItem2 = new CartItem { Price = 15.28m, Quantity = 2 };

            var cart = new Cart { Carts = new List<CartItem>() };

            // Act
            cart.Carts.Add(cartItem);
            cart.Carts.Add(cartItem2);

            // Assert
            Assert.AreEqual(233.52m, cart.PriceTotal);
        }
    }
}
