using Dapper;
using Data4SalesChallenge.Context;
using Data4SalesChallenge.Entities;
using System.Data;

namespace Data4SalesChallenge.Repository
{
    public class SpeciesRepository : ISpeciesRepository
    {
        private readonly DBContext _context;

        public SpeciesRepository(DBContext context) => _context = context;

        public async Task<IEnumerable<Species>> GetSpecies()
        {
            Species species = new();
            string query = species.SelectAllRecords();

            using IDbConnection connection = _context.CreateConnection();
            IEnumerable<Species> result = await connection.QueryAsync<Species>(query);
            connection.Close();

            return result.ToList();
        }

        public async Task<Species> GetSpecies(int id)
        {
            Species species = new();
            string query = species.SelectRecord(nameof(species.SpeciesID));

            using IDbConnection connection = _context.CreateConnection();
            Species result = await connection.QuerySingleOrDefaultAsync<Species>(query, new { SpeciesID = id });
            connection.Close();

            return result;
        }

        public async Task<Species> CreateSpecies(SpeciesDto species)
        {
            string query = species.InsertStatement();

            DynamicParameters parameters = new();
            parameters.Add("Name", species.Name, DbType.String);
            parameters.Add("Average_height", species.Average_height, DbType.String);
            parameters.Add("Average_lifespan", species.Average_lifespan, DbType.String);
            parameters.Add("Classification", species.Classification, DbType.String);
            parameters.Add("Designation", species.Designation, DbType.String);
            parameters.Add("Eye_colors", species.Eye_colors, DbType.String);
            parameters.Add("Hair_colors", species.Hair_colors, DbType.String);
            parameters.Add("Homeworld", species.Homeworld, DbType.String);
            parameters.Add("Language", species.Language, DbType.String);
            //parameters.Add("people", species.people, DbType.String);
            //parameters.Add("films", species.films, DbType.String);
            parameters.Add("Skin_colors", species.Skin_colors, DbType.String);
            parameters.Add("Created", species.Created, DbType.String);
            parameters.Add("Edited", species.Edited, DbType.String);
            parameters.Add("Url", species.Url, DbType.String);

            using IDbConnection connection = _context.CreateConnection();
            int id = await connection.QuerySingleAsync<int>(query, parameters);

            Species createdSpecies = new()
            {
                SpeciesID = id,
                Name = species.Name,
                Average_height = species.Average_height,
                Average_lifespan = species.Average_lifespan,
                Classification = species.Classification,
                Designation = species.Designation,
                Eye_colors = species.Eye_colors,
                Hair_colors = species.Hair_colors,
                Homeworld = species.Homeworld,
                Language = species.Language,
                //people = species.people,
                //films = species.films,
                Skin_colors = species.Skin_colors,
                Created = species.Created,
                Edited = species.Edited,
                Url = species.Url
            };

            connection.Close();

            return createdSpecies;
        }

        public async Task UpdateSpecies(int id, SpeciesDto species)
        {
            string query = species.UpdateStatement(nameof(Species.SpeciesID));

            DynamicParameters parameters = new();
            parameters.Add("SpeciesID", id, DbType.Int32);
            parameters.Add("Name", species.Name, DbType.String);
            parameters.Add("Average_height", species.Average_height, DbType.String);
            parameters.Add("Average_lifespan", species.Average_lifespan, DbType.String);
            parameters.Add("Classification", species.Classification, DbType.String);
            parameters.Add("Designation", species.Designation, DbType.String);
            parameters.Add("Eye_colors", species.Eye_colors, DbType.String);
            parameters.Add("Hair_colors", species.Hair_colors, DbType.String);
            parameters.Add("Homeworld", species.Homeworld, DbType.String);
            parameters.Add("Language", species.Language, DbType.String);
            //parameters.Add("people", species.people, DbType.String);
            //parameters.Add("films", species.films, DbType.String);
            parameters.Add("Skin_colors", species.Skin_colors, DbType.String);
            parameters.Add("Created", species.Created, DbType.String);
            parameters.Add("Edited", species.Edited, DbType.String);
            parameters.Add("Url", species.Url, DbType.String);

            using IDbConnection connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);

            connection.Close();
        }

        public async Task DeleteSpecies(int id)
        {
            Species species = new();
            string query = species.DeleteStatement();

            using IDbConnection connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, new { SpeciesID = id });

            connection.Close();
        }
    }
}
