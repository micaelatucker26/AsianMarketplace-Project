using AsianMarketplace_WebAPI.DTOs;
using AsianMarketplace_WebAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AsianMarketplace_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingListController : Controller
    {
        private readonly AsianMarketplaceDbContext _marketplaceDbContext;
        private readonly IMapper _mapper;

        public ShoppingListController(AsianMarketplaceDbContext marketplaceDbContext, IMapper mapper)
        {
            _marketplaceDbContext = marketplaceDbContext;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateShoppingList([FromBody] ShoppingListDTO shoppingListDTO)
        {
            try
            {
                // Map the DTO to the entity
                var newShoppingList = _mapper.Map<ShoppingList>(shoppingListDTO);

                // Add the new shopping list to the context
                _marketplaceDbContext.ShoppingLists.Add(newShoppingList);

                // Save changes to the database
                await _marketplaceDbContext.SaveChangesAsync();

                // Return that shopping list's details (including title, isactive, userId, and date created)
                return
                    CreatedAtAction(nameof(GetShoppingList),
                    new { title = newShoppingList.Title, userId = newShoppingList.UserId }, shoppingListDTO);
            }
            catch (DbUpdateException ex)
            {
                // Handle database update exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred while creating a shopping list.", Details = ex.Message });
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingListDTO>>> GetShoppingLists()
        {
            try
            {
                // Gather shopping lists into a list
                var lists = await _marketplaceDbContext.ShoppingLists.ToListAsync();
                if(lists == null)
                {
                    return NotFound();
                }
                // Map the list of shopping lists to the DTO
                var shoppingListDTOs = _mapper.Map<List<ShoppingListDTO>>(lists);
                return Ok(shoppingListDTOs);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }


        [HttpGet("{title}/{userId}")]
        public async Task<ActionResult<ShoppingListDTO>> GetShoppingList(string title, string userId)
        {
            // Fetch the existing shopping list from the database
            var shoppingList = await _marketplaceDbContext.ShoppingLists
                .FirstOrDefaultAsync(sl => sl.Title == title && sl.UserId == userId);
            if (shoppingList == null)
            {
                return NotFound();
            }
            try
            {
                // Mapt that shopping list to the DTO
                var shoppingListDTO = _mapper.Map<ShoppingListDTO>(shoppingList);
                return Ok(shoppingListDTO);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpPut("{title}/{userId}")]
        public async Task<IActionResult> UpdateShoppingList(string title, string userId, [FromBody] ShoppingListDTO shoppingListDTO)
        {
            // Fetch the existing user from the database
            var shoppingList =  await _marketplaceDbContext.ShoppingLists.FindAsync(title, userId);
            if (shoppingList == null)
            {
                return NotFound();
            }

            // Create a new shopping list with the updated title
            var updatedShoppingList = new ShoppingList
            {
                Title = shoppingListDTO.Title, // New title
                UserId = userId,
                IsActive = shoppingListDTO.IsActive, // Default value of 'N'
                DateCreated = shoppingList.DateCreated // Preserve the original creation date
            };

            try
            {
                // Remove the old shopping list
                _marketplaceDbContext.ShoppingLists.Remove(shoppingList);

                // Add the new shopping list
                _marketplaceDbContext.ShoppingLists.Add(updatedShoppingList);

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

        [HttpDelete("{title}/{userId}")]
        public async Task<IActionResult> DeleteShoppingList(string title, string userId)
        {
            // Fetch the existing user from the database
            var shoppingList = await _marketplaceDbContext.ShoppingLists.FindAsync(title, userId);
            if (shoppingList == null)
            {
                return NotFound();
            }
            try
            {
                // Remove that shopping list from the database
                _marketplaceDbContext.ShoppingLists.Remove(shoppingList);

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
