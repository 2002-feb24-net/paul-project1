using System.Collections.Generic;

namespace PaulsUsedGoods.WebApp.ViewModels
{
    public class SellerViewModel
    {
        public int SellerId {get; set;}
        public string SellerName {get; set;}

        public virtual ICollection<ItemViewModel> Item {get; set;}
        public virtual ICollection<ReviewViewModel> Review {get; set;}
    }
}