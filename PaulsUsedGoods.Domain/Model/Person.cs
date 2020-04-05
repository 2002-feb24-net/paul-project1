using System;
using System.Collections.Generic;
using System.Text;
using PaulsUsedGoods.Domain.Logic;

namespace PaulsUsedGoods.Domain.Model
{
    public class Person
    {
        private string _firstname;
        private string _lastname;
        private string _username;
        private string _password;
        private bool _employeetag;

        public int StoreId {get; set;}
        public int Id {get; set;}

        public string FirstName
        {
            get => _firstname;
            set
            {
                if(value.Length == 0)
                {
                    throw new ArgumentException("There is no input first name!", nameof(value));
                }
                value = CaseConverter.Convert(value);
                if(!BadWordChecker.CheckWord(value))
                {
                    throw new ArgumentException("That name contains a banned term!", nameof(value));
                }
                _firstname = value;
            }
        }

        public string LastName
        {
            get => _lastname;
            set
            {
                if(value.Length == 0)
                {
                    throw new ArgumentException("There is no input last name!", nameof(value));
                }
                value = CaseConverter.Convert(value);
                if(!BadWordChecker.CheckWord(value))
                {
                    throw new ArgumentException("That name contains a banned term!", nameof(value));
                }
                _lastname = value;
            }
        }

        public string Username
        {
            get => _username;
            set
            {
                if(value.Length == 0)
                {
                    throw new ArgumentException("There is no input username!", nameof(value));
                }
                if(!BadWordChecker.CheckWord(value))
                {
                    throw new ArgumentException("That name contains a banned term!", nameof(value));
                }
                _username = value;
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if(value.Length == 0)
                {
                    throw new ArgumentException("There is no input password!", nameof(value));
                }
                _password = value;
            }
        }

        public bool EmployeeTag
        {
            get => _employeetag;
            set {_employeetag = value;}
        }
    }
}
