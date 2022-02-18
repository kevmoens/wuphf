using System;
namespace Wuphf.MVVM
{
    public interface INavigationService
    {
        void GoBack();
        void GoForward();
        bool Navigate(IView view);
        bool Navigate(string uri);
        bool Navigate(Uri uri);
        void Refresh();
    }
}
