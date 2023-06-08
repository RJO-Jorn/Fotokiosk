using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace FotoKiosk
{

    public sealed partial class ReportsPage : Page
    {
        public ReportsPage()
        {
            this.InitializeComponent();
        }

        private async void FileOpener_Click(object sender, RoutedEventArgs e)
        {
            var picker = new FileOpenPicker();
            picker.SuggestedStartLocation = PickerLocationId.Downloads;
            picker.FileTypeFilter.Add(".csv");

            var file = await picker.PickSingleFileAsync();

            if (file == null)
            {
                FileNameEl.Text = "Geen geldig bestand gekozen.";
                return;
            }
            else
            {
                FileNameEl.Text = file.Path;
            }

            using (var fileAccess = await file.OpenReadAsync())
            {
                using (var stream = fileAccess.AsStreamForRead())
                {
                    using (var reader = new StreamReader(stream))
                    {
                         using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                        {
                          var records =csv.GetRecords<Products>();
                          var filepath = file.Path;
                          var parts = filepath.Split(',');
                        
                          var product = new List<Products>();
                            {
                                product.Add(new Products
                                {
                                    Id = int.Parse(parts[0]),
                                    Productname = parts[1],
                                    Amount = int.Parse(parts[2]),
                                    TotalPrice = double.Parse(parts[3])
                                });
                            }
                         }
                    }
                }

            }
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
