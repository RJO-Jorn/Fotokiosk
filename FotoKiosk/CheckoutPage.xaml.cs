using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace FotoKiosk
{
    public partial class CheckoutPage : UserControl
    {
        public static CheckoutPage Instance;

        public CheckoutPage()
        {
            this.InitializeComponent();
            Instance = this;
        }

        private void AddEl_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ResetEl_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
