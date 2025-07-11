using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace PetGrubBakcend.CloudinaryS
{
    public class CloudinaryService:ICloudinaryService
    {
        private readonly Cloudinary cloudinary;
        private readonly ILogger<CloudinaryService> _logger;
        public CloudinaryService(IConfiguration configuration, ILogger<CloudinaryService> logger)
        {
            _logger = logger;

            _logger.LogInformation("Cloudinary config: CloudName={cloud}, ApiKey={key}",
            configuration["Cloudinary:CloudName"],
            configuration["Cloudinary:ApiKey"]);


            var account = new Account
            (
                 configuration["Cloudinary:CloudName"],
                 configuration["Cloudinary:ApiKey"],
                 configuration["Cloudinary:ApiSecret"]
            );

            cloudinary = new Cloudinary(account);
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            if (file == null || file.Length==0)
            {
                _logger.LogWarning("Upload failed : file is null or emmpty");
                return null;
            }
           

            try
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Folder = "product_photos",
                    Transformation = new Transformation()
                   .Width(400)
                   .Height(400)
                   .Crop("fit")
                };

                var uploadResult = await cloudinary.UploadAsync(uploadParams);
                if (uploadResult.Error != null)
                {
                    _logger.LogError($"Cloudinary upload error : {uploadResult.Error.Message}");
                    throw new Exception($"Cloudinary upload error :{uploadResult.Error.Message}");
                }

                _logger.LogInformation("Image uploaded successfully:{url}", uploadResult.SecureUrl.ToString());
                return uploadResult.SecureUrl.ToString();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while uploading image to Cloudinary.");
                throw;
            }

           
        }

    }
}
