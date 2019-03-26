using Olympics.Interfaces;
using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Olympics.Grains
{
    public class ATMGrain : Grain, IATMGrain
    {
        public Task Transfer(Guid fromAccount, Guid toAccount, uint amountToTransfer)
        {
            return Task.WhenAll(
                this.GrainFactory.GetGrain<IAccountGrain>(fromAccount).Withdraw(amountToTransfer),
                this.GrainFactory.GetGrain<IAccountGrain>(toAccount).Deposit(amountToTransfer)
                );
        }
    }
}
