using AsianMarketplace_WebAPI.DTOs;
using AsianMarketplace_WebAPI.Interfaces;
using AsianMarketplace_WebAPI.Models;

namespace AsianMarketplace_WebAPI.Repository
{
    public class SubCategoryRepository : ISubCategoryRepo
    {
        public Task<SubCategory> CreateSubCategory(SubCategory subCategory)
        {
            throw new NotImplementedException();
        }

        public Task<SubCategory> DeleteSubCategory(string name)
        {
            throw new NotImplementedException();
        }

        public Task<List<SubCategory>> GetSubCategories()
        {
            throw new NotImplementedException();
        }

        public Task<SubCategory> GetSubCategory(string name)
        {
            throw new NotImplementedException();
        }

        public Task<SubCategory> UpdateSubCategory(string name, SubCategoryDTO subCategoryDTO)
        {
            throw new NotImplementedException();
        }
    }
}
