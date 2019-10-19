using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExpenseApi.Models;
using ExpenseApi.BL;
using Microsoft.Extensions.Configuration;

namespace ExpenseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly ExpenseContext _context;
        private readonly IConfiguration _configuration;
        public ExpenseController(IConfiguration configuration, ExpenseContext context)
        {
            _configuration = configuration;
            _context = context;
             if (_context.Expenses.Count() == 0)
            {
                // Create some new Expenses if collection is empty,
                _context.Expenses.Add(new Expense { 
                   ExpenseDate = DateTime.UtcNow,
                   Amount = 43.5M,
                   Reason ="Rail ticket to T.A."
                 });
                _context.Expenses.Add(new Expense { 
                   ExpenseDate = DateTime.UtcNow,
                   Amount = 51.5M,
                   Reason ="Rail ticket to BeerSheva"
                 });
                _context.SaveChanges();
            }
        }

        // GET: api/Expense
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses()
        {
            return await _context.Expenses.ToListAsync();
        }

        // GET: api/Expense/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Expense>> GetExpense(long id)
        {
            var expense = await _context.Expenses.FindAsync(id);

            if (expense == null)
            {
                return NotFound();
            }

            return expense;
        }

        // POST: api/Expense
        [HttpPost]
        public async Task<ActionResult<Expense>> AddExpense(Expense expense)
        {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetExpense), new { id = expense.Id }, expense);
        }

        [HttpPost] 
        [Route("[action]")]
        public async Task<ActionResult<Expense>> AddEuroToPoundExpense(DateTime timeStamp, string amount, string reason)
        {
            if(string.IsNullOrWhiteSpace(amount))
            {
              return BadRequest();
            }
            CurrencyConverter converter = new CurrencyConverter(_configuration["ConversionApi"]);
            var reply = await converter.EuroToPound(amount);
            if(!reply.HasValue)
            {
                return BadRequest();
            }
            Expense expense = new Expense();
            expense.ExpenseDate = timeStamp;
            expense.Amount = reply.Value;
            expense.Reason = reason;
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetExpense), new { id = expense.Id }, expense);
        }
      
    }
}
