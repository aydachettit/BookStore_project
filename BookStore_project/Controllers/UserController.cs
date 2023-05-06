using BookStore_project.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Service;
using Service.implementation;
using System.Net.WebSockets;

namespace BookStore_project.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IPasswordHasher<IdentityUser> _PasswordHaser;
        private IBillService _BillService;
        private IStatusService _StatusService;
        public UserController(IPasswordHasher<IdentityUser> PasswordHaser, IStatusService StatusService, IBillService BillService, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _PasswordHaser = PasswordHaser;
            _StatusService = StatusService;
            _BillService = BillService;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> UserDetailAsync(string name)
        {
            var user = await _userManager.FindByNameAsync(name) as IdentityUser;
            var list = _BillService.FindBillByUser(user.Id);
            var model = new UserDetailViewModel();
            model.Id = user.Id;
            model.Name = user.UserName;
            model.Phone = user.PhoneNumber;
            model.Email = user.Email;
            model.ListOfBill = list;
            var statusName = _StatusService.GetAll();
            ViewBag.statusbag = statusName;
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var model = new UserEditViewModel();
            model.Id = user.Id;
            model.UserName = user.UserName;
            model.email = user.Email;
            model.PhoneNumber = user.PhoneNumber;
            return View(model);
        }
        //[Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser usr = await _userManager.FindByIdAsync(model.Id) as IdentityUser;
                usr.Email = model.email;
                usr.UserName = model.UserName;
                usr.PhoneNumber = model.PhoneNumber;
                await _userManager.UpdateAsync(usr);
                return RedirectToAction("UserDetail", new { name = usr.UserName });
            }
            return View();

        }
        [HttpGet]
        public async Task<IActionResult> ChangePassword(string id)
        {
            var model = new UserChangePasswordViewModel();
            model.id = id;
            return View(model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(UserChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.NewPassword != model.ConfirmNewPassword)
                {
                    return View();
                }
                var usr = await _userManager.FindByIdAsync(model.id) as IdentityUser;

                if (usr == null)
                {
                    return View();
                }
                var PasswordIsvalid = await _userManager.CheckPasswordAsync(usr, model.OldPassword);
                if (!PasswordIsvalid)
                {
                    return View();
                }

                var HashNewpassword = _PasswordHaser.HashPassword(usr, model.NewPassword);
                usr.PasswordHash = HashNewpassword;
                var result = await _userManager.UpdateAsync(usr);
                if (result.Succeeded)
                {
                    return RedirectToAction("UserDetail", new { name = usr.UserName });
                }

            }
            return View();

        }
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var model = new UserRegisterViewModel();
            return View(model);

        }
        [HttpPost]
        
        public async Task<IActionResult> Register(UserRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Username.ToUpper(),
                    Email = model.Email.ToUpper(),
                    PasswordHash = model.Password
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if(result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Customer");
                    await _signInManager.SignInAsync(user, isPersistent: false, authenticationMethod: null);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.ToString());
                    }
                }
            }
            return View(model);


        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            var model = new UserLoginViewModel();
            return View(model);

        }
        [HttpPost]

        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser userlogin =await _userManager.FindByEmailAsync(model.Email);
                bool checkPass = await _userManager.CheckPasswordAsync(userlogin, model.Password);
                if (userlogin != null && checkPass)
                {
                    var result = await _signInManager.PasswordSignInAsync(userlogin.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {

                        return RedirectToAction("Index", "Home");

                    }
                    if (result.RequiresTwoFactor)
                    {
                        return RedirectToPage("./LoginWith2fa", new { ReturnUrl = "", RememberMe = model.RememberMe });
                    }
                    if (result.IsLockedOut)
                    {
                        return RedirectToPage("./Lockout");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    }
                }
            }
            ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors);
            return View(model);
        }

    }
}
