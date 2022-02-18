using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Wuphf.MVVM
{
    public class XFNavigationService : INavigation
    {
        private INavigation _navigationService;
        public XFNavigationService(NavigableElement element)
        {
            _navigationService = element.Navigation;
        }

        public IReadOnlyList<Page> ModalStack => _navigationService.ModalStack;

        public IReadOnlyList<Page> NavigationStack => _navigationService.NavigationStack;

        public async Task GoBack()
        {
            await _navigationService.PopAsync();
        }

        public void InsertPageBefore(Page page, Page before)
        {
            _navigationService.InsertPageBefore(page, before);
        }

        public async Task Navigate(Page page)
        {
            await _navigationService.PushAsync(page);
        }

        public async Task<Page> PopAsync()
        {
            return await _navigationService.PopAsync();
        }

        public async Task<Page> PopAsync(bool animated)
        {
            return await _navigationService.PopAsync(animated);
        }

        public async Task<Page> PopModalAsync()
        {
            return await _navigationService.PopModalAsync();
        }

        public async Task<Page> PopModalAsync(bool animated)
        {
            return await _navigationService.PopModalAsync(animated);
        }

        public async Task PopToRootAsync()
        {
            await _navigationService.PopToRootAsync();
        }

        public async Task PopToRootAsync(bool animated)
        {
            await _navigationService.PopToRootAsync(animated);
        }

        public async Task PushAsync(Page page)
        {
            await _navigationService.PushAsync(page);
        }

        public async Task PushAsync(Page page, bool animated)
        {
            await _navigationService.PushAsync(page, animated);
        }

        public async Task PushModalAsync(Page page)
        {
            await _navigationService.PushModalAsync(page);
        }

        public async Task PushModalAsync(Page page, bool animated)
        {
            await _navigationService.PushModalAsync(page, animated);
        }

        public void RemovePage(Page page)
        {
            _navigationService.RemovePage(page);
        }
    }
}
