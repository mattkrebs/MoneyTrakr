using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoneyTrakr.Models
{
    public class RecurringItem
    {
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public bool RecurWeekly { get; set; }
        public bool RecurBiWeekly { get; set; }
        public bool RecurMonthly { get; set; }
        public decimal Amount { get; set; }

        public static List<RecurringItem> GetAllRecurringItems(DateTime StartDate)
        {
            return GetAllRecurringItems(StartDate, 0);
        }


        public static List<RecurringItem> GetAllRecurringItems(DateTime StartDate, int futureMonths)
        {
            List<RecurringItem> items = new List<RecurringItem>();
            MoneyTrakrEntities db = new MoneyTrakrEntities();
            foreach (Recurring recur in db.Recurrings.Where(x => x.Automatic).ToList())
            {
                int days = 0;
                int months = 0;
                if (recur.RecurBiWeekly)
                {
                    days = 14;
                }
                else if (recur.RecurWeekly)
                {
                    days = 7;
                }

                int daysTally = 0;

                foreach (DateTime day in EachDay(recur.StartDate, DateTime.Now.AddMonths(futureMonths)))
                {
                    if (day.Date >= StartDate.Date)
                    {
                        if (days != 0)
                        {

                            items.Add(new RecurringItem()
                            {
                                Amount = recur.Amount,
                                Description = recur.Description,
                                StartDate = recur.StartDate.AddDays(daysTally)
                            });


                            daysTally = daysTally + days;
                        }
                        else
                        {
                            if (recur.StartDate.AddMonths(months).Date == day.Date)
                            {

                                items.Add(new RecurringItem()
                                {
                                    Amount = recur.Amount,
                                    Description = recur.Description,
                                    StartDate = recur.StartDate.AddMonths(months)
                                });
                                months++;
                            }
                        }
                    }


                }

            }

            return items;


        }


        public static IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }


    }






}