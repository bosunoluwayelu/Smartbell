using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Smartbell.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RingtonesController : ControllerBase
    {
        private readonly IRingtoneRepository _repo;
        private readonly IMapper _mapper;

        public RingtonesController(IRingtoneRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: api/Configs
        [HttpGet]
        public async Task<IActionResult> GetRingtones()
        {
            var ringtones = _mapper.Map<IEnumerable<RingtoneResponseDto>>(await _repo.GetAsync());
            return (ringtones == null) ? NotFound() : Ok(ringtones);
        }

        // GET: api/Configs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRingtone(Guid id)
        {
            var ringtone = _mapper.Map<RingtoneResponseDto>(await _repo.GetByIdAsync(id));
            return (ringtone == null) ? NotFound() : Ok(ringtone);
        }

        // PUT: api/Configs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchRingtone(Guid id, RingtoneResponseDto ringtoneResponseDto)
        {
            try
            {
                if (id != ringtoneResponseDto.Id)
                    return BadRequest();

                var ringtoneToPatch = await _repo.GetByIdAsync(id);
                if (ringtoneToPatch == null)
                    return NotFound();

                ringtoneToPatch = _mapper.Map(ringtoneResponseDto, ringtoneToPatch);
                ringtoneToPatch.UpdatedBy = "dboUpdater";
                ringtoneToPatch.UpdatedDate = DateTime.Now;
                return Ok(_mapper.Map<RingtoneResponseDto>(await _repo.UpdateAsync(ringtoneToPatch)));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RingtoneExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //return NoContent();
        }

        // POST: api/Configs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Config>> PostRingtone([FromForm]CreateRingtoneDto ringtone)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var mappedRingtone = _mapper.Map<Ringtone>(ringtone);
                    mappedRingtone.CreatedBy = "dboCreator";
                    mappedRingtone.UpdatedBy = "dboCreator";
                    var createdRingtone = _mapper.Map<RingtoneResponseDto>(await _repo.CreateAsync(mappedRingtone));

                    //return Ok(createdConfig);
                    return CreatedAtAction("GetRingtone", new { id = createdRingtone.Id }, createdRingtone);
                }

                return BadRequest("Error creating Ringtone");
            }
            catch (Exception)
            {
                return new JsonResult("[PostRingtone error!]") { StatusCode = 500 };
            }
        }

        // DELETE: api/Configs/5
        [HttpPost("{id}")]
        public async Task<IActionResult> DeleteRingtone(Guid id)
        {
            var ringtone = await _repo.GetByIdAsync(id);

            if (ringtone == null)
                return NotFound();

            return Ok(_mapper.Map<RingtoneResponseDto>(await _repo.DeleteAsync(ringtone)));
        }

        private bool RingtoneExists(Guid id)
        {
            // return (_repo.GetAsync().(e => e.Id == id)).GetValueOrDefault();
            return (_repo.GetByIdAsync(id)) == null ? true : false;
        }
    }
}