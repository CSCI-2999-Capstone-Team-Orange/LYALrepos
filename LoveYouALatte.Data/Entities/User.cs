using System;
using System.Collections.Generic;

#nullable disable

namespace LoveYouALatte.Data.Entities
{
    public partial class User
    {
        public User()
        {
            Carts = new HashSet<Cart>();
        }

        public int IdUser { get; set; }
        public string Username { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }
    }
}
