using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace Olympics.Interfaces
{
    public interface IUserBiz:IGrainWithStringKey
    {
        Task<bool> Login(string username, string password);
    }
}
