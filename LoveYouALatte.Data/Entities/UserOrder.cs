using System;
using System.Collections.Generic;

#nullable disable

namespace LoveYouALatte.Data.Entities
{
    public partial class UserOrder
    {
        public UserOrder()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int UserOrderId { get; set; }
        public string UserId { get; set; }
        public string GuestUserId { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual GuestUser GuestUser { get; set; }
        public virtual AspNetUser User { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
