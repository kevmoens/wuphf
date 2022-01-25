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
        WuphfRepository repository;
        public AccountController(WuphfRepository repository)
        {
            this.repository = repository;
        }
        // GET: values
        [HttpGet]
        public IEnumerable<Account> Get()
        {
            return repository.Accounts.ToList();
        }

        // GET values/5
        [HttpGet("{userName}")]
        public Account Get(string userName)
        {
            var result = repository.Accounts.AsEnumerable().First((a) => a.UserName.ToUpperInvariant() == userName.ToUpperInvariant());
            return result;
        }

        // POST values (Add)
        [HttpPost]
        public bool Post(Account value)
        {
            repository.Accounts.Add(value);
            return true;
        }

        // POST values
        [HttpPost("{username}")]
        public string Post(string username, string password)
        {
            var account = repository.Accounts.FirstOrDefault((a) => a.UserName.ToUpperInvariant() == username.ToUpperInvariant());
            if (account == null)
            {
                return "InvalidAccount";
            }
            if (account.Password != password)
            {
                return "InvalidPassword";
            }

            return "";
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
            var remAccts = repository.Accounts.Where((a) => a.UserName.ToUpperInvariant() == userName.ToUpperInvariant());
            foreach (var acct in remAccts)
            {
                repository.Accounts.Remove(acct);
            }
        }
    }
}
