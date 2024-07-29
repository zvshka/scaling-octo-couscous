using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp1.Data;
using WebApp1.Helpers;
using WebApp1.Interfaces;
using WebApp1.Models;

namespace WebApp1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementsController : ControllerBase
    {
        private readonly IMeasurementsRepo _repo;

        public MeasurementsController(IMeasurementsRepo repo)
        {
            _repo = repo;
        }

        // GET: api/Measurements/5
        [HttpGet("{deviceId:guid}")]
        [Authorize]
        public async Task<ActionResult<List<Measurement>>> GetMeasurementsByDeviceId(Guid deviceId, [FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var measurements = await _repo.GetByDeviceIdAsync(deviceId, query);

            return Ok(measurements);
        }
    }
}