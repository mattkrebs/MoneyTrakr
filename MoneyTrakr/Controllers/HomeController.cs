using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MoneyTrakr.Web.Models;

namespace MoneyTrakr.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
           // var grouped = TransactionItem.GetProjectionData(180);
            return View();
        }
        public ActionResult About()
        {
            return View();
        }

        public ActionResult ChartData()
        {
            List<DailySummary> dailySummaries = DailySummary.GetDailySummary(180);

            List<ProjectionData> grouped = (from p in dailySummaries
                                            group p by new { month = p.Date.Month, year = p.Date.Year } into d
                                            select new ProjectionData { Month = string.Format("{0}/{1}", d.Key.month, d.Key.year), Amount = d.Sum(s => s.EndingDayBalance) }).ToList();

            return Json(grouped);
        }

    }
}
