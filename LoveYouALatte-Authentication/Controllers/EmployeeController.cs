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
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace LoveYouALatte_Authentication.Controllers
{
    [Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IWebHostEnvironment hostingEnvironment;
        public EmployeeController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender, IWebHostEnvironment environment)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.signInManager = signInManager;
            _emailSender = emailSender;
            hostingEnvironment = environment;
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
                    SELECT idProduct, idDrinkFood, size.idSize, price, drink_name, drink_description, size.size FROM loveyoualattedb.product prod
                    INNER JOIN loveyoualattedb.drinkFood drink ON prod.idDrinkFood = drink.idDrinkFood
                    INNER JOIN loveyoualattedb.size size ON prod.idSize = size.idSize";

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Models.ProductKG prod = new Models.ProductKG();

                        prod.ProductId = dr["idProduct"] as int? ?? default(int);
                        prod.DrinkId = dr["idDrinkFood"] as int? ?? default(int);
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
                var drinks = dbContext.DrinkFoods.ToList();
                foreach (var drink in drinks)
                {
                    drinkList.Add(new DrinkModel
                    {
                        drinkId = drink.IdDrinkFood,
                        drinkName = drink.DrinkName,
                    });
                }
            }

            ViewBag.Drinks = new SelectList(drinkList, "drinkId", "drinkName");

            DrinkModel drinkIds = new DrinkModel();
            
            using (var dbContext = new loveyoualattedbContext())
            {
                var drinks = dbContext.DrinkFoods.ToList();
                foreach (var drink in drinks) {
                    drinkIds.drinkIdList.Add(new DrinkModel
                    {
                        drinkId = drink.IdDrinkFood,
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
                    var updateThisDrink = dbContext.DrinkFoods.First(t => t.IdDrinkFood == formInfo.updateProduct.DrinkId);

                    updateThisProduct.IdProduct = formInfo.updateProduct.ProductId;
                    updateThisProduct.IdDrinkFood = formInfo.updateProduct.DrinkId;
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

            
            List<SelectListItem> categoriesList = new List<SelectListItem>();


            using (var dbContext = new loveyoualattedbContext())
            {
                var categories = dbContext.Categories.ToList();
                foreach (var category in categories)
                {
                    categoriesList.Add( new SelectListItem ()
                    {
                        Value = $"{category.IdCategory}",
                        Text = category.CategoryName

                    });
                    
                }

            }

            updateMenuItem.DrinkCategories = categoriesList;

            

            using (var dbContext = new loveyoualattedbContext())
            {
                var products = dbContext.Products.ToList();
                var sizes = dbContext.Sizes.ToList();
                var drinks = dbContext.DrinkFoods.ToList();
                var categories = dbContext.Categories.ToList();
                foreach (var product in products)
                {

                    productList.productList.Add(new ProductModel
                    {
                        ProductId = product.IdProduct,
                        DrinkId = product.IdDrinkFood,
                        ProductSku = product.ProductSku,
                        category = (int)drinks.Single(a => a.IdDrinkFood == product.IdDrinkFood).IdCategory,
                        CategoryName = categories.Single(n => n.IdCategory == drinks.Single(a => a.IdDrinkFood == product.IdDrinkFood).IdCategory).CategoryName,
                        DrinkName = drinks.Single(d => d.IdDrinkFood == product.IdDrinkFood).DrinkName,
                        DrinkDescription = drinks.Single(d => d.IdDrinkFood == product.IdDrinkFood).DrinkDescription,
                        SizeId = product.IdSize,
                        SizeName = sizes.SingleOrDefault(s => s.IdSize == product.IdSize)?.Size1 ?? "n/a",
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
                    var updateThisDrink = dbContext.DrinkFoods.First(t => t.IdDrinkFood == formInfo.updateProduct.DrinkId);
                    

                    updateThisProduct.IdProduct = formInfo.updateProduct.ProductId;
                    updateThisProduct.ProductSku = formInfo.updateProduct.ProductSku;
                    updateThisProduct.IdDrinkFood = formInfo.updateProduct.DrinkId;
                    updateThisProduct.IdSize = formInfo.updateProduct.SizeId;
                    updateThisProduct.Price = formInfo.updateProduct.Price;
                    updateThisDrink.DrinkName = formInfo.updateProduct.DrinkName;
                    updateThisDrink.IdCategory = formInfo.updateProduct.category;
                    updateThisDrink.DrinkDescription = formInfo.updateProduct.DrinkDescription;
                    dbContext.SaveChanges();
                    
                }
                return RedirectToAction("ManageMenuTable", "Employee");

            }
            else
            {

                ProductModel productList = new ProductModel();

                formInfo.viewProduct = productList;


                List<SelectListItem> categoriesList = new List<SelectListItem>();


                using (var dbContext = new loveyoualattedbContext())
                {
                    var categories = dbContext.Categories.ToList();
                    foreach (var category in categories)
                    {
                        categoriesList.Add(new SelectListItem()
                        {
                            Value = $"{category.IdCategory}",
                            Text = category.CategoryName

                        });

                    }

                }

                formInfo.DrinkCategories = categoriesList;

                using (var dbContext = new loveyoualattedbContext())
            {
                var products = dbContext.Products.ToList();
                var sizes = dbContext.Sizes.ToList();
                var drinks = dbContext.DrinkFoods.ToList();
                var categories = dbContext.Categories.ToList();
                foreach (var product in products)
                {

                    productList.productList.Add(new ProductModel
                    {
                        ProductId = product.IdProduct,
                        DrinkId = product.IdDrinkFood,
                        ProductSku = product.ProductSku,
                        category = (int)drinks.Single(a => a.IdDrinkFood == product.IdDrinkFood).IdCategory,
                        CategoryName = categories.Single(n => n.IdCategory == drinks.Single(a => a.IdDrinkFood == product.IdDrinkFood).IdCategory).CategoryName,
                        DrinkName = drinks.Single(d => d.IdDrinkFood == product.IdDrinkFood).DrinkName,
                        DrinkDescription = drinks.Single(d => d.IdDrinkFood == product.IdDrinkFood).DrinkDescription,
                        SizeId = product?.IdSize ?? null,
                        SizeName = sizes.SingleOrDefault(s => s.IdSize == product.IdSize)?.Size1 ?? "n/a",
                        Price = product.Price
                    }) ;

                    


                }

            }

                

                return View(formInfo);
            }
        }
        
        public ActionResult AddProduct()
        {
            AddProduct vm = new AddProduct();
            CategoryModel categoryIds = new CategoryModel();

            using (var dbContext = new loveyoualattedbContext())
            {
                var categories = dbContext.Categories.ToList();
                foreach (var category in categories)
                {
                    categoryIds.categoryIdList.Add(new CategoryModel
                    {
                        IdCategory = category.IdCategory,
                        CategoryName = category.CategoryName
                    });
                }
            }

            vm.categoryDivID = categoryIds;
            return View(vm);
        }
        [HttpPost]
        public ActionResult AddProduct(AddProduct vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.MyImage != null)
                {
                    var img = vm.MyImage;

                    //Getting file meta data
                    string source_file_ext = Path.GetExtension(vm.MyImage.FileName);
                    var fileName = vm.DrinkName + source_file_ext;
                    var contentType = vm.MyImage.ContentType;

                    var uploads = Path.Combine(hostingEnvironment.WebRootPath, "Images");
                    var filePath = Path.Combine(uploads, fileName);
                    vm.MyImage.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                MySqlDatabase db = new MySqlDatabase(connectionString);
                using (MySqlConnection conn = db.Connection)
                {
                    var val = conn.CreateCommand() as MySqlCommand;
                    val.CommandText = @"SELECT drink_name FROM loveyoualattedb.drinkFood WHERE drink_name = '" + vm.DrinkName + "';";
                    var validDrink = val.ExecuteScalar();
                    if (validDrink != null)
                    {
                        if (vm.DrinkName.ToLower() == validDrink.ToString().ToLower())
                        {
                            ModelState.AddModelError("AddProductError", "Duplicate item.");
                        }
                    }
                    else
                    {
                        var cmd1 = conn.CreateCommand() as MySqlCommand;
                        cmd1.CommandText = @"INSERT INTO loveyoualattedb.drinkFood (idCategory, drink_name, drink_description) VALUES ('" + vm.CategoryID + "', '" + vm.DrinkName + "', '" + vm.DrinkDescription + "');" +
                            "SELECT LAST_INSERT_ID();";
                        var drinkID = cmd1.ExecuteScalar();
                        
                        var cmd2 = conn.CreateCommand() as MySqlCommand;
                        if (vm.CategoryID != 5) //if not food category
                        {
                            cmd2.CommandText = @"INSERT INTO loveyoualattedb.product (idDrinkFood, idSize, productSKU, price) VALUES ('" + drinkID + "', '1', '" + vm.SmallSKU + "','" + vm.SmallPrice + "');" +
                            "INSERT INTO loveyoualattedb.product (idDrinkFood, idSize, productSKU, price) VALUES ('" + drinkID + "', '2', '" + vm.MediumSKU + "','" + vm.MediumPrice + "');" +
                            "INSERT INTO loveyoualattedb.product (idDrinkFood, idSize, productSKU, price) VALUES ('" + drinkID + "', '3', '" + vm.LargeSKU + "','" + vm.LargePrice + "');";
                        }
                        else
                        {
                            cmd2.CommandText = @"INSERT INTO loveyoualattedb.product (idDrinkFood, productSKU, price) VALUES ('" + drinkID + "', '" + vm.ItemSKU + "','" + vm.ItemPrice + "');";
                        }
                        cmd2.ExecuteNonQuery();

                        vm.AddProductSuccess = "Succesfully added product!";
                        //repopulate dropdown
                        CategoryModel categoryIds = new CategoryModel();

                        using (var dbContext = new loveyoualattedbContext())
                        {
                            var categories = dbContext.Categories.ToList();
                            foreach (var category in categories)
                            {
                                categoryIds.categoryIdList.Add(new CategoryModel
                                {
                                    IdCategory = category.IdCategory,
                                    CategoryName = category.CategoryName
                                });
                            }
                        }

                        vm.categoryDivID = categoryIds;

                        return View(vm);
                    }
                }
            }
            if (!ModelState.IsValid)
            {
                //repopulate dropdown

                CategoryModel categoryIds = new CategoryModel();

                using (var dbContext = new loveyoualattedbContext())
                {
                    var categories = dbContext.Categories.ToList();
                    foreach (var category in categories)
                    {
                        categoryIds.categoryIdList.Add(new CategoryModel
                        {
                            IdCategory = category.IdCategory,
                            CategoryName = category.CategoryName
                        });
                    }
                }

                vm.categoryDivID = categoryIds;
                return View(vm);
            }
            
            return View(vm);

        }
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(AddCategory vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.MyImage != null)
                {
                    var img = vm.MyImage;

                    //Getting file meta data
                    string source_file_ext = Path.GetExtension(vm.MyImage.FileName);
                    var fileName = vm.CategoryName + source_file_ext;
                    var contentType = vm.MyImage.ContentType;

                    var uploads = Path.Combine(hostingEnvironment.WebRootPath, "Images");
                    var filePath = Path.Combine(uploads, fileName);
                    vm.MyImage.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                MySqlDatabase db = new MySqlDatabase(connectionString);
                using (MySqlConnection conn = db.Connection)
                {
                    var val = conn.CreateCommand() as MySqlCommand;
                    val.CommandText = @"SELECT categoryName FROM loveyoualattedb.category WHERE categoryName = '" + vm.CategoryName + "';";
                    var validDrink = val.ExecuteScalar();
                    if (validDrink != null)
                    {
                        if (vm.CategoryName.ToLower() == validDrink.ToString().ToLower())
                        {
                            ModelState.AddModelError("AddCategoryError", "Duplicate category.");
                            return View(vm);
                        }
                    }
                    else
                    {
                        var cmd = conn.CreateCommand() as MySqlCommand;
                        cmd.CommandText = @"INSERT INTO loveyoualattedb.category (categoryName, categoryDescription) VALUES ('" + vm.CategoryName + "', '" + vm.CategoryDescription + "');";
                        cmd.ExecuteNonQuery();

                        vm.AddCategorySuccess = "Succesfully added category!";

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



        public IActionResult GuestOrders()
        {
            List<ReceiptModel> guestReceipts = new List<ReceiptModel>();

            using(var dbContext = new loveyoualattedbContext())
            {
                
                var guestOrders = dbContext.UserOrders.Where(g => g.GuestUserId != null).ToList();
                    
                foreach (var order in guestOrders)
                guestReceipts.Add(new ReceiptModel() {
                    UserOrderId = order.UserOrderId,
                    UserId = order.GuestUserId,
                    OrderDate = order.OrderDate
                });
            }

            return View(guestReceipts);
        }


        public IActionResult GuestUserOrderReceipt(int Id)
        {
            ReceiptModel receipt = new ReceiptModel();
            using (var dbContext = new loveyoualattedbContext())
            {
                var products = dbContext.Products.ToList();
                var sizes = dbContext.Sizes.ToList();
                var drinks = dbContext.DrinkFoods.ToList();
                var addOnItems = dbContext.AddOns.ToList();
                var addOnList = dbContext.AddOnItemLists.ToList();


                var userOrder = dbContext.UserOrders.SingleOrDefault(uo => uo.UserOrderId == Id);
                receipt.OrderDate = userOrder.OrderDate;
                receipt.UserId = userOrder.UserId;
                receipt.UserOrderId = userOrder.UserOrderId;

                var orderItems = dbContext.OrderItems.Where(oi => oi.UserOrderId == Id);

                foreach (var item in orderItems)
                {
                    var product = products.Single(p => p.IdProduct == item.ProductId);
                    var addOns = addOnList.Where(i => i.CartAddOnItemId == item.CartAddOnItemId).ToList();
                    List<ReceiptAddOnModel> orderAddOns = new List<ReceiptAddOnModel>();
                    foreach (var addon in addOns)
                    {
                        orderAddOns.Add(new ReceiptAddOnModel()
                        {

                            addOnType = addOnItems.Single(a => a.AddOnId == addon.AddOnId).AddOnType,
                            addOnDescription = addOnItems.Single(a => a.AddOnId == addon.AddOnId).AddOnDescription
                        });

                    }

                    receipt.Items.Add(new ReceiptItemModel
                    {
                        ProductId = item.ProductId,
                        ProductDescription = drinks.Single(d => d.IdDrinkFood == product.IdDrinkFood).DrinkName,
                        sizeDescription = sizes.SingleOrDefault(s => s.IdSize == product.IdSize)?.Size1 ?? "n/a",
                        unitCost = item.LineItemCost,
                        addOnList = orderAddOns,
                        quantity = item.Quantity,
                        tax = item.Tax,
                        totalCost = item.TotalCost,
                        UserOrderId = userOrder.UserOrderId
                    });

                }


            }

            receipt.GrandTotal = receipt.Items.Sum(i => i.totalCost);

            return View(receipt);
        }













        public IActionResult AddEmployee()
        {
            EmployeeRegistrationModel newEmployee = new EmployeeRegistrationModel();
            return View(newEmployee);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeRegistrationModel request)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    string returnUrl = null;
                    var ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                    ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

                    var userCheck = await userManager.FindByEmailAsync(request.Email);
                    if (userCheck == null)
                    {
                        var user = new ApplicationUser
                        {
                            UserName = request.Email,
                            Email = request.Email,
                            firstName = request.FirstName,
                            lastName = request.LastName
                        };
                        var result = await userManager.CreateAsync(user, request.Password);
                        if (result.Succeeded)
                        {
                            if (request.isEmployee)
                            {
                                var newUser = await userManager.FindByIdAsync(user.Id);
                                var addToEmployeeRole = await userManager.AddToRoleAsync(newUser, "Employee");
                            }

                            var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                            var callbackUrl = Url.Page(
                                "/Account/ConfirmEmail",
                                pageHandler: null,
                                values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                                protocol: Request.Scheme);

                            await _emailSender.SendEmailAsync(request.Email, "Confirm your email",
                                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                            if (userManager.Options.SignIn.RequireConfirmedAccount)
                            {
                                return RedirectToAction("registrationconfirmation", new { email = request.Email, returnUrl = returnUrl });
                            }
                            else
                            {
                                await signInManager.SignInAsync(user, isPersistent: false);
                                return LocalRedirect(returnUrl);
                            }
                        }
                        else
                        {
                            if (result.Errors.Count() > 0)
                            {
                                foreach (var error in result.Errors)
                                {
                                    ModelState.AddModelError("message", error.Description);
                                }
                            }
                            return View(request);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("message", "Email already exists.");
                        return View(request);
                    }
                }

                return View(request);
            }
            return View(request);

        }


        public async Task<IActionResult> RegistrationConfirmation(string email, string returnUrl = null)
        {
            EmployeeConfirmRegistrationModel confirmNewEmployee = new EmployeeConfirmRegistrationModel();
            if (email == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound($"Unable to load user with email '{email}'.");
            }

            var Email = email;
            // Once you add a real email sender, you should remove this code that lets you confirm the account
            confirmNewEmployee.DisplayConfirmAccountLink = true;
            if (confirmNewEmployee.DisplayConfirmAccountLink)
            {
                var userId = await userManager.GetUserIdAsync(user);
                var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                confirmNewEmployee.EmailConfirmationUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                    protocol: Request.Scheme);
            }

            return View(confirmNewEmployee);
        }
        













    }
}
