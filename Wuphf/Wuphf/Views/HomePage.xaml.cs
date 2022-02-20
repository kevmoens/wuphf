﻿using System;
using System.Collections.Generic;
using Wuphf.MVVM;
using Xamarin.Forms;

namespace Wuphf.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            this.BindingContext = ViewModelLocator.Resolve(this.GetType());
        }
    }
}
