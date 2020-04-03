using System;
using PaulsUsedGoods.Domain.Logic;
using System.Collections.Generic;

namespace PaulsUsedGoods.Domain.Model
{
    public class Seller
    {
        private string _name;

        public int Id {get; set;}
        public List<Item> Items {get; set;}
        public List<Review> Reviews {get; set;}

        public string Name
        {
            get => _name;
            set
            {
                if(value.Length == 0)
                {
                    throw new ArgumentException("There is no input name!", nameof(value));
                }
                value = CaseConverter.Convert(value);
                if(!BadWordChecker.CheckWord(value))
                {
                    throw new ArgumentException("That name contains a banned term!", nameof(value));
                }
                _name = value;
            }
        }
    }
}
