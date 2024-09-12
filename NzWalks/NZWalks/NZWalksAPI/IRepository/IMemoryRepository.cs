using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.IRepository
{
    public class IMemoryRepository : IRegionRepository
    {
        public Task<Region> CreateAsync(Region region)
        {
            throw new NotImplementedException();
        }

        public Task<Region?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Region>> GetAllAsync(string filter , string filterON)
        {
            return new List<Region>()
            {
                new Region()
                {
                    Id = Guid.NewGuid(),
                    Code = "nani",
                    Name = "Name",
                }
            };
        }

        public Task<Region?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Region?> UpdateAsync(Guid id, Region region)
        {
            throw new NotImplementedException();
        }
    }
}
