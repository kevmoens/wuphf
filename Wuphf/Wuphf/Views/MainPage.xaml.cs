using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wuphf.MVVM;
using Xamarin.Forms;

namespace Wuphf.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            this.BindingContext = ViewModelLocator.Resolve(this.GetType());
            InitializeComponent();
        }
    }
}
