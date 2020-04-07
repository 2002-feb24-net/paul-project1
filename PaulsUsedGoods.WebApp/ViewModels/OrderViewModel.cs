using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace PaulsUsedGoods.WebApp.ViewModels
{
    public class OrderViewModel
    {
        [Display(Name = "Sort Method")]
        public string SortMethod {get; set;}

        [Display(Name = "ID")]
        [Required]
        public int OrderId {get; set;}

        [Display(Name = "User's Name")]
        [Required]
        public string PersonName {get; set;}

        [Display(Name = "Store Name")]
        [Required]
        public string StoreName {get; set;}

        [Display(Name = "Date Ordered")]
        [Required]
        public DateTime OrderDate {get; set;}

        [Display(Name = "Total Price")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:#.##}")]
        public double TotalOrderPrice {get; set;}


        [Display(Name = "Items In Order")]
        public List<ItemViewModel> Items {get; set;}
    }
}