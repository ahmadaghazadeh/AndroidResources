using System;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Forms;
using Android.Core;
using Newtonsoft.Json;


namespace Android
{
    public partial class Form1 : Form
    {
 
        DataTable _dbApi;
        public Form1()
        {
            InitializeComponent();
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
                var path = Shared.GetDirectory();
                TinyPngResizeDrawable(path);
          
        }

        private void TinyPngResizeDrawable(string path)
        {
            var d = new DirectoryInfo(path);
            var precent = Shared.ResourceDpi;
            progressBar1.Maximum = ((precent.Count) * d.GetFiles().Length) - 1;
            Shared.CreateDirectoryDrawable(path);
            foreach (var file in d.GetFiles())
            {
                var img = System.Drawing.Image.FromFile(file.FullName);
                foreach (var per in precent)
                {
                    var id = Shared.GetTinyKeyId(_dbApi);
                    var dr = _dbApi.Rows[id];
                    var key = dr[1].ToString();
                    var tinyPng = new TinyPngClient(key);
                    tinyPng.Upload(file.FullName);
                    var fileName = path + "/" + per.FolderName + "/" + Path.GetFileName(file.Name);
                    var width = (int)(img.Width * per.Percent);
                    var height = (int)(img.Height * per.Percent);
                    var resizeOperation = new ResizeOperation(ResizeType.Fit, width, height);
                    tinyPng.Resize(fileName, resizeOperation);
                    Application.DoEvents();
                    progressBar1.Value++;
                }
            }
        }

        private void TinyPngIcon(string path)
        {
            var d = new DirectoryInfo(path);
            var precent = Shared.IconSize;
            progressBar1.Maximum = ((precent.Count) * d.GetFiles().Length);
            Shared.CreateDirectoryIcon(path);
            foreach (var file in d.GetFiles())
            {
                foreach (var per in precent)
                {
                    var id = Shared.GetTinyKeyId(_dbApi);
                    var dr = _dbApi.Rows[id];
                    var key = dr[1].ToString();
                    var tinyPng = new TinyPngClient(key);
                    tinyPng.Upload(file.FullName);
                    var fileName = path + "/" + per.FolderName + "/" + Path.GetFileName(file.Name);
                    var resizeOperation = new ResizeOperation(ResizeType.Fit, per.Width, per.Hight);
                    tinyPng.Resize(fileName, resizeOperation);
                    Application.DoEvents();
                    progressBar1.Value++;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _dbApi = Shared.ReadCsv(Directory.GetCurrentDirectory() + "/TinyApiKey.csv");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var path = Shared.GetDirectory();
            TinyPngIcon(path);
        }
    }
}
