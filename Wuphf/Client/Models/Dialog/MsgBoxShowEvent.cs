using System;
using System.Threading.Tasks;

namespace Wuphf.Client.Models.Dialog
{
    public class MsgBoxShowEvent
    {
        public MsgBoxShowEvent()
        {
        }
        public string Message { get; set; }
        public string Title { get; set; }
        public DialogButtons Buttons { get; set; }
        public TaskCompletionSource<string> CompletionSource { get; set; }
    }
}
