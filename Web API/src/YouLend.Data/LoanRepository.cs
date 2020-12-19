using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouLend.Abstractions.Data;
using YouLend.Models;

namespace YouLend.Data
{
    public class LoanRepository : ILoanRepository
    {
        private readonly AppDbContext _ctx;

        public LoanRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public IList<Loan> GetAll()
        {
            return _ctx.Loans
                .Where(x => !x.Deleted)
                .ToList();
        }

        public Loan GetById(int id)
        {
            return _ctx.Loans
                .Where(x => !x.Deleted)
                .FirstOrDefault(x => x.Id.Equals(id));
        }

        public async Task<Loan> AddNewLoan(Loan loan)
        {
            await _ctx.AddAsync(loan);
            _ctx.SaveChanges();

            return loan;
        }

        public async Task<Loan> DeleteLoan(int id)
        {
            var loan = _ctx.Loans.FirstOrDefault(x => x.Id.Equals(id));
            loan.Deleted = true;

            await _ctx.SaveChangesAsync();

            return loan;
        }
    }
}
