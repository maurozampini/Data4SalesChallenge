using Data4SalesChallenge.Entities;

namespace Data4SalesChallenge.Repository
{
    public interface IPlanetsRepository
    {
        public Task<IEnumerable<Planets>> GetPlanets();
        public Task<Planets> GetPlanet(int id);
        public Task<Planets> CreatePlanet(PlanetsDto planet);
        public Task UpdatePlanet(int id, PlanetsDto planet);
        public Task DeletePlanet(int id);
    }
}
