using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PaulsUsedGoods.WebApp.ViewModels
{
    public class ItemAnLogInViewModel
    {
        public LogInViewModel LogInViewModel {get; set;}
        public List<ItemViewModel> Items {get; set;}

        public List<ItemViewModel> selectedItems {get; set;}
    }
}