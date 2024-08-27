using AsianMarketplace_WebAPI.DTOs;
using AsianMarketplace_WebAPI.Models;

namespace AsianMarketplace_WebAPI.Interfaces
{
    public interface IShoppingListRepo
    {
        Task<List<ShoppingList>> GetShoppingLists();
        Task<ShoppingList> GetShoppingList(string title, Guid userId);
        Task<ShoppingList> CreateShoppingList(ShoppingList shoppingList);
        Task<ShoppingList> UpdateShoppingList(string title, Guid userId, ShoppingListDTO shoppingListDTO);
        Task<ShoppingList> DeleteShoppingList(string title, Guid userId);
    }
}
