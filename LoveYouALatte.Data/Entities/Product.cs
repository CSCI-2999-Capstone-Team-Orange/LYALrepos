﻿using System;
using System.Collections.Generic;

#nullable disable

namespace LoveYouALatte.Data.Entities
{
    public partial class Product
    {
        public Product()
        {
            CartTables = new HashSet<CartTable>();
        }

        public int IdProduct { get; set; }
        public int IdDrink { get; set; }
        public int IdSize { get; set; }
        public float Price { get; set; }
        public int SizeIdSize { get; set; }
        public int DrinksIdDrinks { get; set; }

        public virtual Drink IdDrinkNavigation { get; set; }
        public virtual Size IdSizeNavigation { get; set; }
        public virtual ICollection<CartTable> CartTables { get; set; }
    }
}
