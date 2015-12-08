﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MappingApp.ViewModel;
using Xamarin.Forms;

namespace MappingApp.View
{
    public partial class MainView : ContentPage
    {
        public MainView()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.Main;
        }
    }
}