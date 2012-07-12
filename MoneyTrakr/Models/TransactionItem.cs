using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoneyTrakr.Models
{
    public class TransactionItem
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal RunningTotal { get; set; }


        public TransactionItem(Transaction tran)
        {
            Amount = tran.Amount;
            Description = tran.Description;
            CreatedDate = tran.CreatedDate;
        }

        public TransactionItem(Recurring recur)
        {
            Amount = recur.Amount;
            Description = recur.Description;
            CreatedDate = recur.StartDate;
        }


        public static List<TransactionItem> CalculateTotals(List<TransactionItem> items)
        {
            List<TransactionItem> runningList = new List<TransactionItem>();
            decimal total =0;
            foreach (TransactionItem item in items.OrderBy(o=>o.CreatedDate))
            {
                total = total + item.Amount;
                item.RunningTotal  =total;
                runningList.Add(item);
            }
            return runningList;
        }


        public static List<TransactionItem> GetAllTransactionsItems()
        {
            MoneyTrakrEntities db = new MoneyTrakrEntities();
            List<TransactionItem> AllLineItems = new List<TransactionItem>();
           
            DateTime startDate = db.Transactions.OrderBy(x => x.CreatedDate).FirstOrDefault().CreatedDate;

            foreach (Transaction trans in db.Transactions.ToList())
            {
                AllLineItems.Add(new TransactionItem(trans));
            }
            foreach (RecurringItem recurs in RecurringItem.GetAllRecurringItems(startDate))
            {
               // AllLineItems.Add(new TransactionItem(recurs));
            }

            return TransactionItem.CalculateTotals(AllLineItems.Where(y => y.CreatedDate >= startDate).OrderBy(x => x.CreatedDate).ToList());
            

        }

        public static List<ProjectionData> GetGroupedProjectionData(int futureDays)
        {
            MoneyTrakrEntities db = new MoneyTrakrEntities();
            List<TransactionItem> AllLineItems = new List<TransactionItem>();

            DateTime startDate = db.Transactions.OrderBy(x => x.CreatedDate).FirstOrDefault().CreatedDate;

            foreach (Transaction trans in db.Transactions.ToList())
            {
                AllLineItems.Add(new TransactionItem(trans));
            }
            foreach (RecurringItem recurs in RecurringItem.GetAllRecurringItems(startDate, futureDays))
            {
               // AllLineItems.Add(new TransactionItem(recurs));
            }
            List<TransactionItem> sortedItems = TransactionItem.CalculateTotals(AllLineItems.Where(y => y.CreatedDate >= startDate).OrderBy(x => x.CreatedDate).ToList());


            List<ProjectionData> grouped = (from p in sortedItems
                          group p by new { month = p.CreatedDate.Month, year = p.CreatedDate.Year } into d
                          select new ProjectionData { Month = string.Format("{0}/{1}", d.Key.month, d.Key.year), Amount = d.Sum(s => s.RunningTotal) }).ToList();

            return grouped;


        }

        public static List<TransactionItem> GetProjectionData(int futureDays)
        {
            MoneyTrakrEntities db = new MoneyTrakrEntities();
            List<TransactionItem> AllLineItems = new List<TransactionItem>();

            DateTime startDate = db.Transactions.OrderBy(x => x.CreatedDate).FirstOrDefault().CreatedDate;

            foreach (Transaction trans in db.Transactions.ToList())
            {
                AllLineItems.Add(new TransactionItem(trans));
            }
            foreach (RecurringItem recurs in RecurringItem.GetAllRecurringItems(startDate, futureDays))
            {
               // AllLineItems.Add(new TransactionItem(recurs));
            }
            List<TransactionItem> sortedItems = TransactionItem.CalculateTotals(AllLineItems.Where(y => y.CreatedDate >= startDate).OrderBy(x => x.CreatedDate).ToList());



            return sortedItems;


        }
    }
}