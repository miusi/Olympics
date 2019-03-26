using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Olympics.Interfaces
{
    public interface IAccountGrain : IGrainWithGuidKey
    {
        [Transaction(TransactionOption.Join)]
        Task Withdraw(uint amount);

        [Transaction(TransactionOption.Join)]
        Task Deposit(uint amount);

        [Transaction(TransactionOption.CreateOrJoin)]
        Task<uint> GetBalance();
    }
}
