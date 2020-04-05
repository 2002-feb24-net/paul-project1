using System.Collections.Generic;

namespace PaulsUsedGoods.WebApp.ViewModels
{
    public class StoreViewModel
    {
        public int StoreId {get; set;}
        public string LocationName {get; set;}

        public virtual ICollection<ItemViewModel> Item {get; set;}
        public virtual ICollection<PersonViewModel> Person {get; set;}

    }
}
