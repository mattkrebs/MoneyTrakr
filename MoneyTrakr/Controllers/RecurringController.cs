using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MoneyTrakr.Framework;

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
                recurring.AccountID = 1;
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
                recurring.AccountID = 1;
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


        public void AddRecurringItemsToTransaction()
        {
           
                List<int> ids = (from a in db.Users
                                 orderby a.AccountID
                                 select a.AccountID).Distinct().ToList();

                foreach (var accountId in ids)
                {
                    List<Recurring> allRecurring = db.Recurrings.Where(r => r.AccountID == accountId).ToList();
                    if (allRecurring != null)
                    {
                        int daysbetweenrecuring = 14;

                        foreach (var r in allRecurring)
                        {
                            if (r.RecurWeekly)
                                daysbetweenrecuring = 7;


                            Transaction startTransaction = db.Transactions.Where(t => t.StartTransaction).FirstOrDefault();

                            DateTime maxDate = DateTime.Now;
                            DateTime now = r.StartDate.Date;
                            while (now <= maxDate)
                            {
                                if (r.RecurMontly)
                                {
                                    DateTime recurDate = DateTime.Parse(String.Format("{0}/{1}/{2}", now.Month, r.StartDate.Day, now.Year));
                                    if (now.Date == recurDate)
                                    {
                                        List<Transaction> transactions = db.Transactions.Where(x => x.CreatedDate == now && x.Description == r.Description).ToList();
                                        if (transactions == null || transactions.Count == 0)
                                        {
                                            Transaction tran = new Transaction();
                                            tran.Amount = r.Amount;
                                            tran.CreatedDate = recurDate;
                                            tran.Description = r.Description;
                                            tran.AccountID = accountId;
                                            db.Transactions.Add(tran);

                                            db.SaveChanges();
                                        }
                                    }
                                    now = now.AddMonths(1);
                                }
                                else
                                {

                                    List<Transaction> transactions = db.Transactions.Where(x => x.CreatedDate == now && x.Description == r.Description).ToList();
                                    if (transactions == null || transactions.Count == 0)
                                    {
                                        Transaction tran = new Transaction();
                                        tran.Amount = r.Amount;
                                        tran.CreatedDate = now.Date;
                                        tran.Description = r.Description;
                                        tran.AccountID = accountId;
                                        db.Transactions.Add(tran);

                                        db.SaveChanges();
                                    }


                                    now = now.AddDays(daysbetweenrecuring);
                                }


                            }

                        }
                    }
                
            }
        }
    }
}