using System;
using System.Collections.Generic;

namespace Frontend.Models
{
    public class User : BaseEntity
    {
        public int UserId { get; set; }
        public string Nickname { get; set; }
        public string Pass { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ImageColumne { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}