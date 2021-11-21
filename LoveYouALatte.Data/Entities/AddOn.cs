using System;
using System.Collections.Generic;

#nullable disable

namespace LoveYouALatte.Data.Entities
{
    public partial class AddOn
    {
        public int quantity;

        public AddOn()
        {
            AddOnItemLists = new HashSet<AddOnItemList>();
        }

        public int AddOnId { get; set; }
        public string AddOnType { get; set; }
        public string AddOnDescription { get; set; }
        public decimal AddOnUnitPrice { get; set; }

        public virtual ICollection<AddOnItemList> AddOnItemLists { get; set; }
    }
}
