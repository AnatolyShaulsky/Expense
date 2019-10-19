using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseApi.Models
{
    public class Expense   
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ExpenseDate { get; set; } = DateTime.UtcNow;

        [Required]
        [DataType(DataType.Currency)]
        public decimal  Amount { get; set; }

        [DataType(DataType.Text), MaxLength(1000)]
        public string  Reason{ get; set; }

        public decimal VAT => 20 * Amount / 100;
    }
}