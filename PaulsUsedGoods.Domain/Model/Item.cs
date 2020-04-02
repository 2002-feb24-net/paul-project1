using System;
using System.Collections.Generic;
using System.Text;
using PaulsUsedGoods.Domain.Logic;

namespace PaulsUsedGoods.Domain.Model
{
    public class Item
    {
        private string _name;
        private string _description;
        private double _price;
        private string _topic;

        public int Id {get; set;}
        public Store Store {get; set;}

        public string Topic
        {
            get => _topic;
            set
            {
                if(value.Length == 0)
                {
                    throw new ArgumentException("There is no input topic!", nameof(value));
                }
                _topic = value;
            }
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

        public string Description
        {
            get => _description;
            set
            {
                if(value.Length == 0)
                {
                    throw new ArgumentException("There is no input description!", nameof(value));
                }
                if(!BadWordChecker.CheckWord(value))
                {
                    throw new ArgumentException("That description contains a banned term!", nameof(value));
                }
                _description = value;
            }
        }

        public double Price
        {
            get => _price;
            set
            {
                if(value == 0)
                {
                    throw new ArgumentException("There is no input price!", nameof(value));
                }
                _price = value;
            }
        }
    }
}
