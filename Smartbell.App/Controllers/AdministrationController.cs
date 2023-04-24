using Microsoft.AspNetCore.Mvc;

namespace Smartbell.App.Controllers
{
    [Authorize]
	public class AdministrationController : Controller
	{
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ILogger<AdministrationController> _logger;

		public AdministrationController(RoleManager<IdentityRole> roleManager,
										UserManager<ApplicationUser> userManager,
										ILogger<AdministrationController> logger)
		{
			_roleManager = roleManager;
			_userManager = userManager;
			_logger = logger;
		}

		[HttpGet]
		public IActionResult CreateRole()
		{
			return View();	
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
		{
			if (ModelState.IsValid)
			{
				var identityRole = new IdentityRole
				{
					Name = model.RoleName
				};

				var result = await _roleManager.CreateAsync(identityRole);

				if (result.Succeeded)
				{
					return RedirectToAction("listroles", "administration");
				}

				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
			}

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> EditUser(string id)
		{
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with id = {id} cannot be found";
                return View("NotFound");
            }

            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            var model = new EditUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Claims = userClaims.Select(c => c.Type + " : " + c.Value).ToList(),
                Roles = (List<string>)userRoles
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.PhoneNumber = model.PhoneNumber;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(model);
        }

        [HttpGet]
		public IActionResult ListUsers()
		{
			var users = _userManager.Users;
            return View(users);
        }

		[HttpGet]
		public IActionResult ListRoles() 
		{
			var roles = _roleManager.Roles;
			return View(roles);
		}

        [HttpGet]
        //[Authorize(Policy = "EditRolePolicy")]
        public async Task<IActionResult> ManageUserRoles(string userId)
        {
            ViewBag.UserId = userId;
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with id = {userId} cannot be found";
                return View("NotFound");
            }

            var model = new List<UserRolesViewModel>();

            foreach (var role in _roleManager.Roles)
            {
                var userRolesViewModel = new UserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                    userRolesViewModel.IsSelected = true;
                else
                    userRolesViewModel.IsSelected = false;

                model.Add(userRolesViewModel);
            }

            return View(model);
        }

        [HttpPost]
        //[Authorize(Policy = "EditRolePolicy")]
        public async Task<IActionResult> ManageUserRoles(
            List<UserRolesViewModel> model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with id = {userId} cannot be found";
                return View("NotFound");
            }

            // remove existing roles for this user
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                //foreach (var error in result.Errors)
                //{
                //    ModelState.AddModelError("", error.Description);
                //}
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }

            // add new selected roles
            result = await _userManager.AddToRolesAsync(user,
                model.Where(m => m.IsSelected).Select(r => r.RoleName));

            if (!result.Succeeded)
            {
                //foreach (var error in result.Errors)
                //{
                //    ModelState.AddModelError("", error.Description);
                //}
                ModelState.AddModelError("", "Cannot add selected roles");
                return View(model);
            }

            return RedirectToAction("EditUser", new { id = userId });
        }
    }
}
