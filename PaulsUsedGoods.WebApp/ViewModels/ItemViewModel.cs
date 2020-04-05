using System.ComponentModel.DataAnnotations;

namespace PaulsUsedGoods.WebApp.ViewModels
{
    public class ItemViewModel
    {
        [Display(Name = "ID")]
        public int ItemId {get; set;}

        [Display(Name = "Store Name")]
        [Required]
        public string StoreName {get; set;}

        [Display(Name = "Order ID")]
        public int? OrderId {get; set;}

        [Display(Name = "Seller Name")]
        [Required]
        public string SellerName {get; set;}

        [Display(Name = "Topic Name")]
        [Required]
        public string TopicName {get; set;}

        [Display(Name = "Item Name")]
        [Required]
        public string ItemName {get; set;}

        [Display(Name = "Item Description")]
        [Required]
        [DataType(DataType.MultilineText)]
        [MaxLength(1024)]
        public string ItemDescription {get; set;}

        [Display(Name = "Item Price")]
        [Required]
        public double ItemPrice {get; set;}
    }
}