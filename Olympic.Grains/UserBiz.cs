
using System;
using System.Collections.Generic;
using System.Text;
using Orleans;
using Olympics.Interfaces;
using System.Threading.Tasks;

namespace Olympics.Grains
{
    public class UserBiz : Grain, IUserBiz
    {
        public Task<bool> Login(string username, string password)
        {
            //TODO 登录验证
            return Task.FromResult(true);
        }
    }
}
