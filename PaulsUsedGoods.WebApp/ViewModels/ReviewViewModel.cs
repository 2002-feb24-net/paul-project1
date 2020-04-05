using System.ComponentModel.DataAnnotations;

namespace PaulsUsedGoods.WebApp.ViewModels
{
    public class ReviewViewModel
    {
        [Display(Name = "ID")]
        [Required]
        public int ReviewId {get; set;}

        [Display(Name = "Customer Name")]
        [Required]
        public string UserName {get; set;}

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password did not match.")]
        public string Password {get; set;}

        [Display(Name = "Seller Name")]
        [Required]
        public string SellerName {get; set;}

        [Display(Name = "Rating")]
        [Required]
        [Range(1,10)]
        public int Score {get; set;}

        [Display(Name = "Comment")]
        [MaxLength(2048)]
        public string Comment {get; set;}
    }
}