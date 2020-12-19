using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YouLend.Models;

namespace YouLend.Data
{
    public class Seed
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var ctx = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                if (ctx.Loans.Any())
                {
                    return;   // Data was already seeded
                }

                ctx.Loans.AddRange(
                    new Loan
                    {
                        Id = 1,
                        BorrowerName = "Fizz",
                        FundingAmount = 50000,
                        RepaymentAmount = 60000,
                        Deleted = false
                    },
                    new Loan
                    {
                        Id = 2,
                        BorrowerName = "Buzz",
                        FundingAmount = 12500,
                        RepaymentAmount = 15000,
                        Deleted = false
                    });

                ctx.SaveChanges();
            }
        }
    }
}
