using AsianMarketplace_WebAPI.DTOs;
using AsianMarketplace_WebAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AsianMarketplace_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly AsianMarketplaceDbContext _marketplaceDbContext;
        private readonly IMapper _mapper;

        public CategoryController(AsianMarketplaceDbContext marketplaceDbContext, IMapper mapper)
        {
            _marketplaceDbContext = marketplaceDbContext;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDTO categoryDTO)
        {
            try
            {
                // Map the DTO to the entity
                var newCategory = _mapper.Map<Category>(categoryDTO);

                // Add the new category to the context
                _marketplaceDbContext.Categories.Add(newCategory);

                // Save changes to the database
                await _marketplaceDbContext.SaveChangesAsync();

                // Return that category's details (including the category name)
                return
                    CreatedAtAction(nameof(GetCategory),
                    new { name = newCategory.Name}, categoryDTO);
            }
            catch (DbUpdateException ex)
            {
                // Handle database update exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred while creating a category.", Details = ex.Message });
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            try
            {
                // Gather categories into a list
                var categories = await _marketplaceDbContext.Categories
                .Include(c => c.SubCategories)
                     .ThenInclude(sc => sc.Items)
                .ToListAsync();
                if (categories == null)
                {
                    return NotFound();
                }
                // Map the list of categories to the DTO
                var categoryDTOs = _mapper.Map<List<CategoryDTO>>(categories);
                return Ok(categoryDTOs);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(string name)
        {
            // Fetch the existing category from the database
            var category = await _marketplaceDbContext.Categories
                .Include(c => c.SubCategories)
                .ThenInclude(sc => sc.Items)
                .FirstOrDefaultAsync(c => c.Name == name); 
            if (category == null)
            {
                return NotFound();
            }
            try
            {
                // Map the category to the DTO
                var categoryDTO = _mapper.Map<CategoryDTO>(category);
                return Ok(categoryDTO);
            }
            catch(Exception ex)
            {
                // Handle other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }


        [HttpPut("{name}")]
        public async Task<IActionResult> UpdateCategory(string name, [FromBody] CategoryDTO categoryDTO)
        {
            // Fetch the existing category from the database
            var category =  await _marketplaceDbContext.Categories.FindAsync(name);
            if (category == null)
            {
                return NotFound();
            }

            try
            {
                // The category name will be updated
                category.Name = categoryDTO.Name;

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
        public async Task<IActionResult> DeleteCategory(string name)
        {
            // Fetch the existing category from the database
            var category = await _marketplaceDbContext.Categories.FindAsync(name);
            if (category == null)
            {
                return NotFound();
            }
            try
            {
                // Remove the category from the database
                _marketplaceDbContext.Categories.Remove(category);

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
