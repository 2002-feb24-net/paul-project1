using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace PaulsUsedGoods.WebApp.ViewModels
{
    public class TopicOptionViewModel
    {
        [Display(Name = "ID")]
        [Required]
        public int TopicOptionId {get; set;}

        [Display(Name = "Topic Name")]
        [Required]
        public string TopicName {get; set;}

        public virtual ICollection<ItemViewModel> Item {get; set;}
    }
}