using PicApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PicApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FullScreenPhotoPage : ContentPage
    {
        public FullScreenPhotoPage(Photo photo)
        {
            InitializeComponent();

            var image = new Image
            {
                Source = ImageSource.FromFile(photo.Path),
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            var timestampLabel = new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TextColor = Color.Black
            };

            var stackLayout = new StackLayout
            {
                Children = { image, timestampLabel },
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            Content = stackLayout;
        }
    }
}