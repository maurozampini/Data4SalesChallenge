using Data4SalesChallenge.Entities;
using Data4SalesChallenge.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Data4SalesChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanetsController : ControllerBase
    {
        private readonly IPlanetsRepository _planetsRepository;

        public PlanetsController(IPlanetsRepository planetsRepository) => _planetsRepository = planetsRepository;

        [HttpGet]
        public async Task<IActionResult> GetPlanets()
        {
            IEnumerable<Planets> planets = await _planetsRepository.GetPlanets();

            return Ok(planets);
        }

        [HttpGet("{id}", Name = "PlanetById")]
        public async Task<IActionResult> GetPlanet(int id)
        {
            Planets planets = await _planetsRepository.GetPlanet(id);
            if (planets is null)
                return NotFound();

            return Ok(planets);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlanet([FromBody] PlanetsDto planets)
        {
            Planets createdPlanet = await _planetsRepository.CreatePlanet(planets);

            return CreatedAtRoute("PlanetById", new { id = createdPlanet.PlanetID }, createdPlanet);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlanet(int id, [FromBody] PlanetsDto planets)
        {
            Planets dbPlanet = await _planetsRepository.GetPlanet(id);
            if (dbPlanet is null)
                return NotFound();

            await _planetsRepository.UpdatePlanet(id, planets);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlanet(int id)
        {
            Planets dbPlanet = await _planetsRepository.GetPlanet(id);
            if (dbPlanet is null)
                return NotFound();

            await _planetsRepository.DeletePlanet(id);

            return Ok();
        }
    }
}
