using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace PaulsUsedGoods.WebApp.ViewModels
{
    public class StoreViewModel
    {
        [Display(Name = "ID")]
        [Required]
        public int StoreId {get; set;}

        [Display(Name = "Store Name")]
        [Required]
        public string LocationName {get; set;}

        [Display(Name = "Stock")]
        [Required]
        public int ItemCount {get; set;}

    }
}
