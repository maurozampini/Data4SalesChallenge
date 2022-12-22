using Data4SalesChallenge.Entities;
using Data4SalesChallenge.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Data4SalesChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        private readonly IFilmsRepository _filmsRepository;

        public FilmsController(IFilmsRepository filmsRepository) => _filmsRepository = filmsRepository;

        [HttpGet]
        public async Task<IActionResult> GetFilms()
        {
            var films = await _filmsRepository.GetFilms();

            return Ok(films);
        }

        [HttpGet("{id}", Name = "FilmById")]
        public async Task<IActionResult> GetFilm(int id)
        {
            Films film = await _filmsRepository.GetFilm(id);
            if (film is null)
                return NotFound();

            return Ok(film);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFilm([FromBody] FilmsDto film)
        {
            Films createdFilm = await _filmsRepository.CreateFilm(film);

            return CreatedAtRoute("FilmById", new { id = createdFilm.FilmsID }, createdFilm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFilm(int id, [FromBody] FilmsDto film)
        {
            Films dbFilm = await _filmsRepository.GetFilm(id);
            if (dbFilm is null)
                return NotFound();

            await _filmsRepository.UpdateFilm(id, film);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilm(int id)
        {
            Films dbFilm = await _filmsRepository.GetFilm(id);
            if (dbFilm is null)
                return NotFound();

            await _filmsRepository.DeleteFilm(id);

            return Ok();
        }
    }
}
