using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.IRepository
{
    public interface IRegionRepository
    {
       Task< List<Region>> GetAllAsync( string? FilterOn , string? FilterQuery);
       Task<Region?> GetByIdAsync(Guid id);
       Task<Region> CreateAsync(Region region);
       Task<Region?> UpdateAsync(Guid id, Region region);
       Task<Region?> DeleteAsync(Guid id);
    }
}
