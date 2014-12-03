using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Catalog1.Models;

namespace Catalog1.Controllers
{
    public class ProductReviewsController : Controller
    {
        private WidgetModel db = new WidgetModel();

        // GET: ProductReviews
        [SiteMap]
        public ActionResult Index()
        {
            return View(db.ProductReviews.ToList());
        }

        // GET: ProductReviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductReview productReview = db.ProductReviews.Find(id);
            if (productReview == null)
            {
                return HttpNotFound();
            }
            return View(productReview);
        }

        // GET: ProductReviews/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.ProductID = id;

            return View(new ProductReview() { TimeStamp = DateTime.Now });
        }

        // POST: ProductReviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "ProductReviewID,IsApproved,Title,ReviewText,Rating,TimeStamp")] 
            ProductReview productReview,
            int ProductID)
        {
            if (ModelState.IsValid)
            {
                var product = db.Products.Find(ProductID);
                product.Reviews.Add(productReview);

                db.ProductReviews.Add(productReview);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productReview);
        }

        // GET: ProductReviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductReview productReview = db.ProductReviews.Find(id);
            if (productReview == null)
            {
                return HttpNotFound();
            }
            return View(productReview);
        }

        // POST: ProductReviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductReviewID,IsApproved,Title,ReviewText,Rating,TimeStamp")] ProductReview productReview)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productReview).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productReview);
        }

        // GET: ProductReviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductReview productReview = db.ProductReviews.Find(id);
            if (productReview == null)
            {
                return HttpNotFound();
            }
            return View(productReview);
        }

        // POST: ProductReviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductReview productReview = db.ProductReviews.Find(id);
            db.ProductReviews.Remove(productReview);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
