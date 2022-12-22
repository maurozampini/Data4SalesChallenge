using Dapper;
using Data4SalesChallenge.Context;
using Data4SalesChallenge.Entities;
using System.Data;

namespace Data4SalesChallenge.Repository
{
    public class VehiclesRepository : IVehiclesRepository
    {
        private readonly DBContext _context;

        public VehiclesRepository(DBContext context) => _context = context;

        public async Task<IEnumerable<Vehicles>> GetVehicles()
        {
            Vehicles vehicles = new();
            string query = vehicles.SelectAllRecords();

            using IDbConnection connection = _context.CreateConnection();
            IEnumerable<Vehicles> result = await connection.QueryAsync<Vehicles>(query);
            connection.Close();

            return result.ToList();
        }

        public async Task<Vehicles> GetVehicles(int id)
        {
            Vehicles vehicles = new();
            string query = vehicles.SelectRecord(nameof(vehicles.VehiclesID));

            using IDbConnection connection = _context.CreateConnection();
            Vehicles result = await connection.QuerySingleOrDefaultAsync<Vehicles>(query, new { VehiclesID = id });
            connection.Close();

            return result;
        }

        public async Task<Vehicles> CreateVehicles(VehiclesDto vehicles)
        {
            string query = vehicles.InsertStatement();

            DynamicParameters parameters = new();
            parameters.Add("Name", vehicles.Name, DbType.String);
            parameters.Add("Cargo_capacity", vehicles.Cargo_capacity, DbType.String);
            parameters.Add("Consumables", vehicles.Consumables, DbType.String);
            parameters.Add("Cost_in_credits", vehicles.Cost_in_credits, DbType.String);
            parameters.Add("Crew", vehicles.Crew, DbType.String);
            parameters.Add("Length", vehicles.Length, DbType.String);
            parameters.Add("Manufacturer", vehicles.Manufacturer, DbType.String);
            parameters.Add("Max_atmosphering_speed", vehicles.Max_atmosphering_speed, DbType.String);
            parameters.Add("Model", vehicles.Model, DbType.String);
            parameters.Add("Passengers", vehicles.Passengers, DbType.String);
            //parameters.Add("Films", vehicles.Films, DbType.String);
            //parameters.Add("Pilots", vehicles.Pilots, DbType.String);
            parameters.Add("Vehicle_class", vehicles.Vehicle_class, DbType.String);
            parameters.Add("Created", vehicles.Created, DbType.String);
            parameters.Add("Edited", vehicles.Edited, DbType.String);
            parameters.Add("Url", vehicles.Url, DbType.String);

            using IDbConnection connection = _context.CreateConnection();
            int id = await connection.QuerySingleAsync<int>(query, parameters);

            Vehicles createdVehicles = new()
            {
                VehiclesID = id,
                Name = vehicles.Name,
                Cargo_capacity = vehicles.Cargo_capacity,
                Consumables = vehicles.Consumables,
                Cost_in_credits = vehicles.Cost_in_credits,
                Crew = vehicles.Crew,
                Length = vehicles.Length,
                Manufacturer = vehicles.Manufacturer,
                Max_atmosphering_speed = vehicles.Max_atmosphering_speed,
                Model = vehicles.Model,
                Passengers = vehicles.Passengers,
                //Films = vehicles.Films,
                //Pilots = vehicles.Pilots,
                Vehicle_class = vehicles.Vehicle_class,
                Created = vehicles.Created,
                Edited = vehicles.Edited,
                Url = vehicles.Url
            };

            connection.Close();

            return createdVehicles;
        }

        public async Task UpdateVehicles(int id, VehiclesDto vehicles)
        {
            string query = vehicles.UpdateStatement(nameof(Vehicles.VehiclesID));

            DynamicParameters parameters = new();
            parameters.Add("VehiclesID", id, DbType.Int32);
            parameters.Add("Name", vehicles.Name, DbType.String);
            parameters.Add("Cargo_capacity", vehicles.Cargo_capacity, DbType.String);
            parameters.Add("Consumables", vehicles.Consumables, DbType.String);
            parameters.Add("Cost_in_credits", vehicles.Cost_in_credits, DbType.String);
            parameters.Add("Crew", vehicles.Crew, DbType.String);
            parameters.Add("Length", vehicles.Length, DbType.String);
            parameters.Add("Manufacturer", vehicles.Manufacturer, DbType.String);
            parameters.Add("Max_atmosphering_speed", vehicles.Max_atmosphering_speed, DbType.String);
            parameters.Add("Model", vehicles.Model, DbType.String);
            parameters.Add("Passengers", vehicles.Passengers, DbType.String);
            //parameters.Add("Films", vehicles.Films, DbType.String);
            //parameters.Add("Pilots", vehicles.Pilots, DbType.String);
            parameters.Add("Vehicle_class", vehicles.Vehicle_class, DbType.String);
            parameters.Add("Created", vehicles.Created, DbType.String);
            parameters.Add("Edited", vehicles.Edited, DbType.String);
            parameters.Add("Url", vehicles.Url, DbType.String);

            using IDbConnection connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);

            connection.Close();
        }

        public async Task DeleteVehicles(int id)
        {
            Vehicles vehicles = new();
            string query = vehicles.DeleteStatement();

            using IDbConnection connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, new { VehiclesID = id });

            connection.Close();
        }
    }
}
