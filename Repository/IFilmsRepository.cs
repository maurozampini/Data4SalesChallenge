using Data4SalesChallenge.Entities;

namespace Data4SalesChallenge.Repository
{
    public interface IFilmsRepository
    {
        public Task<IEnumerable<Films>> GetFilms();
        public Task<Films> GetFilm(int id);
        public Task<Films> CreateFilm(FilmsDto film);
        public Task UpdateFilm(int id, FilmsDto film);
        public Task DeleteFilm(int id);
    }
}
