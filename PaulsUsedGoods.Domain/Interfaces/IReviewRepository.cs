using PaulsUsedGoods.Domain.Model;
using System.Collections.Generic;

namespace PaulsUsedGoods.Domain.Interfaces
{
    interface IReviewRepository
    {
// ! CLASS SPECIFIC
        List<Review> GetPeopleByName(string personName = null);
        Person GetPersonById(int personId);
        void AddPerson(Person inputPerson);
        void DeletePersonById(int personId);
        void UpdatePerson(Item inputPerson);
// ! RELATED TO ORDERS
        List<Order> GetOrderByName(string personName = null);
        Order GetOrderById(int orderId);
// ! GENERAL COMMANDS
        void Save();
    }
}
