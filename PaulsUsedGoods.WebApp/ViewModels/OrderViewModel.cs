using System;
using System.Collections.Generic;

namespace PaulsUsedGoods.WebApp.ViewModels
{
    public class OrderViewModel
    {
        public int OrderId {get; set;}
        public string PersonName {get; set;}
        public DateTime OrderDate {get; set;}
        public double TotalOrderPrice {get; set;}
    }
}