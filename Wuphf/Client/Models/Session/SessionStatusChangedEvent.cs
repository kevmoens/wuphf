using System;
namespace Wuphf.Client.Models.Session
{
    public class SessionStatusChangedEvent
    {
        public SessionStatusChangedEvent()
        {
        }
        public string Token { get; set; }
    }
}
