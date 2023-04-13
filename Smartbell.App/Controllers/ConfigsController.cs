using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace Smartbell.App.Controllers
{
    public class ConfigsController : Controller
    {
        private readonly IConfigService _configService;
        private readonly IMapper _mapper;
        public ConfigsController(IConfigService configService, IMapper mapper)
        {
            _configService = configService;
            _mapper = mapper;
        }

        // GET: ConfigController
        public async Task<IActionResult> Index()
        {
            try
            {
                var config = await _configService.GetAsync();
                return View(config);
            }
            catch (Exception)
            {
                return NotFound();
            }            
        }

        public async Task<IActionResult> AddOrEdit(Guid Id)
        {
            try
            {
                if (Id == Guid.Empty) { return View(new ConfigResponseDto()); }

                var config = await _configService.GetByIdAsync(Id);

				return config == null ? NotFound() : View(config);

			}
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> AddOrEdit(Guid Id, [Bind("Id,Description,Value")] ConfigResponseDto model)
		{
			try
			{
                if (ModelState.IsValid)
                {
                    if (Id == Guid.Empty)
                    {
						// ConfigResponseDto -> CreateConfigDto
						var config = _mapper.Map<CreateConfigDto>(model);
                        var res = _configService.CreateAsync(config);
                    }
                    else
                    {
                        var res = _configService.UpdateAsync(model);
                    }

                    return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", await _configService.GetAsync()) });
                }

				return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", model) });

			}
			catch (Exception)
			{
				return NotFound();
			}
		}

		public async Task<IActionResult> Delete(Guid id)
		{
            return View();
		}


		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var config = await _configService.GetByIdAsync(id);

            if (config != null)
                await _configService.DeleteAsync(config);

			return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", await _configService.GetAsync()) });
		}
	}
}
