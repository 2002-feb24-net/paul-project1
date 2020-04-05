using System;
using System.Collections.Generic;

namespace PaulsUsedGoods.WebApp.ViewModels
{
    public class OrderViewModel
    {
        public int OrderId {get; set;}
        public int PersonId {get; set;}
        public DateTime OrderDate {get; set;}
        public double TotalOrderPrice {get; set;}

        public virtual PersonViewModel Person {get; set;}
        public virtual ICollection<ItemViewModel> Item {get; set;}
    }
}