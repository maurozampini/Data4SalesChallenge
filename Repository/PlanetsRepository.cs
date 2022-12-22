using Dapper;
using Data4SalesChallenge.Context;
using Data4SalesChallenge.Entities;
using System.Data;

namespace Data4SalesChallenge.Repository
{
    public class PlanetsRepository : IPlanetsRepository
    {
        private readonly DBContext _context;

        public PlanetsRepository(DBContext context) => _context = context;

        public async Task<IEnumerable<Planets>> GetPlanets()
        {
            string query = "SELECT * FROM Planets";

            using IDbConnection connection = _context.CreateConnection();
            IEnumerable<Planets> planets = await connection.QueryAsync<Planets>(query);
            connection.Close();

            return planets.ToList();
        }

        public async Task<Planets> GetPlanet(int id)
        {
            string query = "SELECT * FROM Planets WHERE PlanetID = @PlanetID";
            using IDbConnection connection = _context.CreateConnection();
            Planets planet = await connection.QuerySingleOrDefaultAsync<Planets>(query, new { PlanetID = id });
            connection.Close();

            return planet;
        }

        public async Task<Planets> CreatePlanet(PlanetsDto planet)
        {
            string query = planet.InsertStatement();

            DynamicParameters parameters = new();
            parameters.Add("Climate", planet.Climate, DbType.String);
            parameters.Add("Created", planet.Created, DbType.String);
            parameters.Add("Diameter", planet.Diameter, DbType.String);
            parameters.Add("Edited", planet.Edited, DbType.String);
            //parameters.Add("Films", planet.Films, DbType.String);
            parameters.Add("Gravity", planet.Gravity, DbType.String);
            parameters.Add("Name", planet.Name, DbType.String);
            parameters.Add("Name", planet.Name, DbType.String);
            parameters.Add("Orbital_period", planet.Orbital_period, DbType.String);
            parameters.Add("Population", planet.Population, DbType.String);
            //parameters.Add("Residentst", planet.Residentst, DbType.String);
            parameters.Add("Rotation_period", planet.Rotation_period, DbType.String);
            parameters.Add("Surface_water", planet.Surface_water, DbType.String);
            parameters.Add("Terrain", planet.Terrain, DbType.String);
            parameters.Add("Url", planet.Url, DbType.String);

            using IDbConnection connection = _context.CreateConnection();
            int id = await connection.QuerySingleAsync<int>(query, parameters);

            Planets createdPlanet = new()
            {
                PlanetID = id,
                Climate = planet.Climate,
                Created = planet.Created,
                Diameter = planet.Diameter,
                Edited = planet.Edited,
                //Films = planet.Films,
                Gravity = planet.Gravity,
                Name = planet.Name,
                Orbital_period = planet.Orbital_period,
                Population = planet.Population,
                //Residentst = planet.Residentst
                Rotation_period = planet.Rotation_period,
                Surface_water = planet.Surface_water,
                Terrain = planet.Terrain,
                Url = planet.Url
            };

            connection.Close();

            return createdPlanet;
        }

        public async Task UpdatePlanet(int id, PlanetsDto planet)
        {
            string query = planet.UpdateStatement(nameof(Planets.PlanetID));

            DynamicParameters parameters = new();
            parameters.Add("PlanetID", id, DbType.String);
            parameters.Add("Climate", planet.Climate, DbType.String);
            parameters.Add("Created", planet.Created, DbType.String);
            parameters.Add("Diameter", planet.Diameter, DbType.String);
            parameters.Add("Edited", planet.Edited, DbType.String);
            //parameters.Add("Films", planet.Films, DbType.String);
            parameters.Add("Gravity", planet.Gravity, DbType.String);
            parameters.Add("Name", planet.Name, DbType.String);
            parameters.Add("Name", planet.Name, DbType.String);
            parameters.Add("Orbital_period", planet.Orbital_period, DbType.String);
            parameters.Add("Population", planet.Population, DbType.String);
            //parameters.Add("Residentst", planet.Residentst, DbType.String);
            parameters.Add("Rotation_period", planet.Rotation_period, DbType.String);
            parameters.Add("Surface_water", planet.Surface_water, DbType.String);
            parameters.Add("Terrain", planet.Terrain, DbType.String);
            parameters.Add("Url", planet.Url, DbType.String);

            using IDbConnection connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);

            connection.Close();
        }

        public async Task DeletePlanet(int id)
        {
            Planets planet = new();
            string query = planet.DeleteStatement();

            using IDbConnection connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, new { PlanetID = id });

            connection.Close();
        }
    }
}
