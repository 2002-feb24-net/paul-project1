namespace PaulsUsedGoods.DataAccess.Context
{
    public class Review
    {
        public int ReviewId {get; set;}
        public int PersonId {get; set;}
        public int SellerId {get; set;}
        public int Score {get; set;}
        public string Comment {get; set;}

        public virtual Person Person {get; set;}
        public virtual Seller Seller {get; set;}
    }
}