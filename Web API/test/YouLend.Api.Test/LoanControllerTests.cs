using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using YouLend.Abstractions.Data;
using YouLend.Abstractions.Services;
using YouLend.Api.Controllers;
using YouLend.Models;
using YouLend.Services;

namespace YouLend.Api.Test
{
    public class LoanControllerTests
    {
        private readonly Mock<ILoanRepository> _repo = new Mock<ILoanRepository>();
        private readonly ILoanManager _service;
        private readonly LoanController _controller;
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

        public LoanControllerTests()
        {
            var addedLoan = new Loan
            {
                Id = 2,
                BorrowerName = "Test New Loan",
                FundingAmount = 500,
                RepaymentAmount = 600,
                Deleted = false
            };


            var deletedLoan = new Loan
            {
                Id = 1,
                BorrowerName = "Fizz",
                FundingAmount = 1000,
                RepaymentAmount = 1200,
                Deleted = true

            };

            _repo.Setup(x => x.GetAll()).Returns(_loans.ToList());

            _repo.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns((int id) => _loans.Find(s => s.Id == id));

            _repo.Setup(x => x.AddNewLoan(It.IsAny<Loan>()))
                .Callback((Loan loan) => _loans.Add(loan))
                .Returns(Task.FromResult(addedLoan));

            _repo.Setup(x => x.DeleteLoan(It.IsAny<int>()))
                .Callback((int id) => _loans[_loans.FindIndex(x => x.Id == id)].Deleted = true)
                .Returns(Task.FromResult(deletedLoan));

            _service = new LoanManager(_repo.Object);
            _controller = new LoanController(_service);
        }

        [Fact()]
        public void GetAllLoans()
        {
            var loans = _controller.GetAll();

            _repo.Verify(x => x.GetAll(), Times.Once);
            Assert.Equal(1, loans.Count);
        }

        [Fact()]
        public void GetById()
        {
            var testId = 1;

            var loan = _controller.Get(testId);

            _repo.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
            Assert.Equal(testId, loan.Id);

        }

        [Fact()]
        public async Task CreateNewLoan()
        {
            var newLoan = new NewLoan
            {
                BorrowerName = "Test New Loan",
                FundingAmount = 500,
                RepaymentAmount = 600
            };

            var result = await _controller.Create(newLoan) as ObjectResult;
            var actualResult = (Loan)result.Value;

            _repo.Verify(x => x.AddNewLoan(It.IsAny<Loan>()), Times.Once);

            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(newLoan.BorrowerName, actualResult.BorrowerName);
        }

        [Fact()]
        public async Task DeleteLoan()
        {
            var testId = 1;

            var deletedLoan = new Loan
            {
                Id = 1,
                BorrowerName = "Fizz",
                FundingAmount = 1000,
                RepaymentAmount = 1200,
                Deleted = true

            };

            var result = await _controller.Delete(testId) as ObjectResult;
            var actualResult = (Loan)result.Value;

            _repo.Verify(x => x.DeleteLoan(It.IsAny<int>()), Times.Once);

            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(deletedLoan.BorrowerName, actualResult.BorrowerName);
            Assert.True(actualResult.Deleted);
        }
    }
}
