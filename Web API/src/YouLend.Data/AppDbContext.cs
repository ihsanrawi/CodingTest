using Microsoft.EntityFrameworkCore;
using System;
using YouLend.Models;

namespace YouLend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Loan> Loans { get; set; }
    }
}
