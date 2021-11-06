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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using LoveYouALatte_Authentication.Data;

namespace LoveYouALatte_Authentication.Controllers
{
    [Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public EmployeeController(RoleManager<IdentityRole> roleManager,
                                            UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }



        string connectionString = "server=authtest.cjiyeakoxxft.us-east-1.rds.amazonaws.com; port=3306; database=loveyoualattedb; uid=test; pwd=orange1234;";

        [HttpGet]
        [Authorize(Roles = "Employee")]
        public IActionResult ManageMenu()
        {
            MenuViewModel vm = new MenuViewModel();

            var productList = new List<Models.ProductKG>();
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
                        Models.ProductKG prod = new Models.ProductKG();

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


            List<DrinkModel> drinkList = new List<DrinkModel>();
            using (var dbContext = new loveyoualattedbContext())
            {
                var drinks = dbContext.Drinks.ToList();
                foreach (var drink in drinks)
                {
                    drinkList.Add(new DrinkModel
                    {
                        drinkId = drink.IdDrinks,
                        drinkName = drink.DrinkName,
                    });
                }
            }

            ViewBag.Drinks = new SelectList(drinkList, "drinkId", "drinkName");

            DrinkModel drinkIds = new DrinkModel();
            
            using (var dbContext = new loveyoualattedbContext())
            {
                var drinks = dbContext.Drinks.ToList();
                foreach (var drink in drinks) {
                    drinkIds.drinkIdList.Add(new DrinkModel
                    {
                        drinkId = drink.IdDrinks,
                        drinkName = drink.DrinkName,
                    }) ;
                }
            }

            vm.drinkDivID = drinkIds;

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


        [HttpPost]
        public IActionResult UpdateCardMenu(ManageMenuTableModel formInfo)
        {
            if (ModelState.IsValid)
            {
                using (var dbContext = new loveyoualattedbContext())
                {
                    var updateThisProduct = dbContext.Products.First(s => s.IdProduct == formInfo.updateProduct.ProductId);
                    var updateThisDrink = dbContext.Drinks.First(t => t.IdDrinks == formInfo.updateProduct.DrinkId);

                    updateThisProduct.IdProduct = formInfo.updateProduct.ProductId;
                    updateThisProduct.IdDrink = formInfo.updateProduct.DrinkId;
                    updateThisProduct.IdSize = formInfo.updateProduct.SizeId;
                    updateThisProduct.Price = formInfo.updateProduct.Price;
                    updateThisDrink.DrinkName = formInfo.updateProduct.DrinkName;
                    updateThisDrink.DrinkDescription = formInfo.updateProduct.DrinkDescription;
                    dbContext.SaveChanges();

                }
                return RedirectToAction("ManageMenu");

            }
            else
            {
                
                return RedirectToAction("ManageMenu");
            }
        }


        public IActionResult ManageMenuTable()
        {
            ManageMenuTableModel updateMenuItem = new ManageMenuTableModel();
            ProductModel productList = new ProductModel();
            
            
            updateMenuItem.viewProduct = productList;
            
           

            using (var dbContext = new loveyoualattedbContext())
            {
                var products = dbContext.Products.ToList();
                var sizes = dbContext.Sizes.ToList();
                var drinks = dbContext.Drinks.ToList();

                foreach (var product in products)
                {

                    productList.productList.Add(new ProductModel
                    {
                        ProductId = product.IdProduct,
                        DrinkId = product.IdDrink,
                        ProductSku = product.ProductSku,
                        DrinkName = drinks.Single(d => d.IdDrinks == product.IdDrink).DrinkName,
                        DrinkDescription = drinks.Single(d => d.IdDrinks == product.IdDrink).DrinkDescription,
                        SizeId = product.IdSize,
                        SizeName = sizes.Single(s => s.IdSize == product.IdSize).Size1,
                        Price = product.Price
                    }) ;

                    


                }

            }


            return View(updateMenuItem);
        }

        [HttpPost]
        public IActionResult ManageMenuTable(ManageMenuTableModel formInfo)
        {
            
            if (ModelState.IsValid) {
                using (var dbContext = new loveyoualattedbContext())
                {
                    var updateThisProduct = dbContext.Products.First(s => s.IdProduct == formInfo.updateProduct.ProductId);
                    var updateThisDrink = dbContext.Drinks.First(t => t.IdDrinks == formInfo.updateProduct.DrinkId);
                    

                    updateThisProduct.IdProduct = formInfo.updateProduct.ProductId;
                    updateThisProduct.ProductSku = formInfo.updateProduct.ProductSku;
                    updateThisProduct.IdDrink = formInfo.updateProduct.DrinkId;
                    updateThisProduct.IdSize = formInfo.updateProduct.SizeId;
                    updateThisProduct.Price = formInfo.updateProduct.Price;
                    updateThisDrink.DrinkName = formInfo.updateProduct.DrinkName;
                    updateThisDrink.DrinkDescription = formInfo.updateProduct.DrinkDescription;
                    dbContext.SaveChanges();
                    
                }
                return RedirectToAction("ManageMenuTable", "Employee", formInfo);

            }
            else
            {
                ProductModel productList = new ProductModel();

                formInfo.viewProduct = productList;

                using (var dbContext = new loveyoualattedbContext())
                {
                    var products = dbContext.Products.ToList();
                    var sizes = dbContext.Sizes.ToList();
                    var drinks = dbContext.Drinks.ToList();

                    foreach (var product in products)
                    {

                        productList.productList.Add(new ProductModel
                        {
                            ProductId = product.IdProduct,
                            DrinkId = product.IdDrink,
                            ProductSku = product.ProductSku,
                            DrinkName = drinks.Single(d => d.IdDrinks == product.IdDrink).DrinkName,
                            DrinkDescription = drinks.Single(d => d.IdDrinks == product.IdDrink).DrinkDescription,
                            SizeId = product.IdSize,
                            SizeName = sizes.Single(s => s.IdSize == product.IdSize).Size1,
                            Price = product.Price
                        });




                    }

                }

                

                return View(formInfo);
            }
        }
        
        public ActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(AddProduct vm)
        {
            if (ModelState.IsValid)
            {
                MySqlDatabase db = new MySqlDatabase(connectionString);
                using (MySqlConnection conn = db.Connection)
                {
                    var val = conn.CreateCommand() as MySqlCommand;
                    val.CommandText = @"SELECT drink_name FROM loveyoualattedb.drinks WHERE drink_name = '" + vm.DrinkName + "';";
                    var validDrink = val.ExecuteScalar();
                    if (validDrink != null)
                    {
                        if (vm.DrinkName.ToLower() == validDrink.ToString().ToLower())
                        {
                            ModelState.AddModelError("AddProductError", "Duplicate drink.");
                            return View(vm);
                        }
                    }
                    else
                    {
                        var cmd1 = conn.CreateCommand() as MySqlCommand;
                        cmd1.CommandText = @"INSERT INTO loveyoualattedb.drinks (drink_name, drink_description) VALUES ('" + vm.DrinkName + "', '" + vm.DrinkDescription + "');" +
                            "SELECT LAST_INSERT_ID();";
                        var drinkID = cmd1.ExecuteScalar();

                        var cmd2 = conn.CreateCommand() as MySqlCommand;
                        cmd2.CommandText = @"INSERT INTO loveyoualattedb.product (idDrink, idSize, productSKU, price) VALUES ('" + drinkID + "', '1', '" + vm.SmallSKU + "','" + vm.SmallPrice + "');" +
                            "INSERT INTO loveyoualattedb.product (idDrink, idSize, productSKU, price) VALUES ('" + drinkID + "', '2', '" + vm.MediumSKU + "','" + vm.MediumPrice + "');" +
                            "INSERT INTO loveyoualattedb.product (idDrink, idSize, productSKU, price) VALUES ('" + drinkID + "', '3', '" + vm.LargeSKU + "','" + vm.LargePrice + "');";
                        cmd2.ExecuteNonQuery();

                        vm.AddProductSuccess = "Succesfully added product!";

                        return View(vm);
                    }
                }
            }
            else
            {
                return View(vm);
            }
            return View(vm);

        }

        public IActionResult AddEmployee()
        {
           
            return View();
        }
















    }
}
