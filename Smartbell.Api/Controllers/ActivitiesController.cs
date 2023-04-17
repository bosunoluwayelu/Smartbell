using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
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
        private readonly IWebHostEnvironment _environment;

        public ActivitiesController(IActivityRepository repo, 
                                    IMapper mapper,
                                    IWebHostEnvironment environment)
        {
            _repo = repo;
            _mapper = mapper;
            _environment = environment;
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
        public async Task<ActionResult<Config>> PostActivity([FromForm] CreateActivityDto activity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // process file upload
                    //string webRootPath = this._environment.WebRootPath;
                    string contentPath = this._environment.ContentRootPath;
                    var imageFilePath = @"Resources\Images";
                    var videoFilePath = @"Resources\Videos";
                    var imagePath = Path.Combine(contentPath, imageFilePath);
                    var videoPath = Path.Combine(contentPath, videoFilePath);

                    string imageFileName = "";
                    string videoFileName = "";

					if (activity.ImageFilePath != null) {
						imageFileName = Path.GetFileName(activity.ImageFilePath.FileName);
						if (!Directory.Exists(imagePath)) Directory.CreateDirectory(imagePath);
						using var stream = new FileStream(Path.Combine(imagePath, imageFileName), FileMode.Create);
						activity.ImageFilePath.CopyTo(stream);
					}

					if (activity.VideoFilePath != null)
					{
						videoFileName = Path.GetFileName(activity.VideoFilePath.FileName);
						if (!Directory.Exists(videoPath)) Directory.CreateDirectory(videoPath);
						using var stream = new FileStream(Path.Combine(videoPath, videoFileName), FileMode.Create);
						activity.VideoFilePath.CopyTo(stream);
					}

                    //var mappedActivity = _mapper.Map<Activity>(activity);
                    //mappedActivity.CreatedBy = "dboCreator";
                    //mappedActivity.UpdatedBy = "dboCreator";

                    var model = new Activity
                    {
                        Description = activity.Description,
                        ImageFilePath = activity.ImageFilePath == null ? "" : "https://smrtbell.tellimart.com/resources/images/" + imageFileName, // Path.Combine(imagePath, imageFileName),
                        VideoFilePath = activity.VideoFilePath == null ? "" : "https://smrtbell.tellimart.com/resources/videos/" + videoFileName, // Path.Combine(videoPath, videoFileName),
						CreatedBy = "dboCreator",
						UpdatedBy = "dboCreator",
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
					};

                    //var createdActivity = _mapper.Map<ActivityResponseDto>(await _repo.CreateAsync(mappedActivity));
                    var createdActivity = _mapper.Map<ActivityResponseDto>(await _repo.CreateAsync(model));

                    //return Ok(createdConfig);
                    return CreatedAtAction("GetActivity", new { id = createdActivity.Id }, createdActivity);
                }

                return BadRequest("Error creating activity");
            }
            catch (Exception ex)
            {
                return new JsonResult($"[PostActivity error!] - {ex.Message}") { StatusCode = 500 };
            }
        }

        // DELETE: api/Configs/5
        [HttpPost("{id}")]
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
