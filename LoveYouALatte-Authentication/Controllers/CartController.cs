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
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;


namespace LoveYouALatte_Authentication.Controllers
{
    public class CartController : Controller
    {
        string connectionString = "server=authtest.cjiyeakoxxft.us-east-1.rds.amazonaws.com; port=3306; database=loveyoualattedb; uid=test; pwd=orange1234;";


    //private MenuViewModel DisplayMenu()




        [HttpGet]
        public ActionResult AddToCart(int productid, int quantity, decimal totalPrice, decimal lineTax, decimal lineCost)
        {
            //userId of the user that is currently logged in
            var UserID = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            MenuViewModel vm = new MenuViewModel();

            MySqlDatabase db = new MySqlDatabase(connectionString);
            using (MySqlConnection conn = db.Connection)
            {
                var cmd = conn.CreateCommand() as MySqlCommand;
                cmd.CommandText = @"INSERT INTO loveyoualattedb.CartTable(idUser, idProduct, quantity, lineItemCost, lineTax, lineCost) VALUES ('" + UserID + "', " + productid + ", " + quantity + ", " + totalPrice + ", " + lineTax + ", " + lineCost + ")";
                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    return Content("Success");
                }
                else
                {
                    return Content("Error");
                }
            }


        }

        [HttpGet]
        public ActionResult UpdateCartQuantity(int cartid, int quantity, decimal totalPrice, decimal lineTax, decimal lineCost)
        {
            //userId of the user that is currently logged in
            var UserID = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            MenuViewModel vm = new MenuViewModel();

            MySqlDatabase db = new MySqlDatabase(connectionString);
            using (MySqlConnection conn = db.Connection)
            {
                var cmd = conn.CreateCommand() as MySqlCommand;
                cmd.CommandText = @"UPDATE loveyoualattedb.CartTable cart
                                    SET cart.quantity = " + quantity + ", cart.lineitemcost = " + totalPrice + ", cart.lineTax = " + lineTax + ", lineCost = " + lineCost + 
                                    " WHERE (cart.idCartTable = " + cartid + " AND cart.idUser = '" + UserID + "')";
                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    return Content("Success");
                }
                else
                {
                    return Content("Error");
                }
            }
        }
       
        [HttpGet]
        public ActionResult Category()
        {
            CategoryViewModel vm = new CategoryViewModel();

            var categoryList = new List<CategoryModel>();

            MySqlDatabase db = new MySqlDatabase(connectionString);
            using (MySqlConnection conn = db.Connection)
            {
                var cmd = conn.CreateCommand() as MySqlCommand;
                cmd.CommandText = @"
                    SELECT idDrinks, cat.idCategory, cat.categoryName, cat.categoryDescription, drink_name, drink_description FROM loveyoualattedb.drinks drink
                    INNER JOIN loveyoualattedb.category cat ON drink.idCategory = cat.idCategory";

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        CategoryModel cat = new CategoryModel();

                        cat.IdDrinks = dr["idDrinks"] as int? ?? default(int);
                        cat.IdCategory = dr["idCategory"] as int? ?? default(int);
                        cat.CategoryName = dr["categoryName"] as String ?? string.Empty;
                        cat.CategoryDescription = dr["categoryDescription"] as String ?? string.Empty;
                        cat.DrinkName = dr["drink_name"] as String ?? string.Empty;
                        cat.DrinkDescription = dr["drink_description"] as String ?? string.Empty;

                        categoryList.Add(cat);
                    }
                }
            }
            vm.Categories = categoryList;
            return View(vm);
        }
        [HttpGet]
        public ActionResult Menu(int catid)
        {
            MenuViewModel vm = new MenuViewModel();

            var productList = new List<ProductKG>();

            MySqlDatabase db = new MySqlDatabase(connectionString);
            using (MySqlConnection conn = db.Connection)
            {
                var cmd = conn.CreateCommand() as MySqlCommand;
                cmd.CommandText = @"
                    SELECT idProduct, idDrink, size.idSize, price, drink_name, drink_description, cat.categoryName, size.size FROM loveyoualattedb.product prod 
                        INNER JOIN loveyoualattedb.drinks drink ON prod.idDrink = drink.idDrinks
                        INNER JOIN loveyoualattedb.size size ON prod.idSize = size.idSize
                        INNER JOIN loveyoualattedb.category cat ON drink.idCategory = cat.idCategory
                    WHERE cat.idCategory = " + catid;

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ProductKG prod = new ProductKG();

                        prod.ProductId = dr["idProduct"] as int? ?? default(int);
                        prod.DrinkId = dr["idDrink"] as int? ?? default(int);
                        prod.SizeId = dr["idSize"] as int? ?? default(int);
                        prod.Price = dr["price"] as decimal? ?? default(decimal);
                        prod.DrinkName = dr["drink_name"] as String ?? string.Empty;
                        prod.DrinkDescription = dr["drink_description"] as String ?? string.Empty;
                        prod.DrinkCategory = dr["categoryName"] as String ?? string.Empty;
                        prod.SizeName = dr["size"] as String ?? string.Empty;

                        productList.Add(prod);
                    }
                }
            }
            vm.Products = productList;

            ViewAddOnModel dbAddOnList = new ViewAddOnModel();
            
            vm.addOns = dbAddOnList;
        
            
            using (var dbContext = new loveyoualattedbContext()) {

                var addOns = dbContext.AddOns.ToList();
                foreach(var addOn in addOns)
                {
                    dbAddOnList.addOnList.Add(new AddOnModel()
                    {
                        addOnId = addOn.AddOnId,
                        addOnType = addOn.AddOnType,
                        addOnDescription = addOn.AddOnDescription
                    });
                }
            
            }
            
            
            
            return View(vm);
        }


        
        [HttpPost]
        public ActionResult addAddOns([FromBody]List<AddOnModel> drinkAddOns) {
            var addOns = drinkAddOns.Where(a => a.isSelected == true).ToList();
            using (var dbContext = new loveyoualattedbContext())
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var cartTable = dbContext.CartTables.Where(a => a.IdUser == userId).ToList();
                var lastCartTableId = cartTable.OrderBy(ct => ct.IdCartTable).LastOrDefault().IdCartTable;

                var newCartItemId = new CartAddOnItem()
                {
                    IdCartTable = lastCartTableId
                };



                foreach (var addOn in addOns)
                {
                    newCartItemId.AddOnItemLists.Add(new AddOnItemList()
                    {
                        AddOnId = addOn.addOnId
                    });
                }

                dbContext.CartAddOnItems.Add(newCartItemId);
                dbContext.SaveChanges();

                int cartItemId = newCartItemId.CartAddOnItemId;
                var lastCartTableItem = dbContext.CartTables.Single(id => id.IdCartTable == lastCartTableId);
                lastCartTableItem.CartAddOnItem = newCartItemId;
                dbContext.SaveChanges();

                if (addOns.Count() == 0)
                {
                    return Json(new { success = true, responseText = "No Addons were added" });
                }
                else if (addOns.Count >0)
                {
                    return Json(new { success = true, responseText = "Addons have been added" });
                }
                else
                {
                    return Json(new { success = false, responseText = "Addons were not added" });
                }

            }
            
        } 



       



        [HttpGet]
        [Authorize]
        public ActionResult Checkout()
        {
            ViewBag.ErrorMessage = "Your cart is empty. Please add items to cart.";
            CheckoutViewModel vm = new CheckoutViewModel();
            var UserID = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cartList = new List<Cart>();
            //cart info passed to list
      
            MySqlDatabase db = new MySqlDatabase(connectionString);
            using (MySqlConnection conn = db.Connection)
            {
                var cmd = conn.CreateCommand() as MySqlCommand;
                cmd.CommandText = @"
                    SELECT idCartTable, idUser, prod.idProduct, quantity, prod.price, lineItemCost, lineTax, lineCost, size.size, drink.drink_name FROM loveyoualattedb.CartTable cart
                    Inner JOIN loveyoualattedb.product prod ON cart.idProduct = prod.idProduct
                    INNER JOIN loveyoualattedb.drinks drink ON prod.idDrink = drink.idDrinks
                    INNER JOIN loveyoualattedb.size size ON prod.idSize = size.idSize
                    WHERE idUser = '" + UserID + "'";

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Cart cart = new Cart();

                        cart.CartId = dr["idCartTable"] as int? ?? default(int);
                        cart.IdUser = dr["idUser"] as String ?? string.Empty;
                        cart.IdProduct = dr["idProduct"] as int? ?? default(int);
                        cart.Quantity = dr["quantity"] as int? ?? default(int);
                        cart.Price = dr["price"] as decimal? ?? default(decimal);
                        cart.TotalPrice = dr["lineItemCost"] as decimal? ?? default(decimal);
                        cart.LineTax = dr["lineTax"] as decimal? ?? default(decimal);
                        cart.LineCost = dr["lineCost"] as decimal? ?? default(decimal);
                        cart.SizeName = dr["size"] as String ?? string.Empty;
                        cart.DrinkName = dr["drink_name"] as String ?? string.Empty;

                        cartList.Add(cart);
                    }
                }
            }

            List<CheckoutItemModel> checkoutItemList = new List<CheckoutItemModel>();
            

            using (var dbContext = new loveyoualattedbContext())
            {

                var products = dbContext.Products.ToList();
                var sizes = dbContext.Sizes.ToList();
                var drinks = dbContext.Drinks.ToList();
                var addOnItems = dbContext.AddOns.ToList();
                var addOnList = dbContext.AddOnItemLists.ToList();

                var cartItems = dbContext.CartTables.Where(a => a.IdUser == UserID).ToList();
    

                foreach (var item in cartItems)
                {
                    var product = products.Single(p => p.IdProduct == item.IdProduct);
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

                    checkoutItemList.Add(new CheckoutItemModel
                    {
                        cartTableId = item.IdCartTable,
                        ProductId = item.IdProduct,
                        ProductDescription = drinks.Single(d => d.IdDrinks == product.IdDrink).DrinkName,
                        sizeDescription = sizes.Single(s => s.IdSize == product.IdSize).Size1,
                        unitCost = item.LineItemCost,
                        addOnList = orderAddOns,
                        quantity = item.Quantity,
                    });

                }


            }

            vm.checkoutItems = checkoutItemList;


            vm.Carts = cartList;
            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Purchase()
        {
            
            var UserID = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orderId = 0;

            var timeUtc = DateTime.UtcNow;
            var easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            var today = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, easternZone);


            using (var dbContext = new loveyoualattedbContext())
            {
                var dbCart = dbContext.CartTables.Where(s => s.IdUser == UserID).ToList();
                if (dbCart.Count != 0)
                {
                    var newUserOrder = new UserOrder()
                    {

                        UserId = UserID,
                        OrderDate = today
                    };

                    var products = dbContext.Products.ToList();

                    foreach (var cartItem in dbCart)
                    {
                        // TODO: remove this hack when lineItemCost is being set correctly
                        //var product = products.Single(p => p.IdProduct == cartItem.IdProduct);
                        //decimal subTotal = product.Price * (decimal)cartItem.Quantity;
                        //decimal tax = 0.075m * subTotal;
                        //decimal total = subTotal + tax;

                        //var cartAddOnSwap = dbContext.CartAddOnItems.Single(pd => pd.IdCartTable == cartItem.IdCartTable);
                        

                        newUserOrder.OrderItems.Add(
                            new OrderItem()
                            {
                                ProductId = cartItem.IdProduct,
                                Quantity = cartItem.Quantity,
                                CartAddOnItemId = cartItem.CartAddOnItemId,
                                LineItemCost = (cartItem.LineItemCost / cartItem.Quantity),
                                Tax = (cartItem.LineItemCost * 0.075m),
                                TotalCost = (cartItem.LineItemCost * 0.075m) + cartItem.LineItemCost,
                            });

                        //cartAddOnSwap.OrderItemId = newUserOrder.OrderItems.First().OrderItemId;
                    }

                    
                    dbContext.UserOrders.Add(newUserOrder);
                    dbContext.SaveChanges();
                    orderId = newUserOrder.UserOrderId;


                    dbContext.CartTables.RemoveRange(dbCart);
                    dbContext.SaveChanges();
               

                    return RedirectToAction("Receipt", new { id = orderId });
                }
                else
                {
                    ViewBag.ErrorMessage = "Your cart is empty. Please add items to cart.";
                    return RedirectToAction("Checkout");
                    
                    
                }
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult ClearCart()
        {
            var UserID = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            using (var dbContext = new loveyoualattedbContext())
            {
                var dbCart = dbContext.CartTables.Where(s => s.IdUser == UserID).ToList();
                dbContext.CartTables.RemoveRange(dbCart);
                dbContext.SaveChanges();
            }

            return RedirectToAction("Checkout");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Remove(int cartid)
        {
            //userId of the user that is currently logged in
            var UserID = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            MenuViewModel vm = new MenuViewModel();

            MySqlDatabase db = new MySqlDatabase(connectionString);
            using (MySqlConnection conn = db.Connection) { 

                
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM loveyoualattedb.CartTable cart
                                WHERE (cart.idCartTable = " + cartid + " AND cart.idUser = '" + UserID + "')";
            int result = cmd.ExecuteNonQuery();

            if (result > 0)
            {
                return Content("Success");
            }
            else
            {
                return Content("Error");
            }
            }

        }


        [HttpGet]
        [Authorize]
        public ActionResult Receipt(int id)
        {
            //var UserID = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            //ReceiptListModel userReceipt = new ReceiptListModel();

            ReceiptModel receipt = new ReceiptModel();
            using (var dbContext = new loveyoualattedbContext())
            {
                var products = dbContext.Products.ToList();
                var sizes = dbContext.Sizes.ToList();
                var drinks = dbContext.Drinks.ToList();
                var addOnItems = dbContext.AddOns.ToList();
                var addOnList = dbContext.AddOnItemLists.ToList();


                var userOrder = dbContext.UserOrders.SingleOrDefault(uo => uo.UserOrderId == id);
                receipt.OrderDate = userOrder.OrderDate;
                receipt.UserId = userOrder.UserId;
                receipt.UserOrderId = userOrder.UserOrderId;

                var orderItems = dbContext.OrderItems.Where(oi => oi.UserOrderId == id);
               
                foreach (var item in orderItems)
                {
                    var product = products.Single(p => p.IdProduct == item.ProductId);
                    var addOns = addOnList.Where(i => i.CartAddOnItemId == item.CartAddOnItemId).ToList();
                    List<ReceiptAddOnModel> orderAddOns = new List<ReceiptAddOnModel>();
                    foreach (var addon in addOns)
                    {
                        orderAddOns.Add(new ReceiptAddOnModel() { 
                      
                            addOnType = addOnItems.Single(a => a.AddOnId == addon.AddOnId).AddOnType,
                            addOnDescription = addOnItems.Single(a => a.AddOnId == addon.AddOnId).AddOnDescription
                        });

                    }

                    receipt.Items.Add(new ReceiptItemModel
                    {
                        ProductId = item.ProductId,
                        ProductDescription = drinks.Single(d => d.IdDrinks == product.IdDrink).DrinkName,
                        sizeDescription = sizes.Single(s => s.IdSize == product.IdSize).Size1, 
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






    }
}
