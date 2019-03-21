using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Olympics.Interfaces;
using Orleans;

namespace Olympics.BaseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IClusterClient client;

        public LoginController(IClusterClient client)
        {
            this.client = client;
        }

        [HttpGet]
        public ActionResult Login(string username, string password)
        {
            var userBiz = client.GetGrain<IUserBiz>(username);
            userBiz.Login(username, password);
            return Ok();
        }
    }
}