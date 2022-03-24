using System;
namespace Wuphf.Shared.Configuration
{
    public class AppSettings : IAppSettings
    {
        public AppSettings()
        {
        }
        public string WuphfURL { get; set; }
    }
}
