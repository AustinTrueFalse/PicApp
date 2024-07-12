using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PicApp.Models;
using System;
using Android.Content;
using Android.Database;
using Android.Provider;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace PicApp.Droid
{
    public class AndroidPhotoLoaderService : IPhotoLoaderService
    {
        public async Task<List<Photo>> LoadPhotosAsync()
        {
            var photos = new List<Photo>();
            var projection = new[] { MediaStore.Images.Media.InterfaceConsts.Data, MediaStore.Images.Media.InterfaceConsts.DisplayName };
            var uri = MediaStore.Images.Media.ExternalContentUri;

            using (var cursor = Android.App.Application.Context.ContentResolver.Query(uri, projection, null, null, null))
            {
                if (cursor != null)
                {
                    while (cursor.MoveToNext())
                    {
                        string path = cursor.GetString(cursor.GetColumnIndexOrThrow(MediaStore.Images.Media.InterfaceConsts.Data));
                        string name = cursor.GetString(cursor.GetColumnIndexOrThrow(MediaStore.Images.Media.InterfaceConsts.DisplayName));
                        photos.Add(new Photo { Name = name, Path = path });
                    }
                }
            }

            return await Task.FromResult(photos);
        }
    }
}