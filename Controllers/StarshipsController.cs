using Data4SalesChallenge.Entities;
using Data4SalesChallenge.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Data4SalesChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StarshipsController : ControllerBase
    {
        private readonly IStarshipsRepository _starshipssRepository;

        public StarshipsController(IStarshipsRepository starshipssRepository) => _starshipssRepository = starshipssRepository;

        [HttpGet]
        public async Task<IActionResult> GetStarships()
        {
            IEnumerable<Starships> starshipss = await _starshipssRepository.GetStarships();

            return Ok(starshipss);
        }

        [HttpGet("{id}", Name = "StarshipsById")]
        public async Task<IActionResult> GetStarships(int id)
        {
            Starships starships = await _starshipssRepository.GetStarships(id);
            if (starships is null)
                return NotFound();

            return Ok(starships);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStarships([FromBody] StarshipsDto starships)
        {
            Starships createdStarships = await _starshipssRepository.CreateStarships(starships);

            return CreatedAtRoute("StarshipsById", new { id = createdStarships.StarshipsID }, createdStarships);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStarships(int id, [FromBody] StarshipsDto starships)
        {
            Starships dbStarships = await _starshipssRepository.GetStarships(id);
            if (dbStarships is null)
                return NotFound();

            await _starshipssRepository.UpdateStarships(id, starships);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStarships(int id)
        {
            Starships dbStarships = await _starshipssRepository.GetStarships(id);
            if (dbStarships is null)
                return NotFound();

            await _starshipssRepository.DeleteStarships(id);

            return Ok();
        }
    }
}
