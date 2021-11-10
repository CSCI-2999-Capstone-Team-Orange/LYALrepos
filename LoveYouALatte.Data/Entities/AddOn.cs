using System;
using System.Collections.Generic;

#nullable disable

namespace LoveYouALatte.Data.Entities
{
    public partial class AddOn
    {
        public int AddOnId { get; set; }
        public string AddOnType { get; set; }
        public string AddOnDescription { get; set; }
    }
}
