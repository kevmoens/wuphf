using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Neleus.DependencyInjection.Extensions;
using Xamarin.Forms;

namespace Wuphf.MVVM
{

    /// <summary>
    /// Future goals are to handle handling navigating forward and backwards
    /// Handle view disposing and persistence
    ///
    /// MVP is Navigation Parameters
    /// </summary>
    public class RegionService : IRegionService
    {
        IServiceProvider serviceProvider;
        ILogger<IRegionService> logger;
        public RegionService(IServiceProvider serviceProvider, ILogger<IRegionService> logger)
        {
            this.serviceProvider = serviceProvider;
            this.logger = logger;
        }
        public Dictionary<string, object> NavigationServices { get; set; } = new Dictionary<string, object>(System.StringComparer.InvariantCultureIgnoreCase);
        public async Task Navigate(string RegionName, string ViewName, Dictionary<string, object> parameters = null)
        {
            //Need Neuleus so I can register a class that wraps the view so I can pull it back up.
            //Have to make a extension method for Registering the new.


            var navigation = (INavigation)NavigationServices[RegionName];
            Page page = navigation.NavigationStack[navigation.NavigationStack.Count - 1];

            Page view;
            try
            {
                view = serviceProvider.GetServiceByName<Page>(ViewName);
            }
            catch (ArgumentException ex)
            {
                logger?.LogError(ex, $"Navigate Unable to find {ViewName}");
                return;
            }
            if (view == null)
            {
                return;
            }


            await ((INavigation)NavigationServices[RegionName]).PushAsync(view);
            if (parameters == null)
            {
                parameters = new Dictionary<string, object>();
            }

            var tyNavAware = view.BindingContext.GetType()?.GetInterface("INavigationParametersAware", true);
            if (tyNavAware != null && view.BindingContext != null)
            {
                ((INavigationParametersAware)view.BindingContext).NavigatedTo(parameters);
            }

            navigation.RemovePage(page);
        }
    }
}
