using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YouLend.Abstractions.Data;
using YouLend.Abstractions.Services;
using YouLend.Models;

namespace YouLend.Services
{
    public class LoanManager : ILoanManager
    {
        private readonly ILoanRepository _loanRepository;

        public LoanManager(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        public async Task<Loan> AddNewLoan(NewLoan newloan)
        {
            newloan.RepaymentAmount = Math.Round(newloan.FundingAmount * (decimal)1.2, 3);

            var loan = new Loan
            {
                BorrowerName = newloan.BorrowerName,
                FundingAmount = newloan.FundingAmount,
                RepaymentAmount = newloan.RepaymentAmount,
                Deleted = false
            };

            return await _loanRepository.AddNewLoan(loan);
        }

        public async Task<Loan> DeleteLoan(int id)
        {
            return await _loanRepository.DeleteLoan(id);
        }

        public IList<Loan> GetAll()
        {
            return _loanRepository.GetAll();
        }

        public Loan GetById(int id)
        {
            return _loanRepository.GetById(id);
        }
    }
}
