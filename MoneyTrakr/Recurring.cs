//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace MoneyTrakr
{
    public partial class Recurring
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public System.DateTime StartDate { get; set; }
        public bool Automatic { get; set; }
        public bool RecurWeekly { get; set; }
        public bool RecurBiWeekly { get; set; }
        public bool RecurMontly { get; set; }
        public decimal Amount { get; set; }
    }
    
}
