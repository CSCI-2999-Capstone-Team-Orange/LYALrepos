using System;
using System.Collections.Generic;

#nullable disable

namespace LoveYouALatte.Data.Entities
{
    public partial class CartTable
    {
        public int IdCartTable { get; set; }
        public int IdProduct { get; set; }
        public int IdUser { get; set; }
        public int Quantity { get; set; }
        public double TotalCost { get; set; }
        public byte Purchased { get; set; }
        public int UserIdUser { get; set; }
        public int ProductIdProduct { get; set; }
        public int ProductIdDrinks { get; set; }
        public int ProductIdSize { get; set; }
        public int ProductSizeIdSize { get; set; }
        public int ProductDrinksIdDrinks { get; set; }

        public virtual Product Product { get; set; }
        public virtual User UserIdUserNavigation { get; set; }
    }
}
