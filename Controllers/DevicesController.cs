using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp1.DTO.Devices;
using WebApp1.Helpers;
using WebApp1.Interfaces;
using WebApp1.Models;

namespace WebApp1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly IDevicesRepo _repo;

        public DevicesController(IDevicesRepo repo)
        {
            _repo = repo;
        }

        // GET: api/Devices
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<Device>>> GetDevices([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var devices = await _repo.GetAllAsync(query);
            var deviceDto = devices.Select(d => new DeviceDto
            {
                Id = d.Id,
                Name = d.Name
            }).ToList();

            return Ok(deviceDto);
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<ActionResult<Device>> GetDeviceById(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var device = await _repo.GetByIdAsync(id);

            if (device == null)
            {
                return NotFound();
            }

            return Ok(new DeviceDto
            {
                Id = device.Id,
                Name = device.Name
            });
        }

        // PUT: api/Devices/5
        [HttpPut("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> PutDevice(Guid id, UpdateDeviceRequestDto device)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var deviceModel = await _repo.UpdateAsync(id, device);
            if (deviceModel == null)
            {
                return NotFound();
            }

            return Ok(new DeviceDto
            {
                Id = deviceModel.Id,
                Name = deviceModel.Name
            });
        }

        // POST: api/Devices
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Device>> PostDevice([FromBody] CreateDeviceDto device)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var deviceModel = new CreateDeviceDto
            {
                Name = device.Name
            };


            await _repo.CreateAsync(deviceModel);

            return CreatedAtAction(nameof(GetDeviceById), new { id = deviceModel.Id }, device);
        }

        // DELETE: api/Devices/5
        [HttpDelete("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> DeleteDevice(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var deviceModel = await _repo.DeleteAsync(id);
            if (deviceModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}