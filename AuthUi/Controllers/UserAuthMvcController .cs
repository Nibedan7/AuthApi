using AuthApi.Models;
using AuthUi.ApiHelper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuthApi.Controllers
{
    
    public class UserAuthMvcController : Controller
    {
        private readonly UserAuthApiHelper _apiHelper;

        public UserAuthMvcController(UserAuthApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        // Register Page
        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View();
        }

        // Register Handler (POST)
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                bool isRegistered = await _apiHelper.RegisterAsync(registerModel.Name, registerModel.Email, registerModel.Password);
                if (isRegistered)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", "Registration failed. Please try again.");
                }
            }
            return View(registerModel);
        }

        // Login Page
        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View();
        }

        // Login Handler (POST)
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var token = await _apiHelper.LoginAsync(loginModel.Email, loginModel.Password);
                if (!string.IsNullOrEmpty(token))
                {
                    HttpContext.Session.SetString("AuthToken", token);
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }
            return View();
        }

        // Dashboard or Home Page after login
        [HttpGet("Dashboard")]
        public IActionResult Dashboard()
        {
            var token = HttpContext.Session.GetString("AuthToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login");
            } 

            return View();
        }   

        // Logout handler
        public async Task<IActionResult> Logout()
        {
            // Call the LogoutAsync method from the API helper
            var result = await _apiHelper.LogoutAsync();

            if (result)
            {
                HttpContext.Session.Remove("AuthToken");
                return RedirectToAction("Login");
            }
            else
            {
                return BadRequest("Failed to log out.");
            }
        }

    }
}
