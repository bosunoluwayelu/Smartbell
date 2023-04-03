﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Smartbell.Api.Data;
//using Smartbell.Shared.Entities;

namespace Smartbell.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivityRepository _repo;
        private readonly IMapper _mapper;

        public ActivitiesController(IActivityRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: api/Configs
        [HttpGet]
        public async Task<IActionResult> GetActivities()
        {
            var activities = _mapper.Map<IEnumerable<ActivityResponseDto>>(await _repo.GetAsync());

            if (activities == null)
            {
                return NotFound();
            }

            return Ok(activities);
        }

        // GET: api/Configs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivity(Guid id)
        {
            var activity = _mapper.Map<ActivityResponseDto>(await _repo.GetByIdAsync(id));

            if (activity == null)
            {
                return NotFound();
            }

            return Ok(activity);
        }

        // PUT: api/Configs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchActivity(Guid id, ActivityResponseDto activityResponseDto)
        {
            if (id != activityResponseDto.Id)
                return BadRequest();

            try
            {
                var activityToPatch = await _repo.GetByIdAsync(id);
                if (activityToPatch == null)
                    return NotFound();

                activityToPatch = _mapper.Map(activityResponseDto, activityToPatch);
                activityToPatch.UpdatedBy = "dboUpdater";
                activityToPatch.UpdatedDate = DateTime.Now;
                return Ok(_mapper.Map<ActivityResponseDto>(await _repo.UpdateAsync(activityToPatch)));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityExists(id))
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
        public async Task<ActionResult<Config>> PostActivity(CreateActivityDto activity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var mappedActivity = _mapper.Map<Activity>(activity);
                    mappedActivity.CreatedBy = "dboCreator";
                    mappedActivity.UpdatedBy = "dboCreator";
                    var createdActivity = _mapper.Map<ActivityResponseDto>(await _repo.CreateAsync(mappedActivity));

                    //return Ok(createdConfig);
                    return CreatedAtAction("GetActivity", new { id = createdActivity.Id }, createdActivity);
                }

                return BadRequest("Error creating activity");
            }
            catch (Exception)
            {
                return new JsonResult("[PostActivity error!]") { StatusCode = 500 };
            }
        }

        // DELETE: api/Configs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(Guid id)
        {
            var activity = await _repo.GetByIdAsync(id);

            if (activity == null)
                return NotFound();

            return Ok(_mapper.Map<ActivityResponseDto>(await _repo.DeleteAsync(activity)));
        }

        private bool ActivityExists(Guid id)
        {
            // return (_repo.GetAsync().(e => e.Id == id)).GetValueOrDefault();
            return (_repo.GetByIdAsync(id)) == null ? true : false;
        }
    }
}