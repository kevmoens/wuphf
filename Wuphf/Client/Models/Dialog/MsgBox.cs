using System;
using System.Threading;
using System.Threading.Tasks;
using Wuphf.Shared.Dialog;

namespace Wuphf.Client.Models.Dialog
{
    public class MsgBox : IMsgBox
    {
        private static SemaphoreSlim messageLock;
        private static TaskCompletionSource<string> completionSource;
        public MsgBox()
        {
            messageLock = new SemaphoreSlim(1, 1);
        }
        public async Task<string> Show(string message, string title, DialogButtons buttons)
        {
            await messageLock.WaitAsync();
            completionSource = new TaskCompletionSource<string>();

            PubSub.Hub.Default.Publish<MsgBoxShowEvent>(new MsgBoxShowEvent() { Message = message, Title = title, Buttons = buttons, CompletionSource = completionSource });
            var result = await completionSource.Task;
            messageLock.Release();
            return result;
        }
    }
}
