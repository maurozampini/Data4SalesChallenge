using Data4SalesChallenge.Entities;
using Data4SalesChallenge.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Data4SalesChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleRepository _peopleRepository;

        public PeopleController(IPeopleRepository peopleRepository) => _peopleRepository = peopleRepository;

        [HttpGet]
        public async Task<IActionResult> GetPeople()
        {
            IEnumerable<People> people = await _peopleRepository.GetPeople();

            return Ok(people);
        }

        [HttpGet("{id}", Name = "PersonById")]
        public async Task<IActionResult> GetPerson(int id)
        {
            People person = await _peopleRepository.GetPeople(id);
            if (person is null)
                return NotFound();

            return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePerson([FromBody] PeopleDto people)
        {
            People createdPerson = await _peopleRepository.CreatePeople(people);

            return CreatedAtRoute("PeopleById", new { createdPerson.PeopleID }, createdPerson);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson(int id, [FromBody] PeopleDto person)
        {
            People dbPerson = await _peopleRepository.GetPeople(id);
            if (dbPerson is null)
                return NotFound();

            await _peopleRepository.UpdatePeople(id, person);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            People dbPeople = await _peopleRepository.GetPeople(id);
            if (dbPeople is null)
                return NotFound();

            await _peopleRepository.DeletePeople(id);

            return Ok();
        }
    }
}
