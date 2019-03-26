using Olympics.Interfaces;
using Orleans;
using Orleans.CodeGeneration;
using Orleans.Transactions.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

[assembly: GenerateSerializer(typeof(Olympics.Grains.Balance))]
namespace Olympics.Grains
{
    public class Balance
    {
        public uint Value { get; set; } = 1000;
    }

    public class AccountGrain : Grain, IAccountGrain
    {
        private readonly ITransactionalState<Balance> balance;

        public AccountGrain([TransactionalState("balance")] ITransactionalState<Balance> balance)
        {
            this.balance = balance ?? throw new ArgumentNullException(nameof(balance));
        }

        public Task Deposit(uint amount)
        {
            return this.balance.PerformUpdate(x => x.Value += amount);
        }

        public Task<uint> GetBalance()
        {
            return this.balance.PerformRead(x => x.Value);
        }

        public Task Withdraw(uint amount)
        {
            return this.balance.PerformUpdate(x => x.Value -= amount);
        }
    }
}
