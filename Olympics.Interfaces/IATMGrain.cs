using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Olympics.Interfaces
{
    public interface IATMGrain:IGrainWithIntegerKey
    {
        [Transaction(TransactionOption.Create)]
        Task Transfer(Guid fromAccount, Guid toAccount, uint amountToTransfer);
    }
}
