using NZWalksAPI.Controllers;
using NZWalksAPI.Models.Domain;
namespace NZWalksAPI.IRepository
{
    public interface IWalkRepository
    {
        
       Task<Walks> CreateAsync(Walks walks);
        Task<List<Walks>> GetAllAsync(string? FilterOn = null, string? FilterQuery = null, string? SortbyName = null , 
            bool IsAscending = true , int pageNumber = 1 , int pageSize = 1000);
        Task<Walks?> GetByIdAsync(Guid id);
        Task<Walks?> UpdateAsync( Guid id ,Walks walks);
        Task<Walks?> DeleteAsync(Guid id);
    
    }
}
