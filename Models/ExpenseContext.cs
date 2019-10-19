
using Microsoft.EntityFrameworkCore;

namespace ExpenseApi.Models
{
    ///The database context is the main class that coordinates Entity Framework functionality for a data model
    public class ExpenseContext : DbContext
    {
        public ExpenseContext(DbContextOptions<ExpenseContext> options)
            : base(options)
        {
        }

        public DbSet<Expense> Expenses { get; set; }
    }
}