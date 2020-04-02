using System.Collections.Generic;

namespace PaulsUsedGoods.DataAccess.Context
{
    public class Store
    {
        public int StoreId {get; set;}
        public string LocationName {get; set;}

        public virtual ICollection<Item> Item {get; set;}
        public virtual ICollection<Person> Person {get; set;}

    }
}