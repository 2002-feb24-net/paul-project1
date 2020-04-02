using PaulsUsedGoods.Domain.Model;
using System.Collections.Generic;
using System.Text;

namespace PaulsUsedGoods.DataAccess.Repositories
{
    class ItemRepository
    {
// ! CLASS SPECIFIC
        List<Item> GetItemsByName(string itemName = null)
        {
            return new List<Item>{};
        }
        Item GetItemById(int itemId)
        {
            return new Item();
        }
        void AddItem(Item inputItem)
        {

        }
        void DeleteItemById(int itemId)
        {

        }
        void UpdateItem(Item inputItem)
        {

        }
// ! GENERAL COMMANDS
        void Save()
        {

        }
    }
}
