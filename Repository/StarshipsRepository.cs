using Dapper;
using Data4SalesChallenge.Context;
using Data4SalesChallenge.Entities;
using System.Data;

namespace Data4SalesChallenge.Repository
{
    public class StarshipsRepository : IStarshipsRepository
    {
        private readonly DBContext _context;

        public StarshipsRepository(DBContext context) => _context = context;

        public async Task<IEnumerable<Starships>> GetStarships()
        {
            Starships starships = new();
            string query = starships.SelectAllRecords();

            using IDbConnection connection = _context.CreateConnection();
            IEnumerable<Starships> result = await connection.QueryAsync<Starships>(query);
            connection.Close();

            return result.ToList();
        }

        public async Task<Starships> GetStarships(int id)
        {
            Starships starships = new();
            string query = starships.SelectRecord(nameof(starships.StarshipsID));

            using IDbConnection connection = _context.CreateConnection();
            Starships result = await connection.QuerySingleOrDefaultAsync<Starships>(query, new { StarshipsID = id });
            connection.Close();

            return result;
        }

        public async Task<Starships> CreateStarships(StarshipsDto starships)
        {
            string query = starships.InsertStatement();

            DynamicParameters parameters = new();
            parameters.Add("Name", starships.Name, DbType.String);
            parameters.Add("MGLT", starships.MGLT, DbType.String);
            parameters.Add("Cargo_capacity", starships.Cargo_capacity, DbType.String);
            parameters.Add("Consumables", starships.Consumables, DbType.String);
            parameters.Add("Cost_in_credits", starships.Cost_in_credits, DbType.String);
            parameters.Add("Crew", starships.Crew, DbType.String);
            parameters.Add("Hyperdrive_rating", starships.Hyperdrive_rating, DbType.String);
            parameters.Add("Length", starships.Length, DbType.String);
            parameters.Add("Manufacturer", starships.Manufacturer, DbType.String);
            parameters.Add("Max_atmosphering_speed", starships.Max_atmosphering_speed, DbType.String);
            parameters.Add("Model", starships.Model, DbType.String);
            parameters.Add("Passengers", starships.Passengers, DbType.String);
            parameters.Add("Starship_class", starships.Starship_class, DbType.String);
            //parameters.Add("Films", starships.Films, DbType.String);
            //parameters.Add("Pilots", starships.Pilots, DbType.String);
            parameters.Add("Created", starships.Created, DbType.String);
            parameters.Add("Edited", starships.Edited, DbType.String);
            parameters.Add("Url", starships.Url, DbType.String);

            using IDbConnection connection = _context.CreateConnection();
            int id = await connection.QuerySingleAsync<int>(query, parameters);

            Starships createdStarships = new()
            {
                StarshipsID = id,
                Name = starships.Name,
                MGLT = starships.MGLT,
                Cargo_capacity = starships.Cargo_capacity,
                Consumables = starships.Consumables,
                Cost_in_credits = starships.Cost_in_credits,
                Crew = starships.Crew,
                Hyperdrive_rating = starships.Hyperdrive_rating,
                Length = starships.Length,
                Manufacturer = starships.Manufacturer,
                Max_atmosphering_speed = starships.Max_atmosphering_speed,
                Model = starships.Model,
                Passengers = starships.Passengers,
                Starship_class = starships.Starship_class,
                //Films = starships.Films,
                //Pilots = starships.Pilots,
                Created = starships.Created,
                Edited = starships.Edited,
                Url = starships.Url
            };

            connection.Close();

            return createdStarships;
        }

        public async Task UpdateStarships(int id, StarshipsDto starships)
        {
            string query = starships.UpdateStatement(nameof(Starships.StarshipsID));

            DynamicParameters parameters = new();
            parameters.Add("StarshipsID", id, DbType.Int32);
            parameters.Add("Name", starships.Name, DbType.String);
            parameters.Add("MGLT", starships.MGLT, DbType.String);
            parameters.Add("Cargo_capacity", starships.Cargo_capacity, DbType.String);
            parameters.Add("Consumables", starships.Consumables, DbType.String);
            parameters.Add("Cost_in_credits", starships.Cost_in_credits, DbType.String);
            parameters.Add("Crew", starships.Crew, DbType.String);
            parameters.Add("Hyperdrive_rating", starships.Hyperdrive_rating, DbType.String);
            parameters.Add("Length", starships.Length, DbType.String);
            parameters.Add("Manufacturer", starships.Manufacturer, DbType.String);
            parameters.Add("Max_atmosphering_speed", starships.Max_atmosphering_speed, DbType.String);
            parameters.Add("Model", starships.Model, DbType.String);
            parameters.Add("Passengers", starships.Passengers, DbType.String);
            parameters.Add("Starship_class", starships.Starship_class, DbType.String);
            //parameters.Add("Films", starships.Films, DbType.String);
            //parameters.Add("Pilots", starships.Pilots, DbType.String);
            parameters.Add("Created", starships.Created, DbType.String);
            parameters.Add("Edited", starships.Edited, DbType.String);
            parameters.Add("Url", starships.Url, DbType.String);

            using IDbConnection connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);

            connection.Close();
        }

        public async Task DeleteStarships(int id)
        {
            Starships starships = new();
            string query = starships.DeleteStatement();

            using IDbConnection connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, new { StarshipsID = id });

            connection.Close();
        }
    }
}
