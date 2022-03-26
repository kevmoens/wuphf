using System;
using System.Threading.Tasks;

namespace Wuphf.Shared.Dialog
{
    public interface IMsgBox
    {
        Task<string> Show(string message, string title, DialogButtons buttons);
    }
}
