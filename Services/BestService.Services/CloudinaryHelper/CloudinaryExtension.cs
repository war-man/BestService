﻿namespace BestService.Services.CloudinaryHelper
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    public class CloudinaryExtension
    {
        public static async Task<string> UploadImageAsync(
            Cloudinary cloudinary,
            IFormFile fileForm,
            Transformation transformation = null)
        {
            fileForm = fileForm ?? throw new ArgumentNullException(nameof(fileForm));

            byte[] destinationImage;

            using (var memoryStream = new MemoryStream())
            {
                await fileForm.CopyToAsync(memoryStream);
                destinationImage = memoryStream.ToArray();
            }

            using var destinationSteream = new MemoryStream(destinationImage);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(fileForm.FileName, destinationSteream),
                Transformation = transformation,
            };

            var result = await cloudinary.UploadAsync(uploadParams);

            var imageUri = result.Uri.AbsoluteUri;

            return imageUri;
        }

        public static async Task<string> DeleteImageImageAsync(Cloudinary cloudinary, string publicId)
        {
            DeletionParams destroyParams = new DeletionParams(publicId)
            {
                ResourceType = ResourceType.Image,
            };

            var destroyResult = await cloudinary.DestroyAsync(destroyParams);

            return destroyResult.Result;
        }
    }
}
