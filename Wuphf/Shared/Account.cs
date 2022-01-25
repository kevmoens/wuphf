using System;
using System.ComponentModel.DataAnnotations;
namespace Wuphf.Shared
{

    public class Account
    {
        public Account()
        {
        }
        [Key]
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
