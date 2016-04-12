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
using Model;
using System.Linq;

namespace DAL
{
    public class FileHandler
    {
        private CloudStorageAccount _storageAccount;
        private CloudBlobClient _blobClient;
        private CloudBlobContainer _container;
        private ImageTestContext _db;

        private List<ImageSize> imageSizes;
        public FileHandler(CloudStorageAccount storageAccount, ImageTestContext context)
        {
            // Retrieve storage account from connection string.
            _storageAccount = storageAccount;
            // Create the blob client.
            _blobClient = _storageAccount.CreateCloudBlobClient();
            // Retrieve reference to a previously created container.
            _container = _blobClient.GetContainerReference("pictures");

            _db = context;

            imageSizes = new List<ImageSize>
            {
                new ImageSize { Size = Sizes.thumb, width = 80 },
                new ImageSize { Size = Sizes.small, width = 360 },
                new ImageSize { Size = Sizes.medium, width = 640 },
                new ImageSize { Size = Sizes.large, width = 1024 }
            };
            
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
                fileName = Path.GetFileNameWithoutExtension(fileName) + ".jpg"; //Every image gets extension .jpg to simplyfy finding every transformed image


                CloudBlockBlob blockBlob = _container.GetBlockBlobReference(Enum.GetName(typeof(Sizes), Sizes.original) + "/" + fileName);
                blockBlob.Properties.ContentType = "image/jpeg";
                
                // Create or overwrite
                using (var fileStream = file.OpenReadStream())
                {
                    await blockBlob.UploadFromStreamAsync(fileStream);

                }


                //Saves several resized versions to blob container
                await saveTransformImagesToBlobAsJpeg(file, fileName);

                //Saves image in database
                _db.Pictures.Add(new Pictures
                {
                    Url = fileName
                });
                _db.SaveChanges();
               
            }
        }

        public async Task saveTransformImagesToBlobAsJpeg(IFormFile file, string fileName)
        {

            foreach (var size in imageSizes)
            {
                CloudBlockBlob blockBlob = _container.GetBlockBlobReference(Enum.GetName(typeof(Sizes),size.Size) + "/" + fileName);
                blockBlob.Properties.ContentType = "image/jpeg";

                using (var inStream = file.OpenReadStream())
                {
                    // Create or overwrite
                    using (var fileStream = await blockBlob.OpenWriteAsync())
                    {
                        var image = new Image(inStream);

                        image.Resize(size.width, 0) // Passing zero on height or width will perserve the aspect ratio of the image.
                        .Save(fileStream, new JpegFormat());
                    }
                }
            }
        }
        public Picture getPicture(int id, Sizes size)
        {
            var pic =  _db.Pictures.SingleOrDefault(p => p.Id == id);
            if(pic == null)
            {
                return null;
            }
            else
            {
                CloudBlockBlob blockBlob = _container.GetBlockBlobReference(Enum.GetName(typeof(Sizes), size) + "/" + pic.Url);
                
                return new Picture
                {
                    id = pic.Id,
                    description = pic.Description,
                    url = blockBlob.Uri.AbsoluteUri
                };
            }
        }

        public List<Picture> getPictures()
        {
            var list = _db.Pictures.Select(p => new Picture
            {
                id = p.Id,
                description = p.Description,
                url = p.Url
            }).ToList();

            List<Picture> fullList = new List<Picture>();
            
            foreach(var pic in list)
            {
                foreach (var size in Enum.GetNames(typeof(Sizes)))
                {
                    CloudBlockBlob blockBlob = _container.GetBlockBlobReference(size + "/" + pic.url);
                    fullList.Add(new Picture
                    {
                        id = pic.id,
                        description = pic.description,
                        url = blockBlob.Uri.AbsoluteUri
                    });
                }
            }
            return fullList;
        }
    }
}
