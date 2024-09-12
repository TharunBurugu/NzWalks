using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Controllers;
using NZWalksAPI.Models.Domain;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Xml.Linq;

namespace NZWalksAPI.IRepository
{
    public class SqlWalkRepository : IWalkRepository
    {
        public NZWalksDbContext DbContext;

        public SqlWalkRepository(NZWalksDbContext dbContext)
        {
            DbContext = dbContext;
        }
        public async Task<Walks> CreateAsync(Walks walks)
        {
            await DbContext.walks.AddAsync(walks);
            await DbContext.SaveChangesAsync();
            return walks;
        }

        public async Task<Walks?> DeleteAsync(Guid id)
        {
            var existingID = await DbContext.walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingID == null)
            {
                return null;
            }

            DbContext.walks.Remove(existingID);
            await DbContext.SaveChangesAsync();
            return existingID;
        }

        public async Task<List<Walks>> GetAllAsync(string? FilterOn = null, string? FilterQuery = null, string?
            Sortby = null, bool IsAscending = true, int pageNumber = 1, int pageSize = 1000)
        {

            //return  new List<Walks>()        
            //{
            //    new Walks()
            //    {
            //         Id = Guid.NewGuid(),
            //         Name = "Tharun",
            //         Description = "Description",
            //         Type = 0,
            //         WalkImageUrl = null,
            //         DifficultyID = Guid.NewGuid(),
            //         RegionId = Guid.NewGuid(),

            //    },

            //     new Walks()
            //    {
            //         Id = Guid.NewGuid(),
            //         Name = "Nai",
            //         Description = "Get data",
            //         Type = 0,
            //         WalkImageUrl = null,
            //         DifficultyID = Guid.NewGuid(),
            //         RegionId = Guid.NewGuid(),

            //    }
            //};


            var walks = DbContext.walks.Include("Difficulty").Include("Region").AsQueryable();

            //Filtering
            if (string.IsNullOrEmpty(FilterOn) == false && string.IsNullOrEmpty(FilterQuery) == false)
            {
                if (FilterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    //walks = walks.Where(x => x.Name == (FilterQuery));


                    //.Where(): This is a LINQ extension method. It filters a sequence based on a predicate (a condition).
                    // x => x.Name.Contains(FilterQuery): This is a lambda expression that serves as the predicate for filtering.
                    // It's saying: for each element x in walks, filter those where x.Name contains the substring specified by FilterQuery.
                    walks = walks.Where(x => x.Name.Contains(FilterQuery));
                }
            }


            // sorting 
            if (string.IsNullOrWhiteSpace(Sortby) == false)
            {
                if (Sortby.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = IsAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if (Sortby.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = IsAscending ? walks.OrderBy(x => x.Description) : walks.OrderByDescending(x => x.Description);
                }
            }

            // Pagination

            var skipResults = (pageNumber - 1) * pageSize;


            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();

            //return await DbContext.walks.Include("Difficulty").Include("Region").ToListAsync();
            //or
            //return await DbContext.walks.Include(x=> x.Difficulty).Include(x=> x.Region).ToListAsync();

        }

        public async Task<Walks?> GetByIdAsync(Guid id)
        {
            return await DbContext.walks
                .Include("Difficulty")
                .Include("Region")
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walks> Get(Guid id) => await DbContext.walks.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Walks?> UpdateAsync(Guid id, Walks walks)
        {
            var existingId = await DbContext.walks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingId == null)
            {
                return null;
            }
            existingId.Type = walks.Type;
            existingId.Id = id;
            existingId.Region = walks.Region;
            existingId.Name = walks.Name;
            existingId.Description = walks.Description;
            await DbContext.SaveChangesAsync();
            return existingId;

        }
    }
}
