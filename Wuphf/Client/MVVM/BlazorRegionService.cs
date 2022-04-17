using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wuphf.MVVM;
using Xamarin.Forms;

namespace Wuphf.Client.MVVM
{
    public class BlazorRegionService : IRegionService
    {
        NavigationManager navigationManager;
        NavigationCache navigationCache;
        NavigateToRoute navigateToRoute;
        public BlazorRegionService(NavigationManager navigationManager, NavigationCache navigationCache, NavigateToRoute navigateToRoute)
        {
            this.navigationManager = navigationManager;
            this.navigationCache = navigationCache;
            this.navigateToRoute = navigateToRoute;
        }
        //TODO This should be removed from IRegionService
        public Dictionary<string, INavigation> NavigationServices { get; set; }

        public Task Navigate(string RegionName, string ViewName, Dictionary<string, object> parameters = null)
        {
            Guid parmID = Guid.NewGuid();
            //Add parameters to cache
            navigationCache.Parameters.Add(parmID.ToString(), parameters);
            //Call Dependency Injection to get URL Route from ViewName
            string route = navigateToRoute.Routes[ViewName];
            string url = $"{route}?navparmsid={parmID}";
            navigationManager.NavigateTo(url);
            //View Needs check cache 
            return Task.CompletedTask;
        }
    }
}
