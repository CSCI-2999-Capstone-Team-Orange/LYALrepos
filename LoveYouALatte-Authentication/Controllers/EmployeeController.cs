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
        string connectionString = "server=authtest.cjiyeakoxxft.us-east-1.rds.amazonaws.com; port=3306; database=loveyoualattedb; uid=test; pwd=orange1234;";

        [HttpGet]
        [Authorize(Roles = "Employee")]
        public IActionResult ManageMenu()
        {
            MenuViewModel vm = new MenuViewModel();

            var productList = new List<Models.Product>();
            //cart info passed to list

            MySqlDatabase db = new MySqlDatabase(connectionString);
            using (MySqlConnection conn = db.Connection)
            {
                var cmd = conn.CreateCommand() as MySqlCommand;
                cmd.CommandText = @"
                    SELECT idProduct, idDrink, size.idSize, price, drink_name, drink_description, size.size FROM loveyoualattedb.product prod
                    INNER JOIN loveyoualattedb.drinks drink ON prod.idDrink = drink.idDrinks
                    INNER JOIN loveyoualattedb.size size ON prod.idSize = size.idSize";

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Models.Product prod = new Models.Product();

                        prod.ProductId = dr["idProduct"] as int? ?? default(int);
                        prod.DrinkId = dr["idDrink"] as int? ?? default(int);
                        prod.SizeId = dr["idSize"] as int? ?? default(int);
                        prod.Price = dr["price"] as decimal? ?? default(decimal);
                        prod.DrinkName = dr["drink_name"] as String ?? string.Empty;
                        prod.DrinkDescription = dr["drink_description"] as String ?? string.Empty;
                        prod.SizeName = dr["size"] as String ?? string.Empty;

                        productList.Add(prod);
                    }
                }
            }
            vm.Products = productList;
            return View(vm);
        }
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
        public IActionResult AddMenu()
        {
            MenuViewModel vm = new MenuViewModel();

            var productList = new List<Models.Product>();
            //cart info passed to list

            MySqlDatabase db = new MySqlDatabase(connectionString);
            using (MySqlConnection conn = db.Connection)
            {
                var cmd = conn.CreateCommand() as MySqlCommand;
                cmd.CommandText = @"
                    SELECT idProduct, idDrink, size.idSize, price, drink_name, drink_description, size.size FROM loveyoualattedb.product prod
                    INNER JOIN loveyoualattedb.drinks drink ON prod.idDrink = drink.idDrinks
                    INNER JOIN loveyoualattedb.size size ON prod.idSize = size.idSize";

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Models.Product prod = new Models.Product();

                        prod.ProductId = dr["idProduct"] as int? ?? default(int);
                        prod.DrinkId = dr["idDrink"] as int? ?? default(int);
                        prod.SizeId = dr["idSize"] as int? ?? default(int);
                        prod.Price = dr["price"] as decimal? ?? default(decimal);
                        prod.DrinkName = dr["drink_name"] as String ?? string.Empty;
                        prod.DrinkDescription = dr["drink_description"] as String ?? string.Empty;
                        prod.SizeName = dr["size"] as String ?? string.Empty;

                        productList.Add(prod);
                    }
                }
            }
            vm.Products = productList;
            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddMenuItem(int idProduct, decimal price, string drink_name, string drink_description)
        {
            MenuViewModel vm = new MenuViewModel();
            MySqlDatabase db = new MySqlDatabase(connectionString);
            using (MySqlConnection conn = db.Connection)
            {
                var cmd = conn.CreateCommand() as MySqlCommand;
                cmd.CommandText = @"
                                INSERT INTO idProduct, idDrink, size.idSize, price, drink_name, drink_description, size.size FROM loveyoualattedb.product prod
                    FROM loveyoualattedb.drinks drink ON prod.idDrink = drink.idDrinks
                    FROM loveyoualattedb.size size ON prod.idSize = size.idSize";
            }
            return RedirectToAction("AddMenu");
        }
    }
}
