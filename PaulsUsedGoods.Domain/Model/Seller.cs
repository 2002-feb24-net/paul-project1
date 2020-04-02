using System;
using PaulsUsedGoods.Domain.Logic;

namespace PaulsUsedGoods.Domain.Model
{
    class Seller
    {
        private string _name;

        public int Id {get; set;}

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
