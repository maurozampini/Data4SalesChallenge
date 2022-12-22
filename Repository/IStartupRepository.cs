using Newtonsoft.Json.Linq;

namespace Data4SalesChallenge.Repository
{
    public interface IStartupRepository
    {
        public Task<string> GetEntitiesAndCreateTables();
    }
}
