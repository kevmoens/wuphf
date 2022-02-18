using System;
using Xamarin.Forms;
using Microsoft.Extensions.DependencyInjection;
namespace Wuphf.MVVM
{

    public class RegionManager : Frame
    {
        public static IServiceProvider GetServiceProvider(BindableObject Obj)
        {
            return (IServiceProvider)Obj.GetValue(ServiceProviderProperty);
        }
        public static void SetServiceProvider(BindableObject Obj, IServiceProvider Value)
        {
            Obj.SetValue(ServiceProviderProperty, Value);
            AlignToRegionService(Obj);
        }

        public static readonly BindableProperty ServiceProviderProperty = BindableProperty.CreateAttached("ServiceProvider",
                 typeof(IServiceProvider), typeof(RegionManager), BindingMode.TwoWay, propertyChanged:  OnServiceProviderChanged);

        public static void OnServiceProviderChanged(BindableObject d, object oldValue, object newValue )
        {
            AlignToRegionService(d);
        }

        public static string GetRegionName(BindableObject Obj)
        {
            return (string)Obj.GetValue(RegionNameProperty);
        }
        public static void SetRegionName(BindableObject Obj, string Value)
        {
            Obj.SetValue(RegionNameProperty, Value);
            AlignToRegionService(Obj);
        }

        public static readonly BindableProperty RegionNameProperty = BindableProperty.CreateAttached("RegionName",
                 typeof(string), typeof(RegionManager), BindingMode.TwoWay, propertyChanged: OnRegionNameChanged);



        public static void OnRegionNameChanged(BindableObject d, object oldValue, object newValue)
        {
            AlignToRegionService(d);
        }
        public static void AlignToRegionService(BindableObject obj)
        {
            var serviceProvider = GetServiceProvider(obj);
            var regionName = GetRegionName(obj);
            if (string.IsNullOrEmpty(regionName)) return;
            if (serviceProvider == null) return;
            var regionService = serviceProvider.GetService<RegionService>();
            if (regionService != null && !regionService.NavigationServices.ContainsKey(regionName))
            {
                regionService.NavigationServices.Add(regionName, new XFNavigationService((Frame)obj));
            }
        }
    }
}
