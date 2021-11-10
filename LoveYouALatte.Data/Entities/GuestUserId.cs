using System;
using System.Collections.Generic;

#nullable disable

namespace LoveYouALatte.Data.Entities
{
    public partial class GuestUserId
    {
        public int IdGuest { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
