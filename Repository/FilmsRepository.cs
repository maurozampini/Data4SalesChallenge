using Dapper;
using Data4SalesChallenge.Context;
using Data4SalesChallenge.Entities;
using System.Data;

namespace Data4SalesChallenge.Repository
{
    public class FilmsRepository : IFilmsRepository
    {
        private readonly DBContext _context;

        public FilmsRepository(DBContext context) => _context = context;

        public async Task<IEnumerable<Films>> GetFilms()
        {
            string query = "SELECT * FROM Films AS F INNER JOIN FilmsPeople on F.FilmsID = FilmsPeople.FilmsID INNER JOIN People on FilmsPeople.PeopleID = People.PeopleID";

            using IDbConnection connection = _context.CreateConnection();

            IEnumerable<Films> filmsWithPeople = await connection.QueryAsync<Films, People, Films>(query,
                (film, people) =>
                {
                    if (film.People == null)
                    {
                        film.People = new List<People>();
                    }
                    film.People.Add(people);
                    return film;
                },
                splitOn: "FilmsID");

            IEnumerable<Films> result = filmsWithPeople.GroupBy(p => p.FilmsID).Select(g =>
            {
                Films groupedPeople = g.First();
                groupedPeople.People = g.Select(p => p.People.SingleOrDefault()).ToList();
                return groupedPeople;
            });

            connection.Close();

            return result;
        }

        public async Task<Films> GetFilm(int id)
        {
            string query = "SELECT * FROM Films AS F INNER JOIN FilmsPeople on F.FilmsID = FilmsPeople.FilmsID INNER JOIN People on FilmsPeople.PeopleID = People.PeopleID";

            using IDbConnection connection = _context.CreateConnection();

            IEnumerable<Films> filmsWithPeople = await connection.QueryAsync<Films, People, Films>(query,
                (film, people) =>
                {
                    if (film.People == null)
                    {
                        film.People = new List<People>();
                    }
                    film.People.Add(people);
                    return film;
                },
                splitOn: "FilmsID");

            Films? result = filmsWithPeople.GroupBy(p => p.FilmsID).Select(g =>
            {
                Films groupedPeople = g.First();
                groupedPeople.People = g.Select(p => p.People.SingleOrDefault()).ToList();
                return groupedPeople;
            }).FirstOrDefault(p => p.FilmsID == id);

            return result;
        }

        public async Task<Films> CreateFilm(FilmsDto film)
        {
            string query = film.InsertStatement();

            DynamicParameters parameters = new();
            parameters.Add("Episode_id", film.Episode_id, DbType.Int32);
            parameters.Add("Title", film.Title, DbType.String);
            parameters.Add("Opening_crawl", film.Opening_crawl, DbType.String);
            parameters.Add("Director", film.Director, DbType.String);
            parameters.Add("Producer", film.Producer, DbType.String);
            parameters.Add("Release_date", film.Release_date, DbType.String);

            using IDbConnection connection = _context.CreateConnection();
            int id = await connection.QuerySingleAsync<int>(query, parameters);

            Films createdFilm = new()
            {
                FilmsID = id,
                Episode_id = film.Episode_id,
                Title = film.Title,
                Opening_crawl = film.Opening_crawl,
                Director = film.Director,
                Producer = film.Producer,
                Release_date = film.Release_date
            };

            connection.Close();

            return createdFilm;
        }

        public async Task UpdateFilm(int id, FilmsDto film)
        {
            string query = film.UpdateStatement(nameof(Films.Episode_id));

            DynamicParameters parameters = new();
            parameters.Add("FilmsID", id, DbType.Int32);
            parameters.Add("Episode_id", film.Episode_id, DbType.Int32);
            parameters.Add("Title", film.Title, DbType.String);
            parameters.Add("Opening_crawl", film.Opening_crawl, DbType.String);
            parameters.Add("Director", film.Director, DbType.String);
            parameters.Add("Producer", film.Producer, DbType.String);
            parameters.Add("Release_date", film.Release_date, DbType.String);

            using IDbConnection connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, parameters);

            connection.Close();
        }

        public async Task DeleteFilm(int id)
        {
            Films film = new();
            string query = film.DeleteStatement();

            using IDbConnection connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, new { FilmsID = id });

            connection.Close();
        }
    }
}
