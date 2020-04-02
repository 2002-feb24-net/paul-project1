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
        private List<TopicOption> _topics;

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

        public List<TopicOption> Topics
        {
            get => _topics;
            set
            {
                if(value.Count == 0)
                {
                    throw new ArgumentException("There is no input items!", nameof(value));
                }
                _topics = value;
            }
        }

        public Store()
        {
            _items = new List<Item>{};
        }
    }
}
