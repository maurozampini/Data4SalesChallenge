using Data4SalesChallenge.Entities;

namespace Data4SalesChallenge.Repository
{
    public interface ISpeciesRepository
    {
        public Task<IEnumerable<Species>> GetSpecies();
        public Task<Species> GetSpecies(int id);
        public Task<Species> CreateSpecies(SpeciesDto species);
        public Task UpdateSpecies(int id, SpeciesDto species);
        public Task DeleteSpecies(int id);
    }
}
