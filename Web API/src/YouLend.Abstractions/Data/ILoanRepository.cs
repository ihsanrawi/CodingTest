using System.Collections.Generic;
using System.Threading.Tasks;
using YouLend.Models;

namespace YouLend.Abstractions.Data
{
    public interface ILoanRepository
    {
        Task<Loan> AddNewLoan(Loan loan);
        Task<Loan> DeleteLoan(int id);
        IList<Loan> GetAll();
        Loan GetById(int id);
    }
}