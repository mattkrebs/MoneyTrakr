using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoneyTrakr.Framework;

namespace MoneyTrakr.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            int sleep = 10000;
            while (true)
            {
                
                using (MoneyTrakrEntities db = new MoneyTrakrEntities())
                {
                    
               
                    List<int> ids = (from a in db.Users
                               orderby a.AccountID
                               select a.AccountID).Distinct().ToList();
                    sleep = (10000 / ids.Count());
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



                    System.Threading.Thread.Sleep(sleep);


                    
                }

                

            }
        }
    }
}
