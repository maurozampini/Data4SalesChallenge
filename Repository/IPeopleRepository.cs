using Data4SalesChallenge.Entities;

namespace Data4SalesChallenge.Repository
{
    public interface IPeopleRepository
    {
        public Task<IEnumerable<People>> GetPeople();
        public Task<People> GetPeople(int id);
        public Task<People> CreatePeople(PeopleDto people);
        public Task UpdatePeople(int id, PeopleDto film);
        public Task DeletePeople(int id);
    }
}
