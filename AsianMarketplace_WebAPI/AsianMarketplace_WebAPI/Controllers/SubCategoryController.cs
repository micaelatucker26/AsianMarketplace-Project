using AsianMarketplace_WebAPI.DTOs;
using AsianMarketplace_WebAPI.Interfaces;
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
        private readonly IMapper _mapper;
        private readonly ISubCategoryRepo _subCategoryRepo;

        public SubCategoryController(IMapper mapper, ISubCategoryRepo repo)
        {
            _mapper = mapper;
            _subCategoryRepo = repo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubCategory([FromBody] SubCategoryDTO subCategoryDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Map the DTO to the entity
                var newSubCategory = _mapper.Map<SubCategory>(subCategoryDTO);

                // Add the new subcategory to the context
                await _subCategoryRepo.CreateSubCategory(newSubCategory);

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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Gather subcategories into a list
                var subCategories = await _subCategoryRepo.GetSubCategories();
                
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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Fetch the existing subcategory from the database
            var subCategory = await _subCategoryRepo.GetSubCategory(name);

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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Fetch the existing category from the database
                var subCategory = await _subCategoryRepo.UpdateSubCategory(name, subCategoryDTO);
                if (subCategory == null)
                {
                    return NotFound();
                }
                
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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Fetch the existing subcategory from the database
                var subCategory = await _subCategoryRepo.DeleteSubCategory(name);

                if (subCategory == null)
                {
                    return NotFound();
                }
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
