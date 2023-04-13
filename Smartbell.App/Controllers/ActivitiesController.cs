using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Smartbell.App.Controllers
{
    public class ActivitiesController : Controller
    {
        private readonly IActivityService _activityService;
        private readonly IMapper _mapper;
        public ActivitiesController(IActivityService activityService, IMapper mapper)
        {
            _activityService = activityService;
            _mapper = mapper;
        }

        // GET: ConfigController
        public async Task<IActionResult> Index()
        {
            try
            {
                var model = await _activityService.GetAsync();
                return View(model);
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
                if (Id == Guid.Empty) { return View(new CreateActivityDto()); }

                var model = await _activityService.GetByIdAsync(Id);

                return model == null ? NotFound() : View(model);

            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(Guid Id, [Bind("Id,Description,ImageFilePath,VideoFilePath")] CreateActivityDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Id == Guid.Empty)
                    {
                        var res = _activityService.CreateAsync(model);
                    }
                    else
                    {
                        //var res = _configService.UpdateAsync(model);
                    }

                    var response = await _activityService.GetAsync();

                    return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", response) });
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
            var config = await _activityService.GetByIdAsync(id);

            //if (config != null)
                //await _activityService.DeleteAsync(config);

            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", await _activityService.GetAsync()) });
        }
    }
}
