using System;
using System.Collections.Generic;

#nullable disable

namespace LoveYouALatte.Data.Entities
{
    public partial class GuestUser
    {
        public GuestUser()
        {
            CartTables = new HashSet<CartTable>();
            OrderItems = new HashSet<OrderItem>();
            UserOrders = new HashSet<UserOrder>();
        }

        public int GuestUserIncId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string GuestUserId { get; set; }

        public virtual ICollection<CartTable> CartTables { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<UserOrder> UserOrders { get; set; }
    }
}
