using ImageProcessorCore;
using Microsoft.AspNet.Http;
using Microsoft.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
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
        CloudStorageAccount _storageAccount;
        CloudBlobClient _blobClient;
        CloudBlobContainer _container;

        public FileHandler(CloudStorageAccount storageAccount)
        {
            // Retrieve storage account from connection string.
            _storageAccount = storageAccount;

            // Create the blob client.
            _blobClient = _storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            _container = _blobClient.GetContainerReference("pictures");
        }
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

        public async Task<bool> saveSmallPicture(ICollection<IFormFile> files, string basePath)
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

                        Task<bool> saveImg = new Task<bool>(() => {
                                image.Resize(260, 0) // Passing zero one of height or width will perserve the aspect ratio of the image.
                                     .Save(outStream, new JpegFormat());
                                return true;
                        });
                        try
                        {
                            await saveImg;
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

        public async Task saveImageToBlob(ICollection<IFormFile> files)
        {
            foreach (var file in files)
            {
                var fileName = ContentDispositionHeaderValue
                    .Parse(file.ContentDisposition)
                    .FileName
                    .Trim('"');// FileName may return double quotes
                CloudBlockBlob blockBlob = _container.GetBlockBlobReference(fileName);

                // Create or overwrite
                using (var fileStream = file.OpenReadStream())
                {
                    await blockBlob.UploadFromStreamAsync(fileStream);
                }
            }
        }
    }

    
}
