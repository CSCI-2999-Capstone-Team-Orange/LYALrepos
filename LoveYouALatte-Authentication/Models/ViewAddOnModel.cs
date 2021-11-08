using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveYouALatte_Authentication.Models
{
    public class ViewAddOnModel
    {
        public ViewAddOnModel()
        { this.addOnList = new List<AddOnModel>(); }
        
        public List<AddOnModel> addOnList { get; set; }

        
        

    }
}
