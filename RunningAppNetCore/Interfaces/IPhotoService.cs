using CloudinaryDotNet.Actions;

namespace RunningAppNetCore.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync();
    }
}
