using System;
using System.Collections.Generic;
using System.Text;

namespace YouLend.Models
{
    public class NewLoan
    {
        public string BorrowerName { get; set; }
        public decimal RepaymentAmount { get; set; }
        public decimal FundingAmount { get; set; }
    }
}
