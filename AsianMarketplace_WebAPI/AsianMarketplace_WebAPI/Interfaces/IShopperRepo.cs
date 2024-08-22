using AsianMarketplace_WebAPI.DTOs;
using AsianMarketplace_WebAPI.Models;

namespace AsianMarketplace_WebAPI.Interfaces
{
    public interface IShopperRepo
    {
        Task<List<Shopper>> GetShoppers();
        Task<Shopper> GetShopper(string name);
        Task<Shopper> CreateShopper(Shopper shopper);
        //Task<Shopper> UpdateShopper(string name, ShopperDTO shopperDTO);
        Task<Shopper> DeleteShopper(string name);
    }
}
