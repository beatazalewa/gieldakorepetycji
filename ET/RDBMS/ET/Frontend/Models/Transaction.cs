using System;
using System.ComponentModel.DataAnnotations;

namespace Frontend.Models
{
    public class Transaction : BaseEntity
    {
        public int TransactionId { get; set; }

        [Required]
        [RegularExpression(@"^\d+\.\d{0,2}PLN")]
        public double Amount { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int UserId { get; set; }
        public User AccountHolder { get; set; }

    }
}