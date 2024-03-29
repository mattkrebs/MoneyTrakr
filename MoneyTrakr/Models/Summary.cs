﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoneyTrakr.Framework;
namespace MoneyTrakr.Web.Models
{
    public class Summary
    {
        public decimal CurrentBalance { get; set; }
        public int ProjectionMonths { get; set; }
        public decimal ProjectionBalance { get; set; }
        public List<Transaction> Transactions { get; set; }
        public List<RecurringItem> RecurringItems { get; set; }

        public static Summary GetSummary()
        {
            MoneyTrakrEntities db = new MoneyTrakrEntities();
            Summary summary = new Summary();
            summary.CurrentBalance = 0;
            summary.ProjectionMonths = 1;
            summary.Transactions = new List<Transaction>();
           
            foreach (var transaction in db.Transactions.OrderBy(x => x.StartTransaction).OrderBy(y=>y.CreatedDate))
            {
                summary.CurrentBalance = summary.CurrentBalance + transaction.Amount;
                summary.Transactions.Add(transaction);
            }

            summary.RecurringItems = RecurringItem.GetAllRecurringItems(DateTime.Now.AddDays(1), summary.ProjectionMonths);

            foreach (RecurringItem recurs in summary.RecurringItems)
            {
                summary.ProjectionBalance = summary.ProjectionBalance + recurs.Amount;
              
            }
            summary.ProjectionBalance = summary.ProjectionBalance + summary.CurrentBalance;
            
            return summary;
        }
    }
}