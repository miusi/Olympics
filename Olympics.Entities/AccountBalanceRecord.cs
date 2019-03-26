using System;
using System.Collections.Generic;
using System.Text;

namespace Olympics.Entities
{
    /// <summary>
    /// 结算记录表
    /// </summary>
    public class AccountBalanceRecord
    {
        public int Id { get; set; }

        public int FromAccountId { get; set; }

        public int ToAccountId { get; set; }
        
        public string AccountType { get; set; }

        public decimal Amount { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
