using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyPng.Responses;

namespace Android.Core
{
    public static class Extensions
    {

        public async static Task<byte[]> GetImageByteData(this TinyPngImageResponse result)
        {
            return await result.HttpResponseMessage.Content.ReadAsByteArrayAsync();
        }
        public async static Task SaveImageToDisk(this TinyPngImageResponse result, string filePath)
        {
            var byteData = await result.GetImageByteData();
            File.WriteAllBytes(filePath, byteData);
        }
    }
}
