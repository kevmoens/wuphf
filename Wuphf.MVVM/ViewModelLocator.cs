using System;
using System.Globalization;
using System.Reflection;

namespace Wuphf.MVVM
{

    public class ViewModelLocator
    {
        public static IServiceProvider ServiceProvider;

        /// <summary>
        /// Default view type to view model type resolver, assumes the view model is in same assembly as the view type, but in the "ViewModels" namespace.
        /// </summary>
        static Func<Type, Type> _defaultViewTypeToViewModelTypeResolver =
            viewType =>
            {
                var viewName = viewType.FullName;
                if (viewName == null)
                {
                    return null;
                }
                viewName = viewName.Replace(".Views.", ".ViewModels.");
                var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                var suffix = viewName.EndsWith("View") ? "Model" : "ViewModel";
                var viewModelName = String.Format(CultureInfo.InvariantCulture, "{0}{1}, {2}", viewName, suffix, viewAssemblyName);
                return Type.GetType(viewModelName);
            };

        public static object Resolve(Type type)
        {
            var viewModelType = _defaultViewTypeToViewModelTypeResolver.Invoke(type);
            if (viewModelType == null)
            {
                return null;
            }
            return ServiceProvider?.GetService(viewModelType);
        }
    }
}
