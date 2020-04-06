using System.ComponentModel.DataAnnotations;

namespace PaulsUsedGoods.WebApp.ViewModels
{
    public class SellerViewModel
    {
        [Display(Name = "ID")]
        [Required]
        public int SellerId {get; set;}

        [Display(Name = "Seller Name")]
        [Required]
        public string SellerName {get; set;}

        [Display(Name = "Number of Items for Sale")]
        [Required]
        public int Items {get; set;}

        [Display(Name = "Average Rating")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:#.##}")]
        public double AverageReview {get; set;}
    }
}