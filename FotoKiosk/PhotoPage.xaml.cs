using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace FotoKiosk
{
    public sealed partial class PhotoPage : UserControl
    {
        public PhotoPage()
        {
            this.InitializeComponent();
            loadPhotos();
        }

        private void loadPhotos()
        {
            // TODO
        }
    }
}

using System.Windows;
using System.Windows.Controls;

namespace WpfApp
{
    public partial class PhotoPage : Page
    {
        public PhotoPage()
        {
            InitializeComponent();
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            // Navigeer terug naar het vorige scherm
            NavigationService.GoBack();
        }
    }
}

