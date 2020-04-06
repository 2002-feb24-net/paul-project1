using PaulsUsedGoods.Domain.Model;
using System.Collections.Generic;

namespace PaulsUsedGoods.Domain.Interfaces
{
    public interface IPersonRepository
    {
// ! CLASS SPECIFIC
        List<Person> GetPeopleByName(string personName = null);
        Person GetPersonById(int personId);
        void AddPerson(Person inputPerson);
        void DeletePersonById(int personId);
        void DeletePeopleByStoreId(int storeId);
        void UpdatePerson(Person inputPerson);
// ! GENERAL COMMANDS
        void Save();
    }
}
