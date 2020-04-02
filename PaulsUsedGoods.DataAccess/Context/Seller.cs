using System.Collections.Generic;

namespace PaulsUsedGoods.DataAccess.Context
{
    public class Seller
    {
        public int SellerId {get; set;}
        public string SellerName {get; set;}

        public virtual ICollection<Item> Item {get; set;}
        public virtual ICollection<Review> Review {get; set;}
    }
}