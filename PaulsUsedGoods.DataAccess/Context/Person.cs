using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaulsUsedGoods.DataAccess.Context
{
    public class Person
    {
        public int PersonId {get; set;}
        public int StoreId {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string Username {get; set;}
        public string Password {get; set;}
        public bool Employee {get; set;}

        public virtual Store Store {get; set;}
        public virtual ICollection<Order> Order {get; set;}
        public virtual ICollection<Review> Review {get; set;}
    }
}