using System;
using System.Collections.Generic;
using System.Text;
using PaulsUsedGoods.Domain.Interfaces;

namespace PaulsUsedGoods.Domain.Model
{
    public class Order : IOrder
    {
        public List<int> itemsInOrder {get; set;}
        public string Username {get; set;}
        private DateTime _date;
        private double _price;
        private List<Item> _items;

        public int Id {get; set;}

        public Order()
        {
            itemsInOrder = new List<int>();
        }

        public DateTime Date
        {
            get => _date;
            set
            {
                if(value.Year < 2020)
                {
                    throw new ArgumentException("This store didn't exist!", nameof(value));
                }
                _date = value;
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

        public void AddToCurrentOrder(int id)
        {
            itemsInOrder.Add(id);
        }
    }
}
