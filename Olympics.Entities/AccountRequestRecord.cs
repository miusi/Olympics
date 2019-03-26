using System;
using System.Collections.Generic;
using System.Text;

namespace Olympics.Entities
{
    public class AccountRequestRecord
    {
        public int Id { get; set; }

        public int FromAccountId { get; set; }

        public int ToAccountId { get; set; }
        
        public string AccountType { get; set; }

        public decimal Amount { get; set; }

        public int Status { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
