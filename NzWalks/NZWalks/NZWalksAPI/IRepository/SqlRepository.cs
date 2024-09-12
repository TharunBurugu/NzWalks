using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NZWalksAPI.Controllers;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.IRepository
{
    public class SqlRepository : IRegionRepository
    {
        public NZWalksDbContext DbContext;
        public SqlRepository(NZWalksDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
          await  DbContext.Regions.AddAsync(region); 
          await  DbContext.SaveChangesAsync();
          return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
           var existeingDatabase = await DbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existeingDatabase == null)
            {
                return null;
            }

            DbContext.Regions.Remove(existeingDatabase);
            await DbContext.SaveChangesAsync();
            return existeingDatabase;
        }

        public async Task<List<Region>> GetAllAsync( string? FilterOn , string? FilterQuery)
        {
            var region =  DbContext.Regions.AsQueryable();

            if (string.IsNullOrEmpty(FilterOn) == false && string.IsNullOrEmpty(FilterQuery) == false)
            {
                if (FilterOn.Equals("name", StringComparison.OrdinalIgnoreCase))
                {
                    region = region.Where(x => x.Name.Contains(FilterQuery));
                }
            }

            return await region.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await DbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }
        //public async Task<Region?> GetByIdAsync(Guid id) => await DbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);


        public async Task<Region?> UpdateAsync( Guid id ,Region region)
        {
            var existingRegion = await DbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRegion == null)
            {
                return null;
            }
          existingRegion.Code = region.Code;
          existingRegion.Name = region.Name;
          existingRegion.RegionImageUrl   = region.RegionImageUrl;
          await DbContext.SaveChangesAsync();
          return existingRegion;

        }
    }
}
