using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoneyTrakr.Models
{
    public class DailySummary
    {
        public DateTime Date { get; set; }
        public decimal EndingDayBalance { get; set; }
        public List<TransactionItem> TransactionItems { get; set; }

        public DailySummary()
        {
            TransactionItems = new List<TransactionItem>();
        }



        


        public static List<DailySummary> GetDailySummary(int daysInFuture){

           

            MoneyTrakrEntities db = new MoneyTrakrEntities();
            DateTime startDate = db.Transactions.Where(t => t.StartTransaction).FirstOrDefault().CreatedDate.Date;
            var recurrenceDates = new Dictionary<DateTime,List<TransactionItem>>();
            DateTime today = DateTime.Now.Date;

            DateTime maxDate = DateTime.Now.AddDays(daysInFuture);
            foreach (var recur in db.Recurrings)
            {
                DateTime now = today;
                int daysBetweenPayment = 0;
                DateTime recurringDate = recur.StartDate.Date;

                if (recur.RecurBiWeekly)
                {
                    daysBetweenPayment = 14;
                }
                else if (recur.RecurWeekly)
                {
                    daysBetweenPayment = 7;
                }

                if (recur.RecurMontly)
                {
                    
                    while (now <= maxDate)
                    {

                        if (!recurrenceDates.ContainsKey(recurringDate))
                        {
                            recurrenceDates.Add(recurringDate, new List<TransactionItem>());    
                        }
                        recurrenceDates[recurringDate].Add(new TransactionItem(recur) { CreatedDate = recurringDate });
                        recurringDate = recurringDate.AddMonths(1);
                        now = now.AddMonths(1);
                    }
                }
                else
                {
                    while (now <= maxDate)
                    {
                        if (!recurrenceDates.ContainsKey(recurringDate))
                        {
                            recurrenceDates.Add(recurringDate, new List<TransactionItem>());
                        }
                        recurrenceDates[recurringDate].Add(new TransactionItem(recur) { CreatedDate = recurringDate });
                        recurringDate = recurringDate.AddDays(daysBetweenPayment);
                        now = now.AddDays(daysBetweenPayment);
                    }
                }                
            }

            foreach(var t in db.Transactions){

                if(!recurrenceDates.ContainsKey(t.CreatedDate.Date)){
                    recurrenceDates.Add(t.CreatedDate.Date,new List<TransactionItem>());
                }

                recurrenceDates[t.CreatedDate.Date].Add(new TransactionItem(t));
            }

            List<DailySummary> dailySummaries = new List<DailySummary>();            
            for (int i = 0; i < daysInFuture; i++)
            {                
                DailySummary ds = new DailySummary();
                var currentDay = startDate.AddDays(i);
                ds.Date = currentDay;
                if (recurrenceDates.ContainsKey(currentDay))
                {
                    ds.TransactionItems.AddRange(recurrenceDates[currentDay].OrderByDescending(x => x.IsStartAmount));                 
                }
                decimal todaysMoney = ds.TransactionItems.Sum(t=>t.Amount);
                if(i>0){
                    ds.EndingDayBalance = todaysMoney + dailySummaries[i-1].EndingDayBalance;
                }else{
                    ds.EndingDayBalance = todaysMoney;
                }
                dailySummaries.Add(ds);
            }

            return dailySummaries.OrderBy(x => x.Date).ToList();
        
        }
       
    }
}