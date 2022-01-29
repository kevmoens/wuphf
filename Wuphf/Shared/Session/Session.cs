using System;
using System.ComponentModel.DataAnnotations;

namespace Wuphf.Shared.Session
{
    public class Session
    {
        public Session()
        {
        }
        public string UserName { get; set; }

        [Key]
        public string Token { get; set; }
    }
}
