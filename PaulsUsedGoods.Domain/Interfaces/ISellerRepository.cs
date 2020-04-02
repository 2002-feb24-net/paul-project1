using PaulsUsedGoods.Domain.Model;
using System.Collections.Generic;

namespace PaulsUsedGoods.Domain.Interfaces
{
    interface ISellerRepository
    {
// ! CLASS SPECIFIC
        List<Seller> GetSellersByName(string sellerName = null);
        Seller GetSellerById(int sellerId);
        void AddSeller(Seller inputSeller);
        void DeleteSellerById(int sellerId);
        void UpdateSeller(Item inputSeller);
// ! ITEMS WITHIN THE INDIVIDUAL SELLER ONLY
        List<Item> GetItemsByName(string itemName = null);
        Item GetItemById(int itemId);
        void AddItem(Item inputItem);
        void DeleteItemById(int itemId);
        void UpdateItem(Item inputItem);
// ! GENERAL COMMANDS
        void Save();
    }
}
