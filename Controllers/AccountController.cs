using ABCMoneyTransfer.Models;
using ABCMoneyTransfer.Persistence.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ABCMoneyTransfer.Controllers
{
    [AllowAnonymous]
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel, string? returnUrl = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(loginModel.Username, loginModel.Password, loginModel.RememberMe, true);
                    if (result.Succeeded)
                    {
                        if (!String.IsNullOrEmpty(returnUrl))
                        {
                            return LocalRedirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("index", "exchangerates");
                        }
                    }
                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError("", "User account locked out.");
                    }
                    else if (result.IsNotAllowed)
                    {
                        ModelState.AddModelError("", "User is not allowed.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid Username & Password.");
                    }
                    ViewData["ReturnUrl"] = returnUrl;
                    return View(loginModel);
                }
                else
                {
                    ViewData["ReturnUrl"] = returnUrl;
                    return View(loginModel);
                }
            }
            catch (Exception ex)
            {
                ViewData["ReturnUrl"] = returnUrl;
                ModelState.AddModelError("", "Error Occured : " + ex.Message);
                return View(loginModel);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser
                    {
                        Email = registerModel.Email,
                        UserName = registerModel.Username
                    };
                    var result = await _userManager.CreateAsync(user, registerModel.Password);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: true);
                        return RedirectToAction("index", "exchangerates");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        return View(registerModel);
                    }
                }
                else
                {
                    return View(registerModel);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error Occured : " + ex.Message);
                return View(registerModel);
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout(string? returnUrl = null)
        {
            try
            {
                await _signInManager.SignOutAsync();
                if (!String.IsNullOrEmpty(returnUrl))
                {
                    return LocalRedirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("login", "account");
                }
            }
            catch (Exception ex)
            {
                return Content("Error Occured : " + ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
