using System;
using System.Collections.Generic;
using System.Text;
using PaulsUsedGoods.Domain.Interfaces;

namespace PaulsUsedGoods.Domain.Model
{
    public class Order : IOrder
    {
        private int _userId;
        public List<int> itemsInOrder {get; set;}
        public string Username {get; set;}
        public int UserId
        {
            get => _userId;
            set
            {
                if(value == 0)
                {
                    throw new ArgumentException("User ID is 0! Cant be assigned to order.", nameof(value));
                }
                _userId = value;
            }
        }
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
                _items = value;
            }
        }

        public double Price
        {
            get => _price;
            set
            {
                _price = value;
            }
        }

        public void AddToCurrentOrder(int id)
        {
            itemsInOrder.Add(id);
        }
    }
}
