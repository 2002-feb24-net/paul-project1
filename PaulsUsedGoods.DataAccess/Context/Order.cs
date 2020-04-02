using System;
using System.Collections.Generic;

namespace PaulsUsedGoods.DataAccess.Context
{
    public class Order
    {
        public int OrderId {get; set;}
        public int PersonId {get; set;}
        public DateTime OrderDate {get; set;}
        public double TotalOrderPrice {get; set;}

        public virtual Person Person {get; set;}
        public virtual ICollection<Item> Item {get; set;}
    }
}