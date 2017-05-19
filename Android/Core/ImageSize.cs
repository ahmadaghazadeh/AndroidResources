using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Android.Core
{
    class ImageSize
    {
        public int Width { get; set; }
        public int Hight { get; set; }
        public string FolderName { get; set; }

        public ImageSize(int width, int height, string folderName)
        {
            this.Width = width;
            this.Hight = height;
            this.FolderName = folderName;
        }
    }
}
