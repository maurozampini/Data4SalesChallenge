using Dapper;
using Data4SalesChallenge.Context;
using Data4SalesChallenge.Entities;
using System.Data;

namespace Data4SalesChallenge.Repository
{
    public class PeopleRepository : IPeopleRepository
    {
        private readonly DBContext _context;

        public PeopleRepository(DBContext context) => _context = context;

        public async Task<IEnumerable<People>> GetPeople()
        {
            string query = "SELECT * FROM People";

            using (IDbConnection connection = _context.CreateConnection())
            {
                IEnumerable<People> people = await connection.QueryAsync<People>(query);
                connection.Close();
                return people.ToList();
            }
        }

        public async Task<People> GetPeople(int PeopleID)
        {
            string query = "SELECT * FROM People WHERE PeopleID = @PeopleID";
            using IDbConnection connection = _context.CreateConnection();
            People person = await connection.QuerySingleOrDefaultAsync<People>(query, new { PeopleID });
            connection.Close();

            return person;
        }

        public async Task<People> CreatePeople(PeopleDto people)
        {
            string query = people.InsertStatement();

            DynamicParameters parameters = new();
            parameters.Add("Name", people.Name, DbType.String);
            parameters.Add("Height", people.Height, DbType.String);
            parameters.Add("Mass", people.Mass, DbType.String);
            parameters.Add("Hair_color", people.Hair_color, DbType.String);
            parameters.Add("Skin_color", people.Skin_color, DbType.String);
            parameters.Add("Eye_color", people.Eye_color, DbType.String);
            parameters.Add("Birth_year", people.Birth_year, DbType.String);
            parameters.Add("Gender", people.Gender, DbType.String);
            parameters.Add("Homeworld", people.Homeworld, DbType.String);
            //parameters.Add("Residentst", people.Films, DbType.String);
            //parameters.Add("Rotation_period", people.Species, DbType.String);
            //parameters.Add("Surface_water", people.Vehicles, DbType.String);
            //parameters.Add("Terrain", people.Starships, DbType.String);
            parameters.Add("Url", people.Created, DbType.String);
            parameters.Add("Url", people.Edited, DbType.String);
            parameters.Add("Url", people.Url, DbType.String);

            using IDbConnection connection = _context.CreateConnection();
            int id = await connection.QuerySingleAsync<int>(query, parameters);

            People createdPeople = new()
            {
                PeopleID = id,
                Name = people.Name,
                Height = people.Height,
                Mass = people.Mass,
                Hair_color = people.Hair_color,
                Skin_color = people.Skin_color,
                Eye_color = people.Eye_color,
                Birth_year = people.Birth_year,
                Gender = people.Gender,
                Homeworld = people.Homeworld,
                Created = people.Created,
                Edited = people.Edited,
                Url = people.Url
            };

            connection.Close();

            return createdPeople;
        }

        public async Task UpdatePeople(int id, PeopleDto people)
        {
            string query = people.UpdateStatement(nameof(People.PeopleID));

            DynamicParameters parameters = new();
            parameters.Add("Name", people.Name, DbType.String);
            parameters.Add("Height", people.Height, DbType.String);
            parameters.Add("Mass", people.Mass, DbType.String);
            parameters.Add("Hair_color", people.Hair_color, DbType.String);
            parameters.Add("Skin_color", people.Skin_color, DbType.String);
            parameters.Add("Eye_color", people.Eye_color, DbType.String);
            parameters.Add("Birth_year", people.Birth_year, DbType.String);
            parameters.Add("Gender", people.Gender, DbType.String);
            parameters.Add("Homeworld", people.Homeworld, DbType.String);
            //parameters.Add("Residentst", people.Films, DbType.String);
            //parameters.Add("Rotation_period", people.Species, DbType.String);
            //parameters.Add("Surface_water", people.Vehicles, DbType.String);
            //parameters.Add("Terrain", people.Starships, DbType.String);
            parameters.Add("Url", people.Created, DbType.String);
            parameters.Add("Url", people.Edited, DbType.String);
            parameters.Add("Url", people.Url, DbType.String);

            using IDbConnection connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);

            connection.Close();
        }

        public async Task DeletePeople(int id)
        {
            People people = new();
            string query = people.DeleteStatement();

            using IDbConnection connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, new { PeopleID = id });

            connection.Close();
        }
    }
}
