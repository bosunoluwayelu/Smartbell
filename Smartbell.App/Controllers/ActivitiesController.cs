using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Smartbell.App.Controllers
{
    public class ActivitiesController : Controller
    {
        // GET: ActivitiesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ActivitiesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ActivitiesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ActivitiesController/Create
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

        // GET: ActivitiesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ActivitiesController/Edit/5
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

        // GET: ActivitiesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ActivitiesController/Delete/5
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
