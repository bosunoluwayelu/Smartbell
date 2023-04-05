using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Smartbell.App.Controllers
{
    public class RingtonesController : Controller
    {
        private readonly IRingtoneService _ringtoneService;
        private readonly IMapper _mapper;
        public RingtonesController(IRingtoneService ringtoneService, IMapper mapper)
        {
            _ringtoneService = ringtoneService;
            _mapper = mapper;
        }

        // GET: RingtonesController
        public async Task<IActionResult> Index()
        {
            var ringtones = await _ringtoneService.GetAsync();

            return View(ringtones);
        }

        // GET: Ringtones/AddOrEdit(Insert)
        // GET: Ringtones/AddOrEdit/5(Update)
        public async Task<IActionResult> AddOrEdit(Guid id)
        {
            if (id == Guid.Empty)
                return View(new CreateRingtoneViewModel());
            else
            {
                var ringtoneModel = await _ringtoneService.GetByIdAsync(id);
                if (ringtoneModel == null)
                {
                    return NotFound();
                }
                //_mapper.Map<CreateRingtoneViewModel>(ringtoneModel)
                return View(ringtoneModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(Guid id, [Bind("Id,Description,RingtoneFilePath")] CreateRingtoneViewModel createRingtoneViewModel)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (id == Guid.Empty)
                {
                    //transactionModel.Date = DateTime.Now;
                    //_context.Add(transactionModel);
                    //await _context.SaveChangesAsync();

                }
                //Update
                else
                {
                    //try
                    //{
                    //    _context.Update(transactionModel);
                    //    await _context.SaveChangesAsync();
                    //}
                    //catch (DbUpdateConcurrencyException)
                    //{
                    //    if (!TransactionModelExists(transactionModel.TransactionId))
                    //    { return NotFound(); }
                    //    else
                    //    { throw; }
                    //}
                }
                // return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Transactions.ToList()) });
                return Json(new { });
            }
            // return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", transactionModel) });
            return Json(new { });
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
