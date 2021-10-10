using System;
using System.Collections.Generic;

#nullable disable

namespace LoveYouALatte.Data.Entities
{
    public partial class User
    {
        public User()
        {
            CartTables = new HashSet<CartTable>();
        }

        public int IdUser { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }

        public virtual ICollection<CartTable> CartTables { get; set; }
    }
}
