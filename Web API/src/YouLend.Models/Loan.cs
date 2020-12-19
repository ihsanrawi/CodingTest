using System;

namespace YouLend.Models
{
    public class Loan
    {
        public int Id { get; set; }
        public string BorrowerName { get; set; }
        public decimal RepaymentAmount { get; set; }
        public decimal FundingAmount { get; set; }
        public bool Deleted { get; set; }
    }
}
