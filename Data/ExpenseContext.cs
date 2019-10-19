using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ExpenseApi.Models;

    public class ExpenseContext : DbContext
    {
        public ExpenseContext (DbContextOptions<ExpenseContext> options)
            : base(options)
        {
        }

        public DbSet<ExpenseApi.Models.Expense> Expenses { get; set; }
    }
