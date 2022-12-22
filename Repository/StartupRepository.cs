using Dapper;
using Data4SalesChallenge.Context;
using Data4SalesChallenge.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;

namespace Data4SalesChallenge.Repository
{
    public class StartupRepository : IStartupRepository
    {
        private const string dbName = "mauro_starwars";
        private const string requestUri = "https://swapi.dev/api/";
        private static readonly HttpClient client = new();
        private readonly DBContext _context;
        public StartupRepository(DBContext context) => _context = context;

        private static string ConvertedJsonObject(bool error, string status, string message)
        {
            JObject jsonObject = new()
                {
                    { "Error", error },
                    { "Status", status },
                    { "Message", message }
                };

            return JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
        }

        public async Task<string> GetEntitiesAndCreateTables()
        {
            try
            {
                using HttpResponseMessage response = await client.GetAsync(requestUri);
                IEnumerable<JProperty>? propertiesList = JObject.Parse(response.Content.ReadAsStringAsync().Result).Properties();

                if (propertiesList != null)
                {
                    using (IDbConnection connection = _context.CreateConnection())
                    {
                        string dropQuery = "DROP TABLE IF EXISTS FilmsPeople; " +
                            "DROP TABLE IF EXISTS FilmsPlanets; ";

                        string createTableQuery = "CREATE TABLE FilmsPeople (FilmsID int NOT NULL, PeopleID int NOT NULL, PRIMARY KEY(FilmsID, PeopleID)); " +
                            "CREATE TABLE FilmsPlanets (FilmsID int NOT NULL, PlanetID int NOT NULL, PRIMARY KEY(FilmsID, PlanetID)); ";

                        await connection.ExecuteAsync($"{dropQuery} {createTableQuery}");
                        connection.Close();
                    }

                    foreach (JProperty property in propertiesList)
                    {
                        string? newRequestUri = property.Value.Value<string>();

                        HttpResponseMessage? newResponse = await client.GetAsync(newRequestUri);

                        switch (char.ToUpper(property.Path[0]) + property.Path[1..])
                        {
                            case nameof(Films):
                                Films film = new();

                                string relationCharacterQuery = "INSERT INTO FilmsPeople(FilmsID, PeopleID) VALUES ";
                                string relationPlanetsQuery = "INSERT INTO FilmsPlanets(FilmsID, PlanetID) VALUES ";

                                string dropQuery = film.DropStatement();

                                string createTableQuery = "CREATE TABLE Films (FilmsID int NOT NULL AUTO_INCREMENT PRIMARY KEY, Episode_id varchar(10), Title varchar(255), Opening_crawl varchar(1000)" +
                                    ", Director varchar(255), Producer varchar(255), Release_date varchar(255), Created varchar(255), Edited varchar(255), Url varchar(255));";

                                string insertQuery = film.InsertStatement();

                                Entity<Films>? responseFilms;
                                string? nextFilm = null;

                                do
                                {
                                    if (nextFilm != null)
                                    {
                                        newResponse = await client.GetAsync(nextFilm);
                                    }

                                    responseFilms = await newResponse.Content.ReadFromJsonAsync<Entity<Films>>();

                                    if (responseFilms != null)
                                    {
                                        foreach (Films item in responseFilms.Results)
                                        {
                                            string[] splitedUrl = item.Url.Split("/");
                                            string FilmsID = splitedUrl[splitedUrl.Length - 2];

                                            foreach (string character in item.Characters)
                                            {
                                                string[] splitedCharacter = character.Split("/");
                                                string resultado = splitedCharacter[splitedCharacter.Length - 2];
                                                relationCharacterQuery += $"({FilmsID}, {resultado}){(item.Characters.Last() != character ? ',' : string.Empty)}";
                                            }

                                            relationCharacterQuery += $"{(responseFilms.Results.Last() == item ? ';' : ',')}";

                                            foreach (string planet in item.Planets)
                                            {
                                                string[] splitedPlanet = planet.Split("/");
                                                string resultado = splitedPlanet[splitedPlanet.Length - 2];
                                                relationPlanetsQuery += $"({FilmsID}, {resultado}){(item.Planets.Last() != planet ? ',' : string.Empty)}";
                                            }

                                            relationPlanetsQuery += $"{(responseFilms.Results.Last() == item ? ';' : ',')}";
                                        }
                                    }

                                    nextFilm = responseFilms?.Next;
                                } while (nextFilm != null);

                                using (IDbConnection connection = _context.CreateConnection())
                                {
                                    await connection.ExecuteAsync($"{dropQuery} {createTableQuery}");
                                    connection.Close();
                                    await connection.ExecuteAsync(insertQuery, responseFilms.Results);
                                    connection.Close();
                                    await connection.ExecuteAsync(relationCharacterQuery + relationPlanetsQuery);
                                    connection.Close();
                                }

                                break;
                            case nameof(People):
                                Entity<People>? responsePeople;
                                string? nextPeople = null;
                                People people = new();
                                List<People> peopleToInsert = new();

                                string PeopleCreateTableQuery = "CREATE TABLE People (PeopleID int NOT NULL AUTO_INCREMENT PRIMARY KEY, Name varchar(255), Height varchar(255)" +
                                    ", Mass varchar(255), Hair_color varchar(255), Skin_color varchar(255), Eye_color varchar(255), Birth_year varchar(255)" +
                                    ", Gender varchar(255), Homeworld varchar(255), Created varchar(255), Edited varchar(255), Url varchar(255));";

                                do
                                {
                                    if (nextPeople != null)
                                    {
                                        newResponse = await client.GetAsync(nextPeople);
                                    }

                                    responsePeople = await newResponse.Content.ReadFromJsonAsync<Entity<People>>();

                                    if (responsePeople != null)
                                    {
                                        peopleToInsert.AddRange(responsePeople.Results.ToList());
                                        nextPeople = responsePeople?.Next;
                                    }
                                } while (nextPeople != null);

                                using (IDbConnection connection = _context.CreateConnection())
                                {
                                    await connection.ExecuteAsync($"{people.DropStatement()} {PeopleCreateTableQuery}");
                                    connection.Close();
                                    await connection.ExecuteAsync(people.InsertStatement(), peopleToInsert);
                                    connection.Close();
                                }

                                break;
                            case nameof(Planets):
                                Entity<Planets>? responsePlanets;
                                string? nextPlanets = null;
                                Planets planets = new();
                                List<Planets> planetsToInsert = new();

                                string PlanetsCreateTableQuery = "CREATE TABLE Planets (PlanetID int NOT NULL AUTO_INCREMENT PRIMARY KEY, Climate varchar(255), Created varchar(255)" +
                                    ", Diameter varchar(255), Edited varchar(255), Gravity varchar(255), Name varchar(255), Orbital_period varchar(255)" +
                                    ", Population varchar(255), Rotation_period varchar(255), Surface_water varchar(255), Terrain varchar(255), Url varchar(255));";

                                do
                                {
                                    if (nextPlanets != null)
                                    {
                                        newResponse = await client.GetAsync(nextPlanets);
                                    }

                                    responsePlanets = await newResponse.Content.ReadFromJsonAsync<Entity<Planets>>();

                                    if (responsePlanets != null)
                                    {
                                        planetsToInsert.AddRange(responsePlanets.Results.ToList());
                                        nextPlanets = responsePlanets?.Next;
                                    }
                                } while (nextPlanets != null);

                                using (IDbConnection connection = _context.CreateConnection())
                                {
                                    await connection.ExecuteAsync($"{planets.DropStatement()} {PlanetsCreateTableQuery}");
                                    connection.Close();
                                    await connection.ExecuteAsync(planets.InsertStatement(), planetsToInsert);
                                    connection.Close();
                                }

                                break;
                            case nameof(Species):
                                Entity<Species>? responseSpecies;
                                string? nextSpecies = null;
                                Species species = new();
                                List<Species> speciesToInsert = new();

                                string speciesCreateTableQuery = "CREATE TABLE Species (SpeciesID int NOT NULL AUTO_INCREMENT PRIMARY KEY, Name varchar(255), Average_height varchar(255)" +
                                    ", Average_lifespan varchar(255), Classification varchar(255), Designation varchar(255), Eye_colors varchar(255), Hair_colors varchar(255)" +
                                    ", Homeworld varchar(255), Language varchar(255), Skin_colors varchar(255), Created varchar(255), Edited varchar(255), Url varchar(255));";

                                do
                                {
                                    if (nextSpecies != null)
                                    {
                                        newResponse = await client.GetAsync(nextSpecies);
                                    }

                                    responseSpecies = await newResponse.Content.ReadFromJsonAsync<Entity<Species>>();

                                    if (responseSpecies != null)
                                    {
                                        speciesToInsert.AddRange(responseSpecies.Results.ToList());
                                        nextSpecies = responseSpecies?.Next;
                                    }
                                } while (nextSpecies != null);

                                using (IDbConnection connection = _context.CreateConnection())
                                {
                                    await connection.ExecuteAsync($"{species.DropStatement()} {speciesCreateTableQuery}");
                                    connection.Close();
                                    await connection.ExecuteAsync(species.InsertStatement(), speciesToInsert);
                                    connection.Close();
                                }

                                break;
                            case nameof(Starships):
                                Entity<Starships>? responseStarships;
                                string? nextStarships = null;
                                Starships starships = new();
                                List<Starships> starshipsToInsert = new();

                                string starshipsCreateTableQuery = "CREATE TABLE Starships (StarshipsID int NOT NULL AUTO_INCREMENT PRIMARY KEY, Name varchar(255), MGLT varchar(255)" +
                                    ", Cargo_capacity varchar(255), Consumables varchar(255), Cost_in_credits varchar(255), Crew varchar(255), Hyperdrive_rating varchar(255)" +
                                    ", Length varchar(255), Manufacturer varchar(255), Max_atmosphering_speed varchar(255), Model varchar(255), Passengers varchar(255), Starship_class varchar(255)" +
                                    ", Created varchar(255), Edited varchar(255), Url varchar(255));";

                                do
                                {
                                    if (nextStarships != null)
                                    {
                                        newResponse = await client.GetAsync(nextStarships);
                                    }

                                    responseStarships = await newResponse.Content.ReadFromJsonAsync<Entity<Starships>>();

                                    if (responseStarships != null)
                                    {
                                        starshipsToInsert.AddRange(responseStarships.Results.ToList());
                                        nextStarships = responseStarships?.Next;
                                    }
                                } while (nextStarships != null);

                                using (IDbConnection connection = _context.CreateConnection())
                                {
                                    await connection.ExecuteAsync($"{starships.DropStatement()} {starshipsCreateTableQuery}");
                                    connection.Close();
                                    await connection.ExecuteAsync(starships.InsertStatement(), starshipsToInsert);
                                    connection.Close();
                                }

                                break;
                            case nameof(Vehicles):
                                Entity<Vehicles>? responseVehicles;
                                string? nextVehicles = null;
                                Vehicles vehicles = new();
                                List<Vehicles> vehiclesToInsert = new();

                                string vehiclesCreateTableQuery = "CREATE TABLE Vehicles (VehiclesID int NOT NULL AUTO_INCREMENT PRIMARY KEY, Name varchar(255)" +
                                    ", Cargo_capacity varchar(255), Consumables varchar(255), Cost_in_credits varchar(255), Crew varchar(255)" +
                                    ", Length varchar(255), Manufacturer varchar(255), Max_atmosphering_speed varchar(255), Model varchar(255), Passengers varchar(255)" +
                                    ", Vehicle_class varchar(255), Created varchar(255), Edited varchar(255), Url varchar(255));";

                                do
                                {
                                    if (nextVehicles != null)
                                    {
                                        newResponse = await client.GetAsync(nextVehicles);
                                    }

                                    responseVehicles = await newResponse.Content.ReadFromJsonAsync<Entity<Vehicles>>();

                                    if (responseVehicles != null)
                                    {
                                        vehiclesToInsert.AddRange(responseVehicles.Results.ToList());
                                        nextVehicles = responseVehicles?.Next;
                                    }
                                } while (nextVehicles != null);

                                using (IDbConnection connection = _context.CreateConnection())
                                {
                                    await connection.ExecuteAsync($"{vehicles.DropStatement()} {vehiclesCreateTableQuery}");
                                    connection.Close();
                                    await connection.ExecuteAsync(vehicles.InsertStatement(), vehiclesToInsert);
                                    connection.Close();
                                }

                                break;
                            default:
                                break;
                        }
                    }
                }

                return ConvertedJsonObject(false, "Success", "All tables and records have been created.");
            }
            catch (Exception e)
            {
                return ConvertedJsonObject(true, "Failure", e.Message);
            }
        }
    }
}
