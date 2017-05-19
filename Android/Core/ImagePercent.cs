using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Android.Core
{
    class ImagePercent
    {
        public double Percent { get; set; }
        public string FolderName { get; set; }

        public ImagePercent(double percent, string folderName)
        {
            this.Percent = percent;
            this.FolderName = folderName;
        }
    }
}
