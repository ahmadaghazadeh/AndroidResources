using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Android.Core
{
    class TinyPngClient
    {
        public HttpClient HttpClient = new HttpClient();
        private const string ApiEndpoint = "https://api.tinify.com/shrink";
        private string _apiKey;
        private string _compressPath;
        private readonly WebClient _webClient = new WebClient();

        public int CompressionCount { get; set; }

        /// <summary>
        /// Wrapper for the tinypng.com API
        /// </summary>
        /// <param name="apiKey">Your tinypng.com API key, signup here: https://tinypng.com/developers </param>
        public TinyPngClient(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentNullException(nameof(apiKey));

            _apiKey = Convert.ToBase64String(Encoding.UTF8.GetBytes("api:" + apiKey));
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _apiKey);
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            jsonSettings.Converters.Add(new StringEnumConverter { CamelCaseText = true });

        }

        public void ChangeApiKey(string apiKey)
        {
            _apiKey = Convert.ToBase64String(Encoding.UTF8.GetBytes("api:" + apiKey));
        }

        public void Upload(string path)
        {

            _webClient.Headers.Add(HttpRequestHeader.Authorization, "Basic " + _apiKey);
            _webClient.UploadData(ApiEndpoint, File.ReadAllBytes(path));
            _compressPath = _webClient.ResponseHeaders["Location"];
            CompressionCount = int.Parse(_webClient.ResponseHeaders["Compression-Count"]);
        }

        public void Compress(string output)
        {
            _webClient.DownloadFileAsync(new Uri(_compressPath), output);
        }

        public async void Resize(string output, ResizeOperation resize)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, _compressPath);

            var re = new { resize };
            request.Content = new StringContent(JsonConvert.SerializeObject(re),
                                    Encoding.UTF8,
                                    "application/json");//CONTENT-TYPE header
           
            var response = await HttpClient.SendAsync(request);
            var content = response.Content;
            var buffer = await content.ReadAsByteArrayAsync();
            File.WriteAllBytes(output, buffer);
            CompressionCount++;
        }


        #region IDisposable Support
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                HttpClient?.Dispose();
            }
        }
        #endregion
    }
}
