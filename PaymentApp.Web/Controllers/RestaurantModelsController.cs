using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PaymentApp.Web.Models;

namespace PaymentApp.Web.Controllers
{
    public class RestaurantModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RestaurantModels
        public ActionResult Index()
        {
            return View(db.RestaurantModels.ToList());
        }

        // GET: RestaurantModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurantModel restaurantModel = db.RestaurantModels.Find(id);
            if (restaurantModel == null)
            {
                return HttpNotFound();
            }
            return View(restaurantModel);
        }

        // GET: RestaurantModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RestaurantModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RestaurantId,Email,Password")] RestaurantModel restaurantModel)
        {
            if (ModelState.IsValid)
            {
                db.RestaurantModels.Add(restaurantModel);
                db.SaveChanges();
                return RedirectToAction("RestaurantDetails", restaurantModel);
            }

            return View(restaurantModel);
        }



        public ActionResult RestaurantDetails(RestaurantModel restaurantModel)
        {
            ViewBag.RestaurantModelId = restaurantModel.Id;
            return View();
        }


        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var res = db.RestaurantModels.Where(r => r.Email.Equals(email) && r.Password.Equals(password))
                .SingleOrDefault();


            var restaurant = db.Restaurants.Find(res.RestaurantId);

            if (res == null)
                return RedirectToAction("Index", "Home");

            else
                return RedirectToAction("Dashboard", "Restaurants", restaurant);
            

        }

        // GET: RestaurantModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurantModel restaurantModel = db.RestaurantModels.Find(id);
            if (restaurantModel == null)
            {
                return HttpNotFound();
            }
            return View(restaurantModel);
        }

        // POST: RestaurantModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RestaurantId,Email,Password")] RestaurantModel restaurantModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restaurantModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(restaurantModel);
        }

        // GET: RestaurantModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurantModel restaurantModel = db.RestaurantModels.Find(id);
            if (restaurantModel == null)
            {
                return HttpNotFound();
            }
            return View(restaurantModel);
        }

        // POST: RestaurantModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RestaurantModel restaurantModel = db.RestaurantModels.Find(id);
            db.RestaurantModels.Remove(restaurantModel);
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
