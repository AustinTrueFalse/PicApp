using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PicApp.Models
{
    public interface IPhotoLoaderService
    {
        Task<List<Photo>> LoadPhotosAsync();
    }

    public class Photo
    {
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
