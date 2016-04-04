using ImageProcessorCore;
using Microsoft.AspNet.Http;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ImageProcessorCore.Samplers;
using ImageProcessorCore.Formats;

namespace DAL
{
    public class FileHandler
    {
        public async Task<bool> savePicture(ICollection<IFormFile> files, string basePath)
        {
            foreach (var value in files)
            {
                var fileName = ContentDispositionHeaderValue
                    .Parse(value.ContentDisposition)
                    .FileName
                    .Trim('"');// FileName returns "fileName.ext"(with double quotes) in beta 3


                var filePath = basePath + "\\wwwroot\\Content\\Pictures\\" + fileName;

                try
                {
                    await value.SaveAsAsync(filePath);
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            return true;
        }

        public bool saveSmallPicture(ICollection<IFormFile> files, string basePath)
        {
            foreach (var value in files)
            {
                var fileName = ContentDispositionHeaderValue
                    .Parse(value.ContentDisposition)
                    .FileName
                    .Trim('"');// FileName may return double quotes


                using (var inStream = value.OpenReadStream()) 
                {
                    using (var outStream = File.OpenWrite(basePath + "\\wwwroot\\Content\\Pictures\\Small\\" + Path.GetFileNameWithoutExtension(fileName) + ".jpg"))
                    {
                        var image = new Image(inStream);

                        
                        try
                        {

                            image.Resize(260, 0) // Passing zero one of height or width will perserve the aspect ratio of the image.
                                .Save(outStream, new JpegFormat());
                        }
                        catch (Exception e)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}
