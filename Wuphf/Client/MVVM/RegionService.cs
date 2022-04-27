using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Wuphf.MVVM;

namespace Wuphf.Client.MVVM
{
    public class RegionService : IRegionService
    {
        NavigationManager navigationManager;
        NavigationParameters navigationParameters;
        public RegionService(NavigationManager navigationManager, NavigationParameters navigationParameters)
        {
            this.navigationManager = navigationManager;
            this.navigationParameters = navigationParameters;
        }

        public Dictionary<string, object> NavigationServices { get; set; }

        public Task Navigate(string RegionName, string ViewName, Dictionary<string, object> parameters = null)
        {
            Guid id = Guid.NewGuid();
            navigationManager.NavigateTo($"/{ViewName}?id={id}");
            

            if (parameters == null)
            {
                parameters = new Dictionary<string, object>();
            }
            navigationParameters.Parameters.Add(id.ToString(), parameters);
            return Task.CompletedTask;
        }
    }
}
