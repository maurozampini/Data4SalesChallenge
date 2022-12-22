using Data4SalesChallenge.Entities;
using Data4SalesChallenge.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Data4SalesChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehiclesRepository _vehiclesRepository;

        public VehiclesController(IVehiclesRepository vehiclesRepository) => _vehiclesRepository = vehiclesRepository;

        [HttpGet]
        public async Task<IActionResult> GetVehicles()
        {
            IEnumerable<Vehicles> vehicles = await _vehiclesRepository.GetVehicles();

            return Ok(vehicles);
        }

        [HttpGet("{id}", Name = "VehiclesById")]
        public async Task<IActionResult> GetVehicles(int id)
        {
            Vehicles vehicles = await _vehiclesRepository.GetVehicles(id);
            if (vehicles is null)
                return NotFound();

            return Ok(vehicles);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicles([FromBody] VehiclesDto vehicles)
        {
            Vehicles createdVehicles = await _vehiclesRepository.CreateVehicles(vehicles);

            return CreatedAtRoute("VehiclesById", new { id = createdVehicles.VehiclesID }, createdVehicles);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicles(int id, [FromBody] VehiclesDto vehicles)
        {
            Vehicles dbVehicles = await _vehiclesRepository.GetVehicles(id);
            if (dbVehicles is null)
                return NotFound();

            await _vehiclesRepository.UpdateVehicles(id, vehicles);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicles(int id)
        {
            Vehicles dbVehicles = await _vehiclesRepository.GetVehicles(id);
            if (dbVehicles is null)
                return NotFound();

            await _vehiclesRepository.DeleteVehicles(id);

            return Ok();
        }
    }
}
