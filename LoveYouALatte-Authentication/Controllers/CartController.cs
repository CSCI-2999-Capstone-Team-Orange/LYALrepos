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

namespace LoveYouALatte_Authentication.Controllers
{
    public class CartController : Controller
    {


        public ActionResult AddToCart(int UserID, int ProductID, int Quantity)
        {
            //pass info from menu into cart table


            //return menu page
            return RedirectToAction("Menu", "Home");
        }


        public List<Cart> GetCart(string UserID)
        {
            var cartQuery = new List<Cart>();
            using (var context = new loveyoualattedbContext())
            {
                cartQuery = context.CartTables.Select(x => new Cart
                {
                    IdUser = x.IdUser,
                    IdProduct = x.IdProduct,
                    CartId = x.IdCartTable,
                    Quantity = x.Quantity
                }).Where(x => x.IdUser == UserID).ToList();
            }

            return cartQuery;
        }

        public List<Models.Product> GetProducts()
        {
            var productQuery = new List<Models.Product>();
            //cart info passed to list
            using (var context = new loveyoualattedbContext())
            {
                productQuery = context.Products.Select(x => new Models.Product
                {
                    DrinkId = x.IdDrink,
                    ProductId = x.IdProduct,
                    Price = x.Price,
                    SizeId = x.IdSize
                }).ToList();
            }

            return productQuery;
        }
    }
}
