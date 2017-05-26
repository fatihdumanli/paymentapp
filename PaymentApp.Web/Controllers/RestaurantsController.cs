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
    public class RestaurantsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Restaurants
        public ActionResult Index()
        {
            return View(db.Restaurants.ToList());
        }

        // GET: Restaurants/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return Json(restaurant, JsonRequestBehavior.AllowGet);
        }



        public ActionResult Dashboard(Restaurant restaurant)
        {
            return View(restaurant);
        }


        public IList<Table> GetRestaurantTables(int? restaurantId)
        {
            return db.Tables.Where(t => t.RestaurantId == restaurantId).ToList();
        }

        public bool IsSessionExist(int? restaurantId, int? tableId)
        {
            var session = db.Sessions.Where(s => s.RestaurantId == restaurantId && s.TableId == tableId)
                .Count();

            return session > 0;
        }




        public int GetSessionIdByTransaction(int? restaurantId, int? tableId)
        {
            var session = db.Sessions.Where(s => s.RestaurantId == restaurantId && s.TableId == tableId).SingleOrDefault();
            return session.Id;
        }

        public double GetUnpaidTransactionTotal(int? restaurantId, int? tableId)
        {
            List<Transaction> _transactions = new List<Transaction>();
            var sessions = db.Sessions.Where(s => s.RestaurantId == restaurantId && s.TableId == tableId).ToList();
            double total = 0;

            foreach (var item in sessions)
            {
                var transactions = db.Transactions.Where(t => t.SessionId == item.Id && t.IsPaid.Equals(false))
                                        .ToList();

                foreach (var t in transactions)
                {
                    var product = db.Products.Where(p => p.Id == t.ProductId).SingleOrDefault();
                    total += product.Price;
                }
            }

            return total;            
        }

        // GET: Restaurants/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Restaurants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Longitude,Latitude,PhoneNumber")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                db.Restaurants.Add(restaurant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(restaurant);
        }


        public ActionResult InsertRestaurant(string name, string longitude, string latitude, 
            string phonenumber, string restaurantModelId)
        {

            Restaurant res = new Restaurant()
            {
                Name = name,
                Longitude = Convert.ToDouble(longitude),
                Latitude = Convert.ToDouble(latitude),
                PhoneNumber = phonenumber
            };

            db.Restaurants.Add(res);
            db.SaveChanges();


            var rest = db.Restaurants.OrderByDescending(o => o.Id).Take(1).SingleOrDefault();

            var restaurantModel = db.RestaurantModels.Find(Convert.ToInt32(restaurantModelId));
            restaurantModel.RestaurantId = rest.Id;

            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        // GET: Restaurants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Longitude,Latitude,PhoneNumber")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restaurant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(restaurant);
        }

        // GET: Restaurants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Restaurant restaurant = db.Restaurants.Find(id);
            db.Restaurants.Remove(restaurant);
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
