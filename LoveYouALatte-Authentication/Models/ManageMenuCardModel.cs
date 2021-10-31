using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoveYouALatte_Authentication.Models
{
    public class ManageMenuCardModel
    {
        public MenuCardModel menuCards { get; set; }

        public ProductModel updateProduct { get; set; }
        public List<ProductModel> updateProductsList { get; set; }
    }
}
