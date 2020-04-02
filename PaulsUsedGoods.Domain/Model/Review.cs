using System;
using System.Collections.Generic;
using System.Text;
using PaulsUsedGoods.Domain.Logic;

namespace PaulsUsedGoods.Domain.Model
{
    class Review
    {
        private int _score;
        private string _comment;

        public int Id {get; set;}

        public int Score
        {
            get => _score;
            set 
            {
                if(value < 1 || value > 10)
                {
                    throw new ArgumentException("Chosen number is out of bounds!", nameof(value));
                }
                _score = value;
            }
        }

        public string Comment
        {
            get => _comment;
            set 
            {
                if(value.Length == 0)
                {
                    throw new ArgumentException("There is no input comment!", nameof(value));
                }
                if(!BadWordChecker.CheckWord(value))
                {
                    throw new ArgumentException("That name contains a banned term!", nameof(value));
                }
                _comment = value;
            }
        }
    }
}
