
using System;
using System.Collections.Generic;
using System.Text;
using Orleans;
using Olympics.Interfaces;
using System.Threading.Tasks;
using Olympics.Entities;
using Olypmics.Common;

namespace Olympics.Grains
{
    public class UserBiz : Grain, IUserBiz
    {
        public Task<BaseResult> Login(string username, string password)
        {
            BaseUserEntity user= BaseUserEntity.Users.Find(_ => _.UserName == username);
            BaseResult baseResult = new BaseResult();
            if (user != null)
            {
                if(user.Password == password){
                    baseResult.Status = true;
                    baseResult.Message = "登录成功";
                }
                else
                {
                    baseResult.Status = false; 
                    baseResult.Message = "用户密码不正确";
                }
            }
            else
            {
                baseResult.Status = false; 
                baseResult.Message = "用户密码不正确";
            }
            //TODO 登录验证
            return Task.FromResult(baseResult);
        }

        public override Task OnActivateAsync()
        {
            return base.OnActivateAsync();
        }
    }
}
