using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouLend.Abstractions.Services;
using YouLend.Models;

namespace YouLend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ILoanManager _loanManager;

        public LoanController(ILoanManager loanManager)
        {
            _loanManager = loanManager;
        }

        [HttpGet]
        public IList<Loan> GetAll()
        {
            return _loanManager.GetAll();
        }

        [HttpGet("{id}")]
        public Loan Get(int id) => _loanManager.GetById(id);

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NewLoan newLoan)
        {
            var loan = await _loanManager.AddNewLoan(newLoan);

            if (loan == null)
            {
                return BadRequest();
            }
            return Ok(loan);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var loan = await _loanManager.DeleteLoan(id);

            if (loan == null)
            {
                return BadRequest();
            }

            return Ok(loan);
        }
    }
}
