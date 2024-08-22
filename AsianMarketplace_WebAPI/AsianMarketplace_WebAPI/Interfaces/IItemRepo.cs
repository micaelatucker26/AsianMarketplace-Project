using AsianMarketplace_WebAPI.DTOs;
using AsianMarketplace_WebAPI.Models;

namespace AsianMarketplace_WebAPI.Interfaces
{
    public interface IItemRepo
    {
        Task<List<Item>> GetItems();
        Task<Item> GetItem(Guid itemId);
        Task<Item> CreateItem(Item item);
        Task<Item> UpdateItem(Guid itemId, ItemDTO itemDTO);
        Task<Item> DeleteItem(Guid itemId);
    }
}
