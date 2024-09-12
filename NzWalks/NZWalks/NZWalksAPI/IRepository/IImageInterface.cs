using NZWalksAPI.Models.Domain;
namespace NZWalksAPI.IRepository
{
    public interface IImageInterface
    {
        // Upload method takes the image domain model
        // As a return type Image domain model
       Task<Image> Upload(Image image);
    }
}
