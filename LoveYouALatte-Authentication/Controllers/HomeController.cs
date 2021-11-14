using LoveYouALatte_Authentication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using LoveYouALatte.Data.Entities;
using MySql.Data.MySqlClient;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using LoveYouALatte_Authentication.Data;

namespace LoveYouALatte_Authentication.Controllers
{
    public class HomeController : Controller
    {

        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly ILogger<HomeController> _logger;

        public HomeController(SignInManager<ApplicationUser> signInManager, ILogger<HomeController> logger)
        {
            _logger = logger;
            _signInManager = signInManager;;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult HomePage()
        {
            var userRole = this.User.FindFirstValue(ClaimTypes.Role);
            var UserID = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (userRole == "Employee")
            {
                return RedirectToAction("employeeView", "employee");
            }
            else if (userRole == "admin")
            {
                return RedirectToAction("adminHome", "administration");
            }
            else
            {
                UserInfo authenticatedUser = new UserInfo();
                using (var dbContext = new loveyoualattedbContext())
                {
                    var userDbInfo = dbContext.AspNetUsers.SingleOrDefault(s => s.Id == UserID);
                    if (userDbInfo != null)
                    {
                        authenticatedUser.firstName = userDbInfo.FirstName;
                        authenticatedUser.lastName = userDbInfo.LastName;
                    }
                }
                return View(authenticatedUser);
            }
        }
        public IActionResult FAQ()
        {
            return View();
        }
        public IActionResult ContactUs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            
            return RedirectToAction("HomePage");
            
        }
    }
}
