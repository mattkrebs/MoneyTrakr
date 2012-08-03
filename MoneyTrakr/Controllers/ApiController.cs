using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MoneyTrakr.Framework;
using MoneyTrakr.Web.Models;
using System.Data;

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
            if (transaction.ID == 0)
            {
                db.Transactions.Add(transaction);
            }
            else
            {
                db.Entry(transaction).State = EntityState.Modified;
            }
                db.SaveChanges();

                return Json(transaction);
        }

        [HttpGet]
        public JsonResult GetTransaction(int id)
        {
            return Json(db.Transactions.Find(id), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetSummary()
        {
           
            return Json(Summary.GetSummary(), JsonRequestBehavior.AllowGet);

        }
    }
}
