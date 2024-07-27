using AsianMarketplace_WebAPI.DTOs;
using AsianMarketplace_WebAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AsianMarketplace_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubCategoryController : Controller
    {
        private readonly AsianMarketplaceDbContext _marketplaceDbContext;
        private readonly IMapper _mapper;

        public SubCategoryController(AsianMarketplaceDbContext marketplaceDbContext, IMapper mapper)
        {
            _marketplaceDbContext = marketplaceDbContext;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubCategory([FromBody] SubCategoryDTO subCategoryDTO)
        {
            // Map the DTO to the entity
            var newSubCategory = _mapper.Map<SubCategory>(subCategoryDTO);

            // Add the new category to the context
            _marketplaceDbContext.SubCategories.Add(newSubCategory);

            // Save changes to the database
            await _marketplaceDbContext.SaveChangesAsync();

            return
                CreatedAtAction(nameof(GetSubCategory),
                new { name = newSubCategory.Name }, subCategoryDTO);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubCategoryDTO>>> GetSubCategories()
        {
            var subCategories = await _marketplaceDbContext.SubCategories
                .Include(s => s.Items)
                .ToListAsync();
            var subCategoryDTOs = _mapper.Map<List<SubCategoryDTO>>(subCategories);
            return Ok(subCategoryDTOs);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<CategoryDTO>> GetSubCategory(string name)
        {
            var subCategory = await _marketplaceDbContext.SubCategories
                .Include(i => i.Items)
                .FirstOrDefaultAsync(sc => sc.Name == name);

            if (subCategory == null)
            {
                return NotFound();
            }

            var subCategoryDTO = _mapper.Map<SubCategoryDTO>(subCategory);
            return Ok(subCategoryDTO);
        }


        [HttpPut("{name}")]
        public async Task<IActionResult> UpdateSubCategory(string name, [FromBody] SubCategoryDTO subCategoryDTO)
        {
            // Fetch the existing category from the database
            var subCategory = await _marketplaceDbContext.SubCategories.FindAsync(name);
            if (subCategory == null)
            {
                return NotFound();
            }

            subCategory.Name = name;
            subCategory.CategoryName = subCategoryDTO.CategoryName;

            // Save changes to the database
            await _marketplaceDbContext.SaveChangesAsync();

            // return a response
            return NoContent();
        }

        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteSubCategory(string name)
        {
            // Fetch the existing category from the database
            var subCategory = await _marketplaceDbContext.SubCategories.FindAsync(name);
            if (subCategory == null)
            {
                return NotFound();
            }
            _marketplaceDbContext.SubCategories.Remove(subCategory);

            await _marketplaceDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
