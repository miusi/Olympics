using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Olypmics.Common;
using Orleans;

namespace Olympics.Interfaces
{
    public interface IUserBiz:IGrainWithStringKey
    {
        Task<BaseResult> Login(string username, string password);
    }
}
