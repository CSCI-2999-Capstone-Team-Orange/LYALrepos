using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoveYouALatte.Data.Entities;


namespace LoveYouALatte.Data
{
    public static class loveyoualatteDbContextExtensions
    {
        public static UserOrder GetUserOrder(this loveyoualattedbContext dbContext, int orderId)
        {
            //dbContext.UserOrders.Include()
            return null;
        }
    }
}
