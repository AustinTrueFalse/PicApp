using PicApp.Models;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using System.Collections.ObjectModel;
using System.IO;

using System.Threading.Tasks;


namespace PicApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GalleryPage : ContentPage
    {
        public ObservableCollection<Photo> Photos { get; set; }

        
        public GalleryPage()
        {
            InitializeComponent();
            Photos = new ObservableCollection<Photo>();
            LoadPhotos();
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

        private void OnOpenClicked(object sender, EventArgs e)
        {
            var selectedPhoto = collectionView.SelectedItem as Photo;
            if (selectedPhoto != null)
            {
                Navigation.PushAsync(new FullScreenPhotoPage(selectedPhoto));
            }
            else
            {
                DisplayAlert("Error", "Please select a photo to open", "OK");
            }
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            var selectedPhoto = collectionView.SelectedItem as Photo;
            if (selectedPhoto != null)
            {
                // Удаление изображения из коллекции
                Photos.Remove(selectedPhoto);

                // Удаление изображения из файловой системы
                await DeletePhotoFromFileSystem(selectedPhoto);

                // Показать сообщение об успешном удалении
                await DisplayAlert("Success", "Photo deleted successfully", "OK");

            }
            else
            {
                await DisplayAlert("Error", "Please select a photo to delete", "OK");
            }
        }

        private async Task DeletePhotoFromFileSystem(Photo photo)
        {
            try
            {
                if (File.Exists(photo.Path))                   
                {
                    await DisplayAlert("warn", $"файл существует {photo.Path}", "cancel");
                    File.Delete(photo.Path);
                }
                await Task.CompletedTask; // Для асинхронной операции
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to delete photo: {ex.Message}", "OK");
            }
        }


    }
}