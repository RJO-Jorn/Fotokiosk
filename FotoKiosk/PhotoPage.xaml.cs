using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Timers;
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
using Windows.UI.Xaml.Shapes;

namespace FotoKiosk
{

    public partial class PhotoPage : UserControl
    {

        public PhotoPage()
        {
            this.InitializeComponent();
            loadPhotos();

        }

        public async void loadPhotos()
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 1500;
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;

        }

        public async void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            var appFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var fotosFolder = await appFolder.GetFolderAsync("Assets\\Fotos");
            var dayFolder = await fotosFolder.GetFoldersAsync();
            var now = DateTime.Now;
            int day = (int)now.DayOfWeek;
            string today = day.ToString();


            string folderDayNumber;

            var pathList = new List<string>();

            foreach (var folder in dayFolder)
            {
                folderDayNumber = folder.Name[0].ToString();
                if (folderDayNumber == today)
                {
                    var fileList = await folder.GetFilesAsync();
                    foreach (var file in fileList)
                    {
                        now = DateTime.Now;
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

                        var minTime = now.AddMinutes(-2);
                        var maxTime = now.AddMinutes(-30);
                        if (time <= minTime && time >= maxTime)
                        {
                            pathList.Add(file.Path);
                        }
                      
                    }
                }
            }
            gvFotos.ItemsSource = pathList;
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
