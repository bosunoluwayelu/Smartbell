using System;
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
    public class ConfigsController : ControllerBase
    {
        private readonly IConfigRepository _repo;
        private readonly IMapper _mapper;

        public ConfigsController(IConfigRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: api/Configs
        [HttpGet]
        public async Task<IActionResult> GetConfigs()
        {
            var configs = _mapper.Map<IEnumerable<ConfigResponseDto>>(await _repo.GetAsync());

            if (configs == null)
            {
                return NotFound();
            }

            return Ok(configs);
        }

        // GET: api/Configs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetConfig(Guid id)
        {
            //var configs = await _repo.GetAsync();

            //if (configs == null)
            //{
            //    return NotFound();
            //}
            var config = _mapper.Map<ConfigResponseDto>(await _repo.GetByIdAsync(id));

            if (config == null)
            {
                return NotFound();
            }

            return Ok(config);
        }

        // PUT: api/Configs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchConfig(Guid id, ConfigResponseDto configResponseDto)
        {
            if (id != configResponseDto.Id)
                return BadRequest();

            try
            {
                var configToPatch = await _repo.GetByIdAsync(id);
                if (configToPatch == null)
                    return NotFound();

                configToPatch = _mapper.Map(configResponseDto, configToPatch);
                configToPatch.UpdatedBy = "dboUpdater";
                configToPatch.UpdatedDate = DateTime.Now;
                return Ok(_mapper.Map<ConfigResponseDto>(await _repo.UpdateAsync(configToPatch)));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConfigExists(id))
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
        public async Task<ActionResult<Config>> PostConfig(CreateConfigDto config)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var mappedConfig = _mapper.Map<Config>(config);
                    mappedConfig.CreatedBy = "dboCreator";
                    mappedConfig.UpdatedBy = "dboCreator";
                    var createdConfig = _mapper.Map<ConfigResponseDto>(await _repo.CreateAsync(mappedConfig));

                    //return Ok(createdConfig);
                    return CreatedAtAction("GetConfig", new { id = createdConfig.Id }, createdConfig);
                }

                return BadRequest("Error creating config");
            }
            catch (Exception)
            {
                return new JsonResult("[PostConfig error!]") { StatusCode = 500 };
            }
        }

        // DELETE: api/Configs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfig(Guid id)
        {
            var config = await _repo.GetByIdAsync(id);

            if (config == null)
                return NotFound();

            return Ok(_mapper.Map<ConfigResponseDto>(await _repo.DeleteAsync(config)));
        }

        private bool ConfigExists(Guid id)
        {
            // return (_repo.GetAsync().(e => e.Id == id)).GetValueOrDefault();
            return (_repo.GetByIdAsync(id)) == null ? true : false;
        }
    }
}
