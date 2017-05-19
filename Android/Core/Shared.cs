using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Android.Properties;

namespace Android.Core
{
   
    class Shared
    {
        public static string Url = "https://api.tinify.com/shrink";

        public static readonly IList<ImagePercent> ResourceDpi = new ReadOnlyCollection<ImagePercent>
      (new[] {
             new ImagePercent ( 0.1875 ,"drawable-ldpi"),
             new ImagePercent (0.375 ,"drawable-mdpi"),
             new ImagePercent (0.5  ,"drawable-hdpi"),
             new ImagePercent (0.75 ,"drawable-xhdpi"),
             new ImagePercent (1  ,"drawable-xxhdpi")
      });

        public static readonly IList<ImageSize> IconSize = new ReadOnlyCollection<ImageSize>
     (new[] {
             new ImageSize (48,48 ,"mipmap-mdpi"),
             new ImageSize (72,72  ,"mipmap-hdpi"),
             new ImageSize (96,96 ,"mipmap-xhdpi"),
             new ImageSize (144,144  ,"mipmap-xxhdpi"),
             new ImageSize (192,192  ,"mipmap-xxxhdpi")
     });

        public static string GetDirectory()
        {
            var fbd = new FolderBrowserDialog();
            return fbd.ShowDialog() != DialogResult.OK ? null : fbd.SelectedPath;
        }

        public static int GetTinyKeyId(DataTable dataTable)
        {
            var results = from myRow in dataTable.AsEnumerable()
                          where string.Compare(myRow.Field<string>("Count"), "500", StringComparison.Ordinal) <=1 
                          select new { Row = myRow.Field<string>("Row"), };
            if (results.Any())
                return int.Parse(results.First().Row);
           return - 1;
        }

        public static DataTable ReadCsv(string filePath)
        {
            var dt = new DataTable();
            // Creating the columns
            File.ReadLines(filePath).Take(1)
                .SelectMany(x => x.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                .ToList()
                .ForEach(x => dt.Columns.Add(x.Trim()));

            // Adding the rows
            File.ReadLines(filePath).Skip(1)
                .Select(x => x.Split(','))
                .ToList()
                .ForEach(line => dt.Rows.Add(line));
            return dt;
        }

        public static void CreateDirectoryDrawable(string path)
        {
            foreach (var currentPath in ResourceDpi.Select(dir => path + "/" + dir.FolderName))
            {
                Directory.CreateDirectory(currentPath);
            }
        }

        public static void CreateDirectoryIcon(string path)
        {
            foreach (var currentPath in IconSize.Select(dir => path + "/" + dir.FolderName))
            {
                Directory.CreateDirectory(currentPath);
            }
        }
    }
}
