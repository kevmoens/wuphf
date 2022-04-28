using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wuphf.Server.Repository;
using Wuphf.Shared;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Wuphf.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        IWuphfRepository repository;
        public AccountController(IWuphfRepository repository)
        {
            this.repository = repository;

            //repository.Database.EnsureCreated();
            repository.Database.Migrate();
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
            var result = repository.Accounts.AsEnumerable().FirstOrDefault((a) => a.UserName.ToUpperInvariant() == userName.ToUpperInvariant());
            if (result == null)
            {
                return new Account();
            }
            return result;
        }

        // POST values (Add)
        [HttpPost]
        public string Post(Account value)
        {
            repository.Accounts.Add(value);
            var sess = new Shared.Session.Session() { Token = Guid.NewGuid().ToString(), UserName = value.UserName };
            repository.Sessions.Add(sess);
            repository.SaveChanges();
            return sess.Token;
        }

        // POST values
        [HttpPost("{username}")]
        public string Post(LoginRequest login)
        {
            var account = repository.Accounts.AsEnumerable().FirstOrDefault((a) => a.UserName.ToUpperInvariant() == login.UserName.ToUpperInvariant());
            if (account == null)
            {
                return "Invalid Account";
            }
            if (account.Password != login.Password)
            {
                return "Invalid Password";
            }
            var sessions = repository.Sessions.AsEnumerable()
                .Where((session) => session.UserName.ToUpperInvariant() == login.UserName.ToUpperInvariant()).ToList();
            foreach (var session in sessions)
            {
                repository.Sessions.Remove(session);
            }
            var sess = new Shared.Session.Session() { Token = Guid.NewGuid().ToString(), UserName = login.UserName };
            repository.Sessions.Add(sess);
            repository.SaveChanges();
            return sess.Token;
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
            repository.SaveChanges();
        }
    }
}
