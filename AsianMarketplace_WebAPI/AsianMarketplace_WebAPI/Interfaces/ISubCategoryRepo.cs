using AsianMarketplace_WebAPI.DTOs;
using AsianMarketplace_WebAPI.Models;

namespace AsianMarketplace_WebAPI.Interfaces
{
    public interface ISubCategoryRepo
    {
        Task<List<SubCategory>> GetSubCategories();
        Task<SubCategory> GetSubCategory(string name);
        Task<SubCategory> CreateSubCategory(SubCategory subCategory);
        Task<SubCategory> UpdateSubCategory(string name, SubCategoryDTO subCategoryDTO);
        Task<SubCategory> DeleteSubCategory(string name);
    }
}
