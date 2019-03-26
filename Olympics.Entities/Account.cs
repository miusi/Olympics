using System;
using System.Collections.Generic;
using System.Text;

namespace Olympics.Entities
{
    /// <summary>
    /// 账户
    /// </summary>
    public class Account
    {
        public static readonly  Account[] Accounts =new Account[3];
        static Account()
        {
            Accounts[0] =new Account() {
                Id=Guid.NewGuid(),
                Name="zhangsan"
            };
            Accounts[1] = new Account()
            {
                Id = Guid.NewGuid(),
                Name = "lisi"
            };
            Accounts[2] = new Account()
            {
                Id = Guid.NewGuid(),
                Name = "wangwu"
            };
        }


        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
