using Data4SalesChallenge.Entities;
using Data4SalesChallenge.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Data4SalesChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeciesController : ControllerBase
    {
        private readonly ISpeciesRepository _speciessRepository;

        public SpeciesController(ISpeciesRepository speciessRepository) => _speciessRepository = speciessRepository;

        [HttpGet]
        public async Task<IActionResult> GetSpecies()
        {
            IEnumerable<Species> speciess = await _speciessRepository.GetSpecies();

            return Ok(speciess);
        }

        [HttpGet("{id}", Name = "SpeciesById")]
        public async Task<IActionResult> GetSpecies(int id)
        {
            Species species = await _speciessRepository.GetSpecies(id);
            if (species is null)
                return NotFound();

            return Ok(species);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSpecies([FromBody] SpeciesDto species)
        {
            Species createdSpecies = await _speciessRepository.CreateSpecies(species);

            return CreatedAtRoute("SpeciesById", new { id = createdSpecies.SpeciesID }, createdSpecies);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSpecies(int id, [FromBody] SpeciesDto species)
        {
            Species dbSpecies = await _speciessRepository.GetSpecies(id);
            if (dbSpecies is null)
                return NotFound();

            await _speciessRepository.UpdateSpecies(id, species);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecies(int id)
        {
            Species dbSpecies = await _speciessRepository.GetSpecies(id);
            if (dbSpecies is null)
                return NotFound();

            await _speciessRepository.DeleteSpecies(id);

            return Ok();
        }
    }
}
