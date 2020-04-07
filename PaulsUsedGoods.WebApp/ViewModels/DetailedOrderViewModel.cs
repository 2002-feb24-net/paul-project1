using System;
using System.Collections.Generic;

namespace PaulsUsedGoods.WebApp.ViewModels
{
    public class DetailedOrderViewModel
    {
        public string PersonName {get; set;}
        public string StoreName {get; set;}
        public DateTime DateOfOrder {get; set;}
        public double Price {get; set;}
        public List<PaulsUsedGoods.Domain.Model.Item> ItemList {get; set;}
        public ItemViewModel SuggestedItem {get; set;}
    }
}