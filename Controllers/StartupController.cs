using Data4SalesChallenge.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Data4SalesChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StartupController : ControllerBase
    {
        private readonly IStartupRepository _startupRepository;

        public StartupController(IStartupRepository startupRepository) => _startupRepository = startupRepository;

        [HttpGet]
        public async Task<IActionResult> GetEntitiesAndCreateTables()
        {
            string startup = await _startupRepository.GetEntitiesAndCreateTables();

            return Ok(startup);
        }
    }
}
