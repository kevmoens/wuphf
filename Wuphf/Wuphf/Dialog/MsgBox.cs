using System;
using System.Threading.Tasks;
using Wuphf.Shared.Dialog;
namespace Wuphf.Dialog
{
    public class MsgBox : IMsgBox
    {
        public async Task<string> Show(string message, string title, DialogButtons buttons)
        {
            switch (buttons)
            {
                case DialogButtons.YesNo:
                    if (await App.Current.MainPage.DisplayAlert(title, message, "Yes", "No"))
                    {
                        return "Yes";
                    }
                    return "No";
                case DialogButtons.OK:
                    await App.Current.MainPage.DisplayAlert(title, message, "OK");
                    return "Ok";
            }
            return "";
        }
    }
}
