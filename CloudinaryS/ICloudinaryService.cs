namespace PetGrubBakcend.CloudinaryS
{
    public interface ICloudinaryService
    {
        Task<string> UploadImageAsync(IFormFile file);
    }
}
