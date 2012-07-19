using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoneyTrakr.Web.Models
{
    public class Summary
    {
        public decimal CurrentBalance { get; set; }
        public int ProjectionMonths { get; set; }
        public decimal ProjectionBalance { get; set; }
    }
}