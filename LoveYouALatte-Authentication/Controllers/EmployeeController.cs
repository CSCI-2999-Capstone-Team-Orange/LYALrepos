using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using LoveYouALatte.Data.Entities;
using MySql.Data.MySqlClient;
using System.Security.Claims;
using LoveYouALatte_Authentication.Models;

namespace LoveYouALatte_Authentication.Controllers
{
    public class EmployeeController : Controller
    {

        public IActionResult EmployeeView()
        {
            var UserID = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRole = this.User.FindFirstValue(ClaimTypes.Role);

            var currentUserInfo = new UserInfo();
            
            using (var dbContext = new loveyoualattedbContext())
            {
                var userDbInfo = dbContext.AspNetUsers.SingleOrDefault(s => s.Id == UserID);
                if (userDbInfo != null)
                {
                    currentUserInfo.firstName = userDbInfo.FirstName;
                    currentUserInfo.lastName = userDbInfo.LastName;
                }
            }
            return View(currentUserInfo);
        }

        [Authorize(Roles = "Employee")]
        public IActionResult ManageMenu()
        {
            return View();
        }
    }
}
