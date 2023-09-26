using Book.Store.Web.Areas.User.Models;
using Book.Store.Web.Filters;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Book.Store.Web.Utilities;
using User.Manager;
using Services.Common;
using XAct;
using ResultStatus = Services.Common.ResultStatus;
using User.Entities;

namespace Book.Store.Web.Areas.User.Controllers
{
    [Area("User")]
    [Route("User/User")]
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly BookStoreSession _session;

        public UserController(IConfiguration configuration, BookStoreSession bookStoreSession)
        {
            _configuration = configuration;
            _session = bookStoreSession;
        }
        [Route("Index")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, Route("Index")]
        public IActionResult Index(LoginVm user)
        {
            using (UserManager manager = new UserManager())
            {
                if (ModelState.IsValid)
                {
                    var result = manager.Login(user.UserName, user.Password);
                    if (result.Status == ResultStatus.Ok)
                    {
                        _session.UserId = result.Data.Id;
                        _session.UserType = (int)result.Data.UserType;
                        _session.UserName = result.Data.UserName;
                        ClaimsIdentity identity = null;
                        bool isAuthenticate = false;
                        string redirectUrl = "";
                        if (_session.UserType == 1)
                        {
                            identity = new ClaimsIdentity(new[]
                            {
                                new Claim(ClaimTypes.Name,user.UserName),
                                new Claim(ClaimTypes.Role,"Admin")
                            }, CookieAuthenticationDefaults.AuthenticationScheme);
                            isAuthenticate = true;
                            redirectUrl = "/StoreManagement/AdminDashboard/Index";
                        }
                        if (_session.UserType == 2)
                        {
                            identity = new ClaimsIdentity(new[]
                            {
                                new Claim(ClaimTypes.Name,user.UserName),
                                new Claim(ClaimTypes.Role,"Store")
                            }, CookieAuthenticationDefaults.AuthenticationScheme);
                            isAuthenticate = true;
                            redirectUrl = "/StoreManagement/Book/Index";
                        }
                        if (isAuthenticate == true)
                        {
                            var principal = new ClaimsPrincipal(identity);
                            var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                            return Redirect(redirectUrl);
                        }
                    }
                }

            }
            ModelState.AddModelError("InvalidLogin", "Invalid username or password entered");
            return View();
        }

        [HttpGet, Route("SignUp")]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost, Route("SignUp")]
        public IActionResult Signup(LoginVm signup)
        {
            using (var manager = new UserManager())
            {
                var result = manager.SignUp(signup.ConvertObjectTo<EUser>());
                if (result.Status == ResultStatus.Ok)
                {
                    return Redirect("Index");
                }
            }
            return View();
        }

        private LoginVm ParseIntoVm(EUser user)
        {
            throw new NotImplementedException();
        }

        [HttpGet, Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "User");
        }
    }
}
