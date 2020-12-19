using System;
using System.Linq;
using Xunit;
using YouLend.Data.Test.Infrastructure;
using YouLend.Models;

namespace YouLend.Data.Test
{
    public class LoanRepositoryTests
    {
        [Fact]
        public void AddNewLoan_ReturnCreatedLoan()
        {
            var loan = new Loan
            {
                BorrowerName = "Test",
                FundingAmount = 1000,
                RepaymentAmount = 1200,
                Deleted = false
            };

            using (var ctx = DbContextFactory.Create(nameof(AddNewLoan_ReturnCreatedLoan)))
            {
                var persistance = new LoanRepository(ctx);
                _ = persistance.AddNewLoan(loan);
            }

            using (var ctx = DbContextFactory.Create(nameof(AddNewLoan_ReturnCreatedLoan)))
            {
                var createdLoan = ctx.Loans.Single();
                Assert.Equal(loan.Id, createdLoan.Id);
            }
        }

        [Fact]
        public void DeleteLoan_ReturnDeletedLoan()
        {
            var loans = new[]
            {
                new Loan
                {
                    BorrowerName = "Test",
                    FundingAmount = 1000,
                    RepaymentAmount = 1200,
                    Deleted = false
                },
                new Loan
                {
                    BorrowerName = "Buzz",
                    FundingAmount = 500,
                    RepaymentAmount = 600,
                    Deleted = false
                }
            };

            using (var ctx = DbContextFactory.Create(nameof(DeleteLoan_ReturnDeletedLoan)))
            {
                ctx.Loans.AddRange(loans);

                var persistance = new LoanRepository(ctx);
                _ = persistance.DeleteLoan(1);

                ctx.SaveChanges();
            }

            using (var ctx = DbContextFactory.Create(nameof(DeleteLoan_ReturnDeletedLoan)))
            {
                var deletedLoan = new LoanRepository(ctx).GetById(1);

                Assert.Equal(loans[0].Deleted, deletedLoan.Deleted);
            }
        }

        [Fact]
        public void GetById_ReturnRequestedLoan()
        {

            var loans = new[]
            {
                new Loan
                {
                    BorrowerName = "Fizz",
                    FundingAmount = 2000,
                    RepaymentAmount = 2400,
                    Deleted = false
                },
                new Loan
                {
                    BorrowerName = "Buzz",
                    FundingAmount = 500,
                    RepaymentAmount = 600,
                    Deleted = false
                },
                new Loan
                {
                    BorrowerName = "Test",
                    FundingAmount = 1000,
                    RepaymentAmount = 1200,
                    Deleted = false
                }

            };

            using (var ctx = DbContextFactory.Create(nameof(GetById_ReturnRequestedLoan)))
            {
                ctx.Loans.AddRange(loans);
                ctx.SaveChanges();
            }

            using (var ctx = DbContextFactory.Create(nameof(GetById_ReturnRequestedLoan)))
            {
                var savedLoan = new LoanRepository(ctx).GetById(2);

                Assert.Equal(loans[1].BorrowerName, savedLoan.BorrowerName);
                Assert.Equal(loans[1].FundingAmount, savedLoan.FundingAmount);
                Assert.Equal(loans[1].RepaymentAmount, savedLoan.RepaymentAmount);
            }
        }

        [Fact]
        public void GetAll_ReturnExactNumberOfLoan()
        {

            var loans = new[]
            {
                new Loan
                {
                    BorrowerName = "Fizz",
                    FundingAmount = 2000,
                    RepaymentAmount = 2400,
                    Deleted = false
                },
                new Loan
                {
                    BorrowerName = "Buzz",
                    FundingAmount = 500,
                    RepaymentAmount = 600,
                    Deleted = false
                },
                new Loan
                {
                    BorrowerName = "Test",
                    FundingAmount = 1000,
                    RepaymentAmount = 1200,
                    Deleted = false
                }

            };

            using (var ctx = DbContextFactory.Create(nameof(GetAll_ReturnExactNumberOfLoan)))
            {
                ctx.Loans.AddRange(loans);
                ctx.SaveChanges();
            }

            using (var ctx = DbContextFactory.Create(nameof(GetAll_ReturnExactNumberOfLoan)))
            {
                var savedLoans = new LoanRepository(ctx).GetAll();

                Assert.Equal(3, savedLoans.Count);
            }
        }
    }
}
