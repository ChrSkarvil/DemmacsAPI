using DemmacsAPIv2.Data.Entities;

namespace DemmacsAPIv2.Repositories
{
    public interface IManufacturerRepository
    {
        Task<Manufacturer[]> GetAllManufacturersAsync();
        Task<Manufacturer> GetManufacturerAsync(int id);
    }
}
