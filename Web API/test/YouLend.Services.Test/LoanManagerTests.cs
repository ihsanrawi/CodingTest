using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using YouLend.Abstractions.Data;
using YouLend.Abstractions.Services;
using YouLend.Models;

namespace YouLend.Services.Test
{
    public class LoanManagerTests
    {
        private readonly Mock<ILoanRepository> _repo = new Mock<ILoanRepository>();
        private readonly ILoanManager _service;
        private readonly List<Loan> _loans = new List<Loan>
        {
            new Loan
            {
                Id = 1,
                BorrowerName = "Fizz",
                FundingAmount = 1000,
                RepaymentAmount = 1200,
                Deleted = false
            }
        };

        public LoanManagerTests()
        {
            _service = new LoanManager(_repo.Object);
        }

        [Fact]
        public void GetAllLoan()
        {
            _repo.Setup(x => x.GetAll()).Returns(_loans.ToList());

            var loans = _service.GetAll();

            _repo.Verify(x => x.GetAll(), Times.Once);
            Assert.Equal(1, loans.Count);
        }

        [Fact]
        public void GetLoanById()
        {
            var testId = 1;

            _repo.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns((int id) => _loans.Find(s => s.Id == id));

            var loan = _service.GetById(testId);

            _repo.Verify(x => x.GetById(testId), Times.Once);
            Assert.Equal(testId, loan.Id);
            Assert.Equal("Fizz", loan.BorrowerName);
        }

        [Fact]
        public void AddNewLoan()
        {
            // Arrange
            var newLoan = new NewLoan
            {
                BorrowerName = "TestNew Loan",
                FundingAmount = 500,
                RepaymentAmount = 600
            };

            var addedLoan = new Loan
            {
                Id = 2,
                BorrowerName = "TestNew Loan",
                FundingAmount = 500,
                RepaymentAmount = 600,
                Deleted = false
            };

            _repo.Setup(x => x.AddNewLoan(It.IsAny<Loan>()))
                .Callback((Loan loan) => _loans.Add(loan))
                .Returns(Task.FromResult(addedLoan));

            var loan = _service.AddNewLoan(newLoan).Result;

            _repo.Verify(x => x.AddNewLoan(It.IsAny<Loan>()), Times.Once);

            Assert.Equal(newLoan.BorrowerName, loan.BorrowerName);
            Assert.Equal(2, loan.Id);
        }

        [Fact]
        public void DeleteLoan()
        {
            var testId = 1;

            var result = new Loan
            {
                Id = 1,
                BorrowerName = "Fizz",
                FundingAmount = 1000,
                RepaymentAmount = 1200,
                Deleted = true

            };

            _repo.Setup(x => x.DeleteLoan(It.IsAny<int>()))
                .Callback((int id) => _loans[_loans.FindIndex(x => x.Id == id)].Deleted = true)
                .Returns(Task.FromResult(result));

            var deletedLoan = _service.DeleteLoan(testId).Result;

            _repo.Verify(x => x.DeleteLoan(testId), Times.Once);

            Assert.Equal(testId, deletedLoan.Id);
            Assert.True(deletedLoan.Deleted);
        }
    }
}
