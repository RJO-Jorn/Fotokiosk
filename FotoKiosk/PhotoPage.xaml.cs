using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    
    public partial class PhotoPage : UserControl
    {

        public PhotoPage()
        {
            this.InitializeComponent();
            loadPhotos();
        }

        public async void loadPhotos()
        {
            var appFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var fotosFolder = await appFolder.GetFolderAsync("Assets\\Fotos");
            var dayFolder = await fotosFolder.GetFoldersAsync();

            var now = DateTime.Now;
            int day = (int)now.DayOfWeek;
            string today = day.ToString();


            string folderDayNumber;

            var pathList = new List<string>();

            foreach ( var folder in dayFolder )
            {
                folderDayNumber = folder.Name[0].ToString();
                if (folderDayNumber == today)
                {
                    var fileList = await folder.GetFilesAsync();
                    foreach(var file in fileList ) {
                        pathList.Add(file.Path);
                    }
                    gvFotos.ItemsSource = pathList;
                    
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
