using System;
using System.Collections.Generic;
using Wuphf.MVVM;
using Xamarin.Forms;

namespace Wuphf.Views
{
    public partial class CreateAppointmentsPage : ContentPage
    {
        public CreateAppointmentsPage()
        {
            InitializeComponent();
            this.BindingContext = ViewModelLocator.Resolve(this.GetType());
        }
    }
}
