using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}