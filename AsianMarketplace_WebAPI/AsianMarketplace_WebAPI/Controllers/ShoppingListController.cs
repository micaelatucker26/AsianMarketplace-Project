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
            // Map the DTO to the entity
            var newShoppingList = _mapper.Map<ShoppingList>(shoppingListDTO);

            // Add the new shopping list to the context
            _marketplaceDbContext.ShoppingLists.Add(newShoppingList);

            // Save changes to the database
            await _marketplaceDbContext.SaveChangesAsync();

            return 
                CreatedAtAction(nameof(GetShoppingList), 
                new {title = newShoppingList.Title, userId = newShoppingList.UserId}, shoppingListDTO);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingListDTO>>> GetShoppingLists()
        {
            var lists = await _marketplaceDbContext.ShoppingLists.ToListAsync();
            var shoppingListDTOs = _mapper.Map<List<ShoppingListDTO>>(lists);
            return Ok(shoppingListDTOs);
        }


        [HttpGet("{title}/{userId}")]
        public async Task<ActionResult<ShoppingListDTO>> GetShoppingList(string title, string userId)
        {
            var shoppingList = await _marketplaceDbContext.ShoppingLists
                .FirstOrDefaultAsync(sl => sl.Title == title && sl.UserId == userId);
            if (shoppingList == null)
            {
                return NotFound();
            }

            var shoppingListDTO = _mapper.Map<ShoppingListDTO>(shoppingList);
            return Ok(shoppingListDTO);
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
                IsActive = shoppingListDTO.IsActive,
                DateCreated = shoppingList.DateCreated // Preserve the original creation date
            };

            // Remove the old shopping list
            _marketplaceDbContext.ShoppingLists.Remove(shoppingList);

            // Add the new shopping list
            _marketplaceDbContext.ShoppingLists.Add(updatedShoppingList);

            // Save changes to the database
            await _marketplaceDbContext.SaveChangesAsync();

            // return a response
            return NoContent();
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
            _marketplaceDbContext.ShoppingLists.Remove(shoppingList);

            await _marketplaceDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
