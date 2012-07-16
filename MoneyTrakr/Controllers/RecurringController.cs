using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MoneyTrakr;

namespace MoneyTrakr.Controllers
{ 
    public class RecurringController : Controller
    {
        private MoneyTrakrEntities db = new MoneyTrakrEntities();

        //
        // GET: /Recuring/

        public ViewResult Index()
        {
            return View(db.Recurrings.ToList());
        }

        //
        // GET: /Recuring/Details/5

        public ViewResult Details(int id)
        {
            Recurring recurring = db.Recurrings.Find(id);
            return View(recurring);
        }

        //
        // GET: /Recuring/Create

        public ActionResult Create()
        {
            return View(new Recurring() { StartDate = DateTime.Now });
        } 

        //
        // POST: /Recuring/Create

        [HttpPost]
        public ActionResult Create(Recurring recurring)
        {
            if (ModelState.IsValid)
            {
                recurring.StartDate.AddHours(6);
                db.Recurrings.Add(recurring);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(recurring);
        }
        
        //
        // GET: /Recuring/Edit/5
 
        public ActionResult Edit(int id)
        {
            Recurring recurring = db.Recurrings.Find(id);
            return View(recurring);
        }

        //
        // POST: /Recuring/Edit/5

        [HttpPost]
        public ActionResult Edit(Recurring recurring)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recurring).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(recurring);
        }

        //
        // GET: /Recuring/Delete/5
 
        public ActionResult Delete(int id)
        {
            Recurring recurring = db.Recurrings.Find(id);
            return View(recurring);
        }

        //
        // POST: /Recuring/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Recurring recurring = db.Recurrings.Find(id);
            db.Recurrings.Remove(recurring);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}