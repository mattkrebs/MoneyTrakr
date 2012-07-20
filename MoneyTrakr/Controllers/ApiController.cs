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
           
            return Json(Summary.GetSummary(), JsonRequestBehavior.AllowGet);

        }
    }
}
