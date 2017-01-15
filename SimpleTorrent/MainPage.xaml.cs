using SimpleTorrent.Lib;
using SimpleTorrent.Torrent;
using SimpleTorrent.viewmodels;
using System;
using System.Collections.Generic;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SimpleTorrent
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private SettingsVM vm;
        public static Client client;

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            vm = new SettingsVM();
            this.DataContext = vm;
        }

        private async void Folder_Click(object sender, RoutedEventArgs e)
        {
            var folderPicker = new Windows.Storage.Pickers.FolderPicker();

            folderPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Downloads;

            folderPicker.FileTypeFilter.Add("*");

            Windows.Storage.StorageFolder folder = await folderPicker.PickSingleFolderAsync();

            if (folder != null)
            {
                // Application now has read/write access to all contents in the picked folder
                // (including other sub-folder contents)
                vm.setPath(folder.Path);
                Windows.Storage.AccessCache.StorageApplicationPermissions.
                FutureAccessList.AddOrReplace(Settings.DOWNLOADS_PATH, folder);
            }
        }

        private async void Download_Click(object sender, RoutedEventArgs e)
        {
            client = new Client(3001, "D:\\testDownloads\\test.torrent", Settings.getDownloadsPath());
            await client.loadTorrent("D:\\testDownloads\\test.torrent", Settings.getDownloadsPath());
            client.Start();
        }
    }
}
