using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Smartbell.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _repo;

        public AccountsController(IAccountRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public async Task<ActionResult<APIGenericResponse<ApplicationUser>>> PostAccount(CreateAccountDto createAccountDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _repo.CreateAsync(createAccountDto);
                    if (res.Success)
                        return Ok(res);
                }

                return BadRequest("Error creating account");
            }
            catch (Exception)
            {
                return new JsonResult("[PostAccount error!]") { StatusCode = 500 };
            }
        }



        // DELETE: api/Configs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(string id)
        {
            var res = await _repo.DeleteAsync(id);

            if (res == null)
                return NotFound();

            return Ok(res);
        }

        //private bool AccountExists(Guid id)
        //{
        //    // return (_repo.GetAsync().(e => e.Id == id)).GetValueOrDefault();
        //    return (_repo.GetByIdAsync(id)) == null ? true : false;
        //}
    }
}
