using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Ecommerce.Models.Settings;
using Ecommerce.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Services
{
    public class CloudinaryService : IImageService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IOptions<CloudinarySettings> config)
        {
            var acc = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            //if (file == null || file.Length == 0)
            //    throw new ArgumentException("No image provided.");

            //if (!file.ContentType.StartsWith("image/"))
            //    throw new ArgumentException("Only image files are allowed.");

            //await using var stream = file.OpenReadStream(); // Safe way to read stream

            //var uploadParams = new ImageUploadParams
            //{
            //    File = new FileDescription(file.FileName, stream),
            //    Folder = "ecommerce-products"
            //};

            //var result = await _cloudinary.UploadAsync(uploadParams);

            //if (result.Error != null)
            //    throw new Exception("Cloudinary upload error: " + result.Error.Message);

            //return result.SecureUrl.ToString();

            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty.");

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                Folder = "ecommerce-products"
            };

            var result = await _cloudinary.UploadAsync(uploadParams);
            return result.SecureUrl.ToString();
        }
    }
}
