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
        string connectionString = "server=aa124gktif3j980.cjiyeakoxxft.us-east-1.rds.amazonaws.com; port=3306; database=loveyoualattedb; uid=test; pwd=orange1234;";

        [HttpGet]
        public ActionResult AddToCart(int productid, int quantity)
        {
            var UserID = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //pass info from menu into cart table
            //insert params into cart table
            //return menu page

            MenuViewModel vm = new MenuViewModel();

            MySqlDatabase db = new MySqlDatabase(connectionString);
            using (MySqlConnection conn = db.Connection)
            {
                var cmd = conn.CreateCommand() as MySqlCommand;
                cmd.CommandText = @"INSERT INTO loveyoualattedb.CartTable(idUser, idProduct, quantity) VALUES ('" + UserID + "', " + productid + ", " + quantity + ")";
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
        public ActionResult Menu()
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

        [HttpGet]
        [Authorize]
        public ActionResult Checkout()
        {
            CheckoutViewModel vm = new CheckoutViewModel();
            var UserID = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cartList = new List<Cart>();
            //cart info passed to list

            MySqlDatabase db = new MySqlDatabase(connectionString);
            using (MySqlConnection conn = db.Connection)
            {
                var cmd = conn.CreateCommand() as MySqlCommand;
                cmd.CommandText = @"
                    SELECT idCartTable, idUser, prod.idProduct, quantity, prod.price, size.size, drink.drink_name FROM loveyoualattedb.CartTable cart
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
                        cart.SizeName = dr["size"] as String ?? string.Empty;
                        cart.DrinkName = dr["drink_name"] as String ?? string.Empty;

                        cartList.Add(cart);
                    }
                }
            }
            vm.Carts = cartList;
            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Purchase()
        {
            var UserID = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orderId = 0;

            using (var dbContext = new loveyoualattedbContext())
            {
                var dbCart = dbContext.CartTables.Where(s => s.IdUser == UserID).ToList();
                var newUserOrder = new UserOrder()
                {
                    UserId = UserID,
                    OrderDate = DateTime.Now
                };

                var products = dbContext.Products.ToList();

                foreach (var cartItem in dbCart)
                {
                    // TODO: remove this hack when lineItemCost is being set correctly
                    var product = products.Single(p => p.IdProduct == cartItem.IdProduct);
                    decimal subTotal = product.Price * (decimal)cartItem.Quantity;
                    decimal tax = 0.075m * subTotal;
                    decimal total = subTotal + tax;

                    newUserOrder.OrderItems.Add(
                        new OrderItem()
                        {
                            ProductId = cartItem.IdProduct,
                            Quantity = cartItem.Quantity,
                            LineItemCost = product.Price,
                            Tax = tax,
                            TotalCost = total
                        });
                }

                dbContext.UserOrders.Add(newUserOrder);
                dbContext.SaveChanges();
                orderId = newUserOrder.UserOrderId;

                //foreach(var value in dbCart)
                //{
                //    dbContext.CartTables.Remove(value);
                //    dbContext.SaveChanges();
                //}

                dbContext.CartTables.RemoveRange(dbCart);
                dbContext.SaveChanges();
            }

            return RedirectToAction("Receipt", new { id = orderId });
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


                var userOrder = dbContext.UserOrders.SingleOrDefault(uo => uo.UserOrderId == id);
                receipt.OrderDate = userOrder.OrderDate;
                receipt.UserId = userOrder.UserId;
                receipt.UserOrderId = userOrder.UserOrderId;

                var orderItems = dbContext.OrderItems.Where(oi => oi.UserOrderId == id);

                foreach (var item in orderItems)
                {
                    var product = products.Single(p => p.IdProduct == item.ProductId);
                    receipt.Items.Add(new ReceiptItemModel
                    {
                        ProductId = item.ProductId,
                        ProductDescription = drinks.Single(d => d.IdDrinks == product.IdDrink).DrinkName,
                        sizeDescription = sizes.Single(s => s.IdSize == product.IdSize).Size1,
                        unitCost = item.LineItemCost,
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
