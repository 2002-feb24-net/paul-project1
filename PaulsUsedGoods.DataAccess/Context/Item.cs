namespace PaulsUsedGoods.DataAccess.Context
{
    public class Item
    {
        public int ItemId {get; set;}
        public int StoreId {get; set;}
        public int? OrderId {get; set;}
        public int SellerId {get; set;}
        public int TopicId {get; set;}
        public string ItemName {get; set;}
        public string ItemDescription {get; set;}
        public double ItemPrice {get; set;}

        public virtual Store Store {get; set;}
        public virtual Order Order {get; set;}
        public virtual Seller Seller {get; set;}
        public virtual TopicOption TopicOption {get; set;}
    }
}