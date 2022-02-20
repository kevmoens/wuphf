using System;
using Xamarin.Forms;

namespace Wuphf.MVVM
{
    public interface INavigationService
    {
        void GoBack();
        void GoForward();
        bool Navigate(NavigableElement view);
        bool Navigate(string uri);
        bool Navigate(Uri uri);
        void Refresh();
    }
}
