using Data4SalesChallenge.Entities;

namespace Data4SalesChallenge.Repository
{
    public interface IVehiclesRepository
    {
        public Task<IEnumerable<Vehicles>> GetVehicles();
        public Task<Vehicles> GetVehicles(int id);
        public Task<Vehicles> CreateVehicles(VehiclesDto vehicles);
        public Task UpdateVehicles(int id, VehiclesDto vehicles);
        public Task DeleteVehicles(int id);
    }
}
