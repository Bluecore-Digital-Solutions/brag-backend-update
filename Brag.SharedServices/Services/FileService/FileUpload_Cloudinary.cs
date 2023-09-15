using Brag.Domain.Model.Configuration;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Threading.Tasks;

namespace Brag.SharedServices.Services.FileService
{
    public static class FileUpload_Cloudinary
    {
        public static async Task<ImageUploadResult> CloudinaryImageUploadAsync(string imagePath, CloudinaryModel cloudinaryModel)
        {

            Account account = new Account
                (
                  cloudinaryModel.CloudinaryUsername,
                  cloudinaryModel.CloudinaryApiKey,
                  cloudinaryModel.CloudinarySecreteKey
                  );

            Cloudinary cloudinary = new Cloudinary(account);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(@imagePath),

            };

            var uploadResult = cloudinary.Upload
               (uploadParams);

            return uploadResult;

        }

    }
}
