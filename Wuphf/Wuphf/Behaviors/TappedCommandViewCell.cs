using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Wuphf.Behaviors
{
    public static class TappedCommandViewCell
    {
        private const string TappedCommand = "TappedCommand";
        private const string TappedCommandParameter = "TappedCommandParameter";

        public static readonly BindableProperty TappedCommandProperty =
            BindableProperty.CreateAttached(
                TappedCommand,
                typeof(ICommand),
                typeof(TappedCommandViewCell),
                default(ICommand),
                BindingMode.OneWay,
                null,
                PropertyChanged);

        public static readonly BindableProperty TappedCommandParameterProperty =
            BindableProperty.CreateAttached(
                TappedCommandParameter,
                typeof(object),
                typeof(TappedCommandViewCell),
                default(object),
                BindingMode.OneWay,
                null);

        private static void PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ViewCell cell)
            {
                cell.Tapped -= ViewCellOnTapped;
                cell.Tapped += ViewCellOnTapped;
            }
        }

        private static void ViewCellOnTapped(object sender, EventArgs e)
        {
            if (sender is ViewCell cell && cell.IsEnabled)
            {
                var command = GetTappedCommand(cell);
                var parameter = GetTappedCommandParameter(cell);

                if (command != null && command.CanExecute(parameter))
                {
                    command.Execute(parameter);
                }
            }
        }

        public static ICommand GetTappedCommand(BindableObject bindableObject) =>
            (ICommand)bindableObject.GetValue(TappedCommandProperty);

        public static void SetTappedCommand(BindableObject bindableObject, object value) =>
            bindableObject.SetValue(TappedCommandProperty, value);

        public static object GetTappedCommandParameter(BindableObject bindableObject) =>
            bindableObject.GetValue(TappedCommandParameterProperty);

        public static void SetTappedCommandParameter(BindableObject bindableObject, object value) =>
            bindableObject.SetValue(TappedCommandParameterProperty, value);
    }
}
