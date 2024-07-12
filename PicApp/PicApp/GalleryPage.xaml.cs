using PicApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PicApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GalleryPage : ContentPage
    {
        public GalleryPage()
        {
            InitializeComponent();
            CheckPermissionsAndLoadPhotos();
        }

        private async void CheckPermissionsAndLoadPhotos()
        {
            var status = await CheckAndRequestStoragePermissionAsync();

            if (status == PermissionStatus.Granted)
            {
                LoadPhotos();
            }
            else
            {
                await DisplayAlert("Error", "Storage permission is required to access photos.", "OK");
            }
        }

        private async Task<PermissionStatus> CheckAndRequestStoragePermissionAsync()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();

            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.StorageRead>();
            }

            return status;
        }

        private async void LoadPhotos()
        {
            var photoLoaderService = DependencyService.Get<IPhotoLoaderService>();
            if (photoLoaderService == null)
            {
                await DisplayAlert("Error", "Photo loader service not available", "OK");
                Debug.WriteLine("Photo loader service not available");
                return;
            }

            var photos = await photoLoaderService.LoadPhotosAsync();

            // Проверка полученных данных
            if (photos == null || photos.Count == 0)
            {
                Debug.WriteLine("No photos found");
            }
            else
            {
                foreach (var photo in photos)
                {
                    Debug.WriteLine($"Photo: {photo.Name}, Path: {photo.Path}");
                }
            }

            collectionView.ItemsSource = photos;
        }
    }
}