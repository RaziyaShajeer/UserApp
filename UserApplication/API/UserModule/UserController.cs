using Domain.Models;
using Domain.Services.UserModule.Interface;
using Microsoft.AspNetCore.Mvc;

namespace UserApplication.API.UserModule
{
    public class userController : Controller
    {
        private readonly IUserService _userinterface;
        IHttpContextAccessor _httpContextAccessor;
        public userController(IUserService userinterface, IHttpContextAccessor httpContextAccessor)
        {
            _userinterface = userinterface;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task< IActionResult> ViewAllUsers()
        {
            
            
                try
                {
                if (_httpContextAccessor.HttpContext.Session.GetString("UserId") == null)
                {
                    return RedirectToAction("Login", "Authentication");

                }
                var usersList = await _userinterface.GetUsersAsync();

                    
                    if (usersList == null || !usersList.Any())
                    {
                       
                        ViewBag.Message = "No users found.";
                        return View(new List<Users>()); 
                    }

                    return View(usersList);
                }
                catch (Exception ex)
                {
                  
                    ViewBag.ErrorMessage = "An error occurred while retrieving users. Please try again later.";
                    return View(new List<Users>());
                }
         }

        
    
        [HttpGet]
        public async Task<IActionResult> ViewDashboard()
        {
            return View();
        }

    }
}
