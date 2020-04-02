using System;
using System.Collections.Generic;
using System.Text;
using PaulsUsedGoods.Domain.Logic;

namespace PaulsUsedGoods.Domain.Model
{
    public struct TopicOption
    {
        private string _topic;

        public int Id {get; set;}

        public string Topic
        {
            get => _topic;
            set
            {
                if(value.Length == 0)
                {
                    throw new ArgumentException("There is no input topic!", nameof(value));
                }
                value = CaseConverter.Convert(value);
                if(!BadWordChecker.CheckWord(value))
                {
                    throw new ArgumentException("That name contains a banned term!", nameof(value));
                }
                _topic = value;
            }
        }
    }
}
