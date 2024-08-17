namespace EventManagement.Areas.Services.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Services")]
    public class RoleController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(
                                UserManager<ApplicationUser> userManager,
                                RoleManager<IdentityRole> roleManager
                             )
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost()]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleVm roleVm)
        {
            if (ModelState.IsValid)
            {
                IdentityRole checkrole = await _roleManager.FindByNameAsync(roleVm.Name);
                if (checkrole is null)
                {

                    IdentityRole MappingRole = new IdentityRole();
                    MappingRole.Name = roleVm.Name;

                    await _roleManager.CreateAsync(MappingRole);
                    return RedirectToAction("Index", "Home");
                }
                if (checkrole.Name == "Admin")
                {
                    await _roleManager.AddClaimAsync(checkrole, new Claim("NumberOfAdmins", "1"));
                }
                ModelState.AddModelError(string.Empty, "The Role Already Exist");
            }
            return View(roleVm);
        }

        [HttpGet]
        public IActionResult AssignRole()
        {
            var users = _userManager.Users.ToList();
            var roles = _roleManager.Roles.ToList();

            AssignRoleVm assignRoleVm = new AssignRoleVm();

            foreach (var user in users)
            {
                assignRoleVm.Users.Add(new SelectListItem
                {
                    Value = user.Id,
                    Text = user.UserName
                });
            }
            foreach (var role in roles)
            {
                assignRoleVm.Roles.Add(new SelectListItem
                {
                    Value = role.Id,
                    Text = role.Name
                });
            }

            return View(assignRoleVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRole(AssignRoleVm assignRole)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByIdAsync(assignRole.UserId);
                IdentityRole role = await _roleManager.FindByIdAsync(assignRole.RoleId);
                if (user is not null && role is not null)
                {
                    await _userManager.AddToRoleAsync(user, role.Name);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid Assign");
            }
            return View(assignRole);
        }
    }
}
