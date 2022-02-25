using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace Wuphf.Application
{
    public class AppSettingsManager
    {
        // stores the instance of the singleton
        private static AppSettingsManager _instance;

        // variable to store your appsettings in memory for quick and easy access
        private JObject _secrets;

        // constants needed to locate and access the App Settings file
        private const string Namespace = "Wuphf";
        private const string Filename = "appsettings.json";

        // Creates the instance of the singleton

        private AppSettingsManager()
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(AppSettingsManager)).Assembly;
            var stream = assembly.GetManifestResourceStream($"{Namespace}.{Filename}");
            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                _secrets = JObject.Parse(json);
            }
        }

        // Accesses the instance or creates a new instance
        public static AppSettingsManager Settings
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AppSettingsManager();
                }
                return _instance;
            }
        }

        // Used to retrieved setting values
        public string this[string name]
        {

            get
            {
                try
                {
                    var path = name.Split(':');

                    JToken node = _secrets[path[0]];
                    for (int index = 1; index < path.Length; index++)
                    {
                        node = node[path[index]];
                    }

                    return node.ToString();
                }
                catch (Exception)
                {
                    System.Diagnostics.Debug.WriteLine($"Unable to retrieve secret '{name}'");
                    return string.Empty;
                }
            }
        }
    }
}
