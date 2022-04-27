using System.Collections.Generic;
using System.Threading.Tasks;
//using Xamarin.Forms;

namespace Wuphf.MVVM
{
    public interface IRegionService
    {
        Dictionary<string, object> NavigationServices { get; set; }

        Task Navigate(string RegionName, string ViewName, Dictionary<string, object> parameters = null);
    }
}