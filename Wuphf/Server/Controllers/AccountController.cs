using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wuphf.Shared;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Wuphf.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        // GET: values
        [HttpGet]
        public IEnumerable<Account> Get()
        {
            return new Account[] { new Account() { UserName = "Ella", Password = "1234" } };
        }

        // GET values/5
        [HttpGet("{userName}")]
        public Account Get(string userName)
        {
            return new Account() { UserName = "Ella", Password = "1234" };
        }

        // POST values
        [HttpPost]
        public bool Post(Account value)
        {
            return true;
        }

        // PUT values/5
        [HttpPut("{userName}")]
        public void Put(string userName, Account value)
        {
        }

        // DELETE values/5
        [HttpDelete("{userName}")]
        public void Delete(string userName)
        {
        }
    }
}
