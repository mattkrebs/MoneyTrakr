using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MoneyTrakr;
using MoneyTrakr.Models;

namespace MoneyTrakr.Controllers
{ 
    public class TransactionController : Controller
    {
        private MoneyTrakrModel db = new MoneyTrakrModel();

        //Server=107d983c-033c-448d-8b99-a090017367f8.sqlserver.sequelizer.com;Database=db107d983c033c448d8b99a090017367f8;User ID=dnjqajdmneiuncvj;Password=YCMHgP5T7gAZ877HdWKZ8xcbfdxTgmwmkcURTTgBKKQ34qEHkTTSC8wAALCcqjMU;
        // GET: /Transaction/

        public ViewResult Index()
        {
            return View(db.Transactions.ToList());
        }

        public ViewResult Add(int id)
        {
            Recurring recur = db.Recurrings.Find(id);
            if (recur != null)
            {
                Transaction tran = new Transaction();
                int daydiff = recur.StartDate.Day - DateTime.Now.Day;
                tran.CreatedDate = DateTime.Now.AddDays(daydiff);
                tran.Description = recur.Description;
                tran.Amount = recur.Amount;
                return View("Create", tran);
            }

            return View("Create");
        }

        //
        // POST: /Transaction/Create

        [HttpPost]
        public ActionResult Add(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(transaction);
        }

        public ViewResult Register()
        {
            return View(TransactionItem.GetAllTransactionsItems());
        }


        //
        // GET: /Transaction/Details/5

        public ViewResult Details(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            return View(transaction);
        }

        //
        // GET: /Transaction/Create

        public ActionResult Create()
        {
            if (db.Transactions.ToList().Count > 0)
                return View(new Transaction() { CreatedDate = DateTime.Now});
            else
                return RedirectToAction("Start");
            
        }

        public ActionResult Start()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Start(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(transaction);
        }

        //
        // POST: /Transaction/Create

        [HttpPost]
        public ActionResult Create(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(transaction);
        }
        
        //
        // GET: /Transaction/Edit/5
 
        public ActionResult Edit(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            return View(transaction);
        }

        //
        // POST: /Transaction/Edit/5

        [HttpPost]
        public ActionResult Edit(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transaction);
        }

        //
        // GET: /Transaction/Delete/5
 
        public ActionResult Delete(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            return View(transaction);
        }

        //
        // POST: /Transaction/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }






        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

    }
}