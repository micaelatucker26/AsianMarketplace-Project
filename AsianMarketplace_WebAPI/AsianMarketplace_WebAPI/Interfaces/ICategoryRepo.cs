using AsianMarketplace_WebAPI.DTOs;
using AsianMarketplace_WebAPI.Models;

namespace AsianMarketplace_WebAPI.Interfaces
{
    public interface ICategoryRepo
    {
        Task<List<Category>> GetCategories();
        Task<Category> GetCategory(string name);
        Task<Category> CreateCategory(Category category);
        Task<Category> UpdateCategory(string name, CategoryDTO categoryDTO);
        Task<Category> DeleteCategory(string name);
    }
}
