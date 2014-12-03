using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog1.Models
{
    public class Repository
    {
        IWidgetModel db;

        public Repository(IWidgetModel model)
        {
            db = model;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var products = db.Products.Include("Reviews");

            foreach (Product product in products)
            {
                product.Price -= Decimal.Round(product.Price * product.PriceModifier, 2);
            }
            return products;
        }
        public Product FindByID(int? id)
        {
            Product product = db.Products.Find(id);
            return product;
        }
        public void UpdateProduct(Product product)
        {
            db.Entry(product).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public Cart FindOrCreateCart(string username)
        {
            Cart userShoppingCart;
            if (GetCartByName(username)
                .Count() > 0)
            {
                userShoppingCart = GetCartByName(username)
                    .First();
            }
            else
            {
                userShoppingCart = new Cart()
                {
                    Carts = new List<CartItem>(),
                    UserName = username,
                };
                db.Carts.Add(userShoppingCart);
                db.SaveChanges();
            }
            return userShoppingCart;
        }

        public IQueryable<Cart> GetCartByName(string username)
        {
            return db.Carts.Where(q => q.UserName == username);
        }
        public void AddShoppingCart(int? productID, int amount, Cart userShoppingCart)
        {
            if (productID != null)
            {
                Product product = db.Products.Find(productID);
                product.Quantity -= amount;
                CheckQuantityLevels(product);

                if (GetCartItemByName(userShoppingCart, product)
                    .Count() > 0)
                {
                    GetCartItemByName(userShoppingCart, product)
                        .First().Quantity += amount;
                }
                else
                {
                    var cartItem = new CartItem()
                    {
                        Name = product.Name,
                        Quantity = amount,
                        Price = product.Price
                    };
                    userShoppingCart.Carts.Add(cartItem);
                }
                db.SaveChanges();
            }
        }

        private void CheckQuantityLevels(Product product)
        {
            if (product.Quantity < 5)
            {
                var email = new Email
                {
                    Name = "Inventory level low",
                    Recipient = "owner@widget.com",
                    Subject = "Inventory level low",
                    Content = "Check inventory for product, " + product.Name +
                    " with inventory level of " + product.Quantity,
                };
                db.Emails.Add(email);
                db.SaveChanges();
            }
        }

        private static IEnumerable<CartItem> GetCartItemByName(Cart userShoppingCart, Product product)
        {
            return userShoppingCart.Carts.Where(q => q.Name == product.Name);
        }
    }
}