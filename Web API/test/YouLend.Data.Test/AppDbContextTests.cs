using System;
using System.Linq;
using Xunit;
using YouLend.Data.Test.Infrastructure;
using YouLend.Models;

namespace YouLend.Data.Test
{
    public class AppDbContextTests
    {
        [Fact]
        public void SaveLoanToDatabase()
        {
            var loan = new Loan
            {
                BorrowerName = "Test",
                FundingAmount = 1000,
                RepaymentAmount = 1200,
                Deleted = false
            };

            using (var ctx = DbContextFactory.Create(nameof(SaveLoanToDatabase)))
            {
                ctx.Loans.Add(loan);
                ctx.SaveChanges();
            }

            using (var ctx = DbContextFactory.Create(nameof(SaveLoanToDatabase)))
            {
                var savedLoan = ctx.Loans.Single();
                Assert.Equal(loan.BorrowerName, savedLoan.BorrowerName);
                Assert.Equal(loan.FundingAmount, savedLoan.FundingAmount);
                Assert.Equal(loan.RepaymentAmount, savedLoan.RepaymentAmount);
            }
        }
    }
}
