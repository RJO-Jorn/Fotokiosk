using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace FotoKiosk
{
    public sealed partial class SearchPage : Page
    {
        public SearchPage()
        {
            this.InitializeComponent();
        }

        private async void search_Click(object sender, RoutedEventArgs e)
        {
            var appFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var fotosFolder = await appFolder.GetFolderAsync("Assets\\Fotos");
            var dayFolder = await fotosFolder.GetFoldersAsync();
            var timepick = HourMinPick.SelectedTime ;
            var timestring = timepick.ToString();
            var format1 = "HH:mm:ss";
            DateTime time1;
            var selectedTime = DateTime.TryParseExact(timestring, format1, CultureInfo.InvariantCulture, DateTimeStyles.None, out time1);
            var inputDay = comboday.SelectedValue;
            string selectedDay = inputDay.ToString();
            


            var pathList = new List<string>();
            foreach (var folder in dayFolder)
            {
                string folderName = folder.Name.ToString();
                string folderDay = folderName.Substring(2);
                if (folderDay == selectedDay)
                {
                    
                    var fileList = await folder.GetFilesAsync();
                    foreach (var file in fileList)
                    {

                        var path = file.Path;
                        string last19 = path.Substring(path.Length - 19);
                        string datetimestr = last19.Substring(0, 8);
                        var parts = datetimestr.Split('_');
                        var hour = parts[0];
                        var min = parts[1];
                        var sec = parts[2];
                        var timestr = hour + "/" + min + "/" + sec;
                        var format = "HH/mm/ss";
                        DateTime time;
                        var timeDT = DateTime.TryParseExact(timestr, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out time);

                        var minTime = time1.AddMinutes(-10);
                        var maxTime = time1.AddMinutes(10);
                        if (time >= minTime && time <= maxTime)
                        {
                            pathList.Add(file.Path);
                        }

                    }
                }
                
            }
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                gvFotos.ItemsSource = pathList;
            });
        }
        public async void gvFotos_ItemClick(object sender, ItemClickEventArgs e)
        {
            string url = (string)e.ClickedItem;
            string last8 = url.Substring(url.Length - 8);
            string itemId = last8.Substring(0, 4);

            CheckoutPage.Instance.photoIdEl.Text = itemId;

        }
    }
}
