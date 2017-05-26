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
    public class SessionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Sessions
        public ActionResult Index()
        {
            return View(db.Sessions.ToList());
        }

        // GET: Sessions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = db.Sessions.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        public ActionResult Pay(int? id)
        {
            var transactions = db.Transactions.Where(t => t.SessionId == id).ToList();

            foreach (var item in transactions)
                item.IsPaid = true;

            var session = db.Sessions.Find(id);
            db.Sessions.Remove(session);

            db.SaveChanges();


            return Json(true, JsonRequestBehavior.AllowGet);
        }

        // GET: Sessions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sessions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(string restaurantId, string tableId, string slot, string customerId,
            DateTime? started)
        {

            Session session = new Session()
            {
                RestaurantId = Convert.ToInt32(restaurantId),
                TableId = Convert.ToInt32(tableId),
                Slot = Convert.ToInt32(slot),
                CustomerId = Convert.ToInt32(customerId),
                Started = started,
                Ended = DateTime.Now
            };

            db.Sessions.Add(session);
            db.SaveChanges();

            return Json(session, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTransactionsBySessionId(int? sessionId)
        {
            return Json(db.Transactions.Where(t => t.SessionId == sessionId).ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: Sessions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = db.Sessions.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        // POST: Sessions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RestaurantId,TableId,Slot,CustomerId,Started")] Session session)
        {
            if (ModelState.IsValid)
            {
                db.Entry(session).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(session);
        }

        // GET: Sessions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = db.Sessions.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        // POST: Sessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Session session = db.Sessions.Find(id);
            db.Sessions.Remove(session);
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
