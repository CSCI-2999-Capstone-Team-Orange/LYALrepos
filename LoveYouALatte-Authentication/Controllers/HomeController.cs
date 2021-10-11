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

namespace LoveYouALatte_Authentication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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

            return View();
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

        //string connectionString = "server=aa124gktif3j980.cjiyeakoxxft.us-east-1.rds.amazonaws.com; port=3306; database=loveyoualattedb; uid=test; pwd=orange1234;";

        //[HttpPost]
        //public ActionResult AddTime()
        //{
        //    DateTime utcTime = DateTime.UtcNow;
        //    var easternTime = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
        //    DateTime currentTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, easternTime);

        //    MySqlDatabase db = new MySqlDatabase(connectionString);
        //    using (MySqlConnection conn = db.Connection)
        //    {
        //        var cmd = conn.CreateCommand() as MySqlCommand;
        //        cmd.CommandText = @"INSERT INTO log_time(log_time) VALUES ('" + currentTime.ToString("yyyy/MM/dd HH:mm:ss") + "')";

        //        cmd.ExecuteNonQuery();
        //    }



        //    return RedirectToAction("Index", "Home", new { currentTime = currentTime });
        //}
    }
}
