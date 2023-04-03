using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Smartbell.App.Controllers
{
    public class RingtonesController : Controller
    {
        private readonly IRingtoneService _ringtoneService;
        public RingtonesController(IRingtoneService ringtoneService)
        {
            _ringtoneService = ringtoneService;
        }

        // GET: RingtoneController
        public async Task<IActionResult> Index()
        {
            var ringtones = await _ringtoneService.GetAsync();

            return View(ringtones);
        }

        // GET: RingtoneController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RingtoneController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RingtoneController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RingtoneController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RingtoneController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RingtoneController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RingtoneController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
