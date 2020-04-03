using System;
using System.Collections.Generic;
using System.Text;
using PaulsUsedGoods.Domain.Logic;

namespace PaulsUsedGoods.Domain.Model
{
    public class Store
    {
        private string _name;
        private List<Item> _items;

        public int Id {get; set;}

        public Store()
        {
            _items = new List<Item>{};
        }

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
        public List<Item> Items
        {
            get => _items;
            set
            {
                if(value.Count == 0)
                {
                    throw new ArgumentException("There is no input items!", nameof(value));
                }
                _items = value;
            }
        }
    }
}
