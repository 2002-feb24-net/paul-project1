using System.Collections.Generic;

namespace PaulsUsedGoods.DataAccess.Context
{
    public class TopicOption
    {
        public int TopicOptionId {get; set;}
        public string TopicName {get; set;}

        public virtual ICollection<Item> Item {get; set;}
    }
}