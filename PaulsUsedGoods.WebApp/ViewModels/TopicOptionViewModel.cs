using System.Collections.Generic;

namespace PaulsUsedGoods.WebApp.ViewModels
{
    public class TopicOptionViewModel
    {
        public int TopicOptionId {get; set;}
        public string TopicName {get; set;}

        public virtual ICollection<ItemViewModel> Item {get; set;}
    }
}