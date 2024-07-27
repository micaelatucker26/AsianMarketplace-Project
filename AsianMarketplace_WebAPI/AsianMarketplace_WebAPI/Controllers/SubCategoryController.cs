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
            try
            {
                // Map the DTO to the entity
                var newSubCategory = _mapper.Map<SubCategory>(subCategoryDTO);

                // Add the new subcategory to the context
                _marketplaceDbContext.SubCategories.Add(newSubCategory);

                // Save changes to the database
                await _marketplaceDbContext.SaveChangesAsync();

                // Return that subcategory's details (including name and category name)
                return
                    CreatedAtAction(nameof(GetSubCategory),
                    new { name = newSubCategory.Name }, subCategoryDTO);
            }
            catch (DbUpdateException ex)
            {
                // Handle database update exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred while creating a subcategory.", Details = ex.Message });
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubCategoryDTO>>> GetSubCategories()
        {
            try
            {
                // Gather subcategories into a list
                var subCategories = await _marketplaceDbContext.SubCategories
                .Include(s => s.Items)
                .ToListAsync();
                if(subCategories == null)
                {
                    return NotFound();
                }
                // Map the list of subcategories to the DTO
                var subCategoryDTOs = _mapper.Map<List<SubCategoryDTO>>(subCategories);
                return Ok(subCategoryDTOs);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<CategoryDTO>> GetSubCategory(string name)
        {
            // Fetch the existing subcategory from the database
            var subCategory = await _marketplaceDbContext.SubCategories
                .Include(i => i.Items)
                .FirstOrDefaultAsync(sc => sc.Name == name);

            if (subCategory == null)
            {
                return NotFound();
            }
            try
            {
                // Map that subcategory to the DTO
                var subCategoryDTO = _mapper.Map<SubCategoryDTO>(subCategory);
                return Ok(subCategoryDTO);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
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

            try
            {
                // The name and category name will be updated
                subCategory.Name = name;
                subCategory.CategoryName = subCategoryDTO.CategoryName;

                // Save changes to the database
                await _marketplaceDbContext.SaveChangesAsync();

                // Return a response
                return NoContent();
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteSubCategory(string name)
        {
            // Fetch the existing subcategory from the database
            var subCategory = await _marketplaceDbContext.SubCategories.FindAsync(name);
            if (subCategory == null)
            {
                return NotFound();
            }
            try
            {
                // Remove that subcategory from the database
                _marketplaceDbContext.SubCategories.Remove(subCategory);

                // Save changes to the database
                await _marketplaceDbContext.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }
    }
}
