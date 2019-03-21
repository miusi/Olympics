using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Olympics.Interfaces;
using Orleans;

namespace Olympics.Base.Controllers
{
    [ApiController]
    public class LoginController : Controller
    {
        private IClusterClient client;

        public LoginController(IClusterClient client)
        {
            this.client = client;
        }
          
        public ActionResult Login(string username,string password)
        {
            var userBiz= client.GetGrain<IUserBiz>(username);
            userBiz.Login(username, password);
            return Ok();
        }
         
    }
}