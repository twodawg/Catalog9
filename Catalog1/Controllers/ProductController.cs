using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Catalog1.Models;
using System.Collections.Generic;
using System.Net;

namespace Catalog1.Controllers
{
    public class ProductController : Controller
    {
        //WidgetModel db = new WidgetModel();

        Repository repository;

        // Normal program operation
        public ProductController()
        {
            repository = new Repository(new WidgetModel());
        }
        // Testing
        public ProductController(Repository repo)
        {
            repository = repo;
        }

        // GET: Product
        [SiteMap]
        public ActionResult Index()
        {
            ViewBag.UserName = "Lord User";

            var products = repository.GetAllProducts();

            return View(products);
        }
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                //RedirectToAction("Index");
            }
            Product product = repository.FindByID(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        [Authorize]
        public ActionResult ShoppingCart(int? productID, int amount = 0)
        {
            Cart userShoppingCart;
            
            userShoppingCart = repository.FindOrCreateCart(User.Identity.Name);

            // Add a Shopping Cart Item (CartItems)
            repository.AddShoppingCart(productID, amount, userShoppingCart);
            
            //ViewBag.PriceTotal = userShoppingCart.PriceTotal;
            
            return View(userShoppingCart);
        }

        

        

        //[HttpGet]
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repository.FindByID(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.UpdateProduct(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }
    }
}