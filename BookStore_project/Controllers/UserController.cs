using BookStore_project.Models.Author;
using BookStore_project.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using Service.implementation;
using Service.Implementation;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;

namespace BookStore_project.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IPasswordHasher<IdentityUser> _PasswordHaser;
        private IBillService _BillService;
        private IStatusService _StatusService;
        private IUserService _userService;
        public UserController(IUserService userService,IPasswordHasher<IdentityUser> PasswordHaser, IStatusService StatusService, IBillService BillService, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userService = userService;
            _PasswordHaser = PasswordHaser;
            _StatusService = StatusService;
            _BillService = BillService;
            _userManager = userManager;
            _signInManager = signInManager;
        }
       
        public async Task<IActionResult> Lock(string id)
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            var user = await _userManager.FindByIdAsync(id) as IdentityUser;
            await _userManager.SetLockoutEnabledAsync(user, true);
            await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow.AddMinutes(30));
            return RedirectToAction("Index");
        }
       
        public async Task<IActionResult> Unlock(string id)
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            var user = await _userManager.FindByIdAsync(id) as IdentityUser;
            await _userManager.SetLockoutEnabledAsync(user, false);
            await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow);
            return RedirectToAction("Index");
        }
        
        public IActionResult Index()
        { if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            
            var model = _userService.getALL().Select(user => new UserIndexViewModel
            {
                id = user.Id,
                UserName = user.UserName,
                Phone = user.PhoneNumber,
                Gmail = user.Email,
                Lock=user.LockoutEnabled
            }).ToList();

            return View(model);
        }
        
        public async Task<IActionResult> UserDetailAsync(string name)
        {
            var user = await _userManager.FindByNameAsync(name) as IdentityUser;
            var list = _BillService.FindBillByUser(user.UserName);
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
                    ModelState.AddModelError("", "Mật khẩu nhập lại không trùng khớp!");
                }
                var usr = await _userManager.FindByIdAsync(model.id) as IdentityUser;

                if (usr == null)
                {
                    return View();
                }
                var PasswordIsvalid = await _userManager.CheckPasswordAsync(usr, model.OldPassword);
                if (!PasswordIsvalid)
                {
                    ModelState.AddModelError("", "Old password is not correct!");
                }
                var res = await _userManager.ChangePasswordAsync(usr, model.OldPassword, model.NewPassword);
                
                if (res.Succeeded)
                {
                   
                    return RedirectToAction("UserDetail", new { name = usr.UserName });
                }
                else
                {
                    foreach (var error in res.Errors)
                    {
                        ModelState.AddModelError("","Mật khẩu phải lớn hơn 6 kí tự,có ít nhất 1 kí tự đặc biệt và chữ in hoa  ");
                    }
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
                        ModelState.AddModelError("", error.Description.ToString());
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
                        ModelState.AddModelError(string.Empty, "User Locked Please contact Admin.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    }
                }
                 else
                {
                    ModelState.AddModelError(string.Empty, "Username or password might not be correct");
                }
            }
            
            return View(model);
        }

    }
}
