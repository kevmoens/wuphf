using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wuphf.Server.Repository;
using Wuphf.Shared;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Wuphf.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        WuphfRepository db = new WuphfRepository();
        
        // GET: values
        [HttpGet]
        public IEnumerable<Account> Get()
        {
            return db.Accounts.ToList();
        }

        // GET values/5
        [HttpGet("{userName}")]
        public Account Get(string userName)
        {
            return db.Accounts.FirstOrDefault((a) => a.UserName.ToUpperInvariant() == userName.ToUpperInvariant());
        }

        // POST values
        [HttpPost]
        public bool Post(Account value)
        {
            db.Accounts.Add(value);
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
            var remAccts = db.Accounts.Where((a) => a.UserName.ToUpperInvariant() == userName.ToUpperInvariant());
            foreach (var acct in remAccts)
            {
                db.Accounts.Remove(acct);
            }
        }
    }
}
