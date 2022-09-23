using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        // IEnumerable<Region> GetAll();
        

        //For Asynchronous code
       Task <IEnumerable<Region>> GetAllAsync();
    }
}
