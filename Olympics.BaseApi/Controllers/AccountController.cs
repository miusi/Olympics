using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Olympics.Entities;
using Olympics.Interfaces;
using Olypmics.Common;
using Orleans;

namespace Olympics.BaseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IClusterClient client;

        public AccountController(IClusterClient client)
        {
            this.client = client;
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            IUserBiz userBiz = client.GetGrain<IUserBiz>(username);
            BaseResult baseResult = await userBiz.Login(username, password);
            return Ok(baseResult);
        }

        [HttpGet("tranfer")]
        public async Task<IActionResult> Tranfer(int fromAccount, int toAccount, uint amount)
        {
            IATMGrain atm = client.GetGrain<IATMGrain>(0);
            Account from = Account.Accounts[fromAccount];
            Account to = Account.Accounts[toAccount];
            await atm.Transfer(from.Id, to.Id, amount);
            uint fromBalance = await client.GetGrain<IAccountGrain>(from.Id).GetBalance();
            uint toBalance = await client.GetGrain<IAccountGrain>(to.Id).GetBalance();
            BaseResult baseResult = new BaseResult();
            baseResult.ResultValue = $"账户{from.Name} 转账 {amount} 到 {to.Name},最新余额：{from.Name}:{fromBalance},{to.Name}:{toBalance}";
            return Ok(baseResult.ResultValue);
        }

        [HttpGet("getbalance")]
        public async Task<IActionResult> Balance(int account)
        {
            Account from = Account.Accounts[account];
            var accountGrain = client.GetGrain<IAccountGrain>(from.Id);
            uint balance = await accountGrain.GetBalance();
            return Ok($"{from.Name}余额:{balance}");
        }
    }
}