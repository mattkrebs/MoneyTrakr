using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MoneyTrakr.Framework;
using MoneyTrakr.Web.Models;

namespace MoneyTrakr.Controllers
{
    public class ApiController : Controller
    {
        //
        // GET: /Api/
        public MoneyTrakrEntities db = new MoneyTrakrEntities();

        [HttpPost]
        public JsonResult AddTransaction(Transaction transaction)
        {
             
                db.Transactions.Add(transaction);
                db.SaveChanges();

                return Json(transaction);
          
        }


        [HttpGet]
        public JsonResult GetSummary()
        {
            decimal runningTotal = 0;
            Summary summary = new Summary();
            var summaries = DailySummary.GetDailySummary(180);

            foreach (var item in summaries)
            {
                foreach (TransactionItem t in item.TransactionItems)
                {
                    runningTotal = runningTotal + t.Amount;
                }
            }

            summary.CurrentBalance = runningTotal;
            return Json(summary);

        }
    }
}
