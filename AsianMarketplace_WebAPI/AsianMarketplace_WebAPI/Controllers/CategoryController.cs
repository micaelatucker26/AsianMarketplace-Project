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
            // Map the DTO to the entity
            var newCategory = _mapper.Map<Category>(categoryDTO);

            // Add the new category to the context
            _marketplaceDbContext.Categories.Add(newCategory);

            // Save changes to the database
            await _marketplaceDbContext.SaveChangesAsync();

            return
                CreatedAtAction(nameof(GetCategory),
                new { name = newCategory.Name}, categoryDTO);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            var categories = await _marketplaceDbContext.Categories
                .Include(c => c.SubCategories)
                     .ThenInclude(sc => sc.Items)
                .ToListAsync();
            var categoryDTOs = _mapper.Map<List<CategoryDTO>>(categories);
            return Ok(categoryDTOs);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(string name)
        {
            var category = await _marketplaceDbContext.Categories
                .Include(c => c.SubCategories)
                .ThenInclude(sc => sc.Items)
                .FirstOrDefaultAsync(c => c.Name == name); 
            if (category == null)
            {
                return NotFound();
            }

            var categoryDTO = _mapper.Map<CategoryDTO>(category);
            return Ok(categoryDTO);
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

            category.Name = categoryDTO.Name;

            // Save changes to the database
            await _marketplaceDbContext.SaveChangesAsync(); 
           
            // return a response
            return NoContent();
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
            _marketplaceDbContext.Categories.Remove(category);

            await _marketplaceDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
