using System.Collections.Generic;

namespace Wuphf.Client.MVVM
{
    public class NavigationCache
    {
        public Dictionary<string, Dictionary<string, object>> Parameters { get; set; } = new Dictionary<string, Dictionary<string, object>>();
    }
}
