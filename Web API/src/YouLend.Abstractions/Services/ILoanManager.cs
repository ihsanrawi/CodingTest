using System.Collections.Generic;
using System.Threading.Tasks;
using YouLend.Models;

namespace YouLend.Abstractions.Services
{
    public interface ILoanManager
    {
        Task<Loan> AddNewLoan(NewLoan newloan);
        Task<Loan> DeleteLoan(int id);
        IList<Loan> GetAll();
        Loan GetById(int id);
    }
}