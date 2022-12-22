using Data4SalesChallenge.Entities;

namespace Data4SalesChallenge.Repository
{
    public interface IStarshipsRepository
    {
        public Task<IEnumerable<Starships>> GetStarships();
        public Task<Starships> GetStarships(int id);
        public Task<Starships> CreateStarships(StarshipsDto starships);
        public Task UpdateStarships(int id, StarshipsDto starships);
        public Task DeleteStarships(int id);
    }
}
