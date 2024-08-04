using AsianMarketplace_WebAPI.DTOs;
using AsianMarketplace_WebAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AsianMarketplace_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : Controller
    {
        private readonly AsianMarketplaceDbContext _marketplaceDbContext;
        private readonly IMapper _mapper;

        public ItemController(AsianMarketplaceDbContext marketplaceDbContext, IMapper mapper)
        {
            _marketplaceDbContext = marketplaceDbContext;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] ItemDTO itemDTO)
        {
            try
            {
                // Map the DTO to the entity
                var newItem = _mapper.Map<Item>(itemDTO);

                // Add the new item to the context
                _marketplaceDbContext.Items.Add(newItem);

                // Save changes to the database
                await _marketplaceDbContext.SaveChangesAsync();

                // Return that item's details (including itemId, name, description, quantity, price, imageURL, and subcategory name)
                return
                    CreatedAtAction(nameof(GetItem),
                    new { itemId = newItem.ItemId }, itemDTO);
            }
            catch (DbUpdateException ex)
            {
                // Handle database update exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred while creating an item.", Details = ex.Message });
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDTO>>> GetItems()
        {
            try
            {
                // Gather items into a list
                var items = await _marketplaceDbContext.Items
                .OrderBy(i => i.Name)
                .Include(i => i.CartItems)
                .Include(i => i.OrderItems)
                .Include(i => i.ShoppingListItems)
                .ToListAsync();
                if(items == null)
                {
                    return NotFound();
                }
                // Map the list of items to the DTO
                var itemDTOs = _mapper.Map<List<ItemDTO>>(items);
                return Ok(itemDTOs);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpGet("{itemId}")]
        public async Task<ActionResult<ItemDTO>> GetItem(Guid itemId)
        {
            // Fetch the existing item from the database
            var item = await _marketplaceDbContext.Items
                .Include(i => i.CartItems)
                .Include(i => i.OrderItems)
                .Include(i => i.ShoppingListItems)
                .FirstOrDefaultAsync(i => i.ItemId == itemId);
            if (item == null)
            {
                return NotFound();
            }

            try
            {
                // Map that item to the DTO
                var itemDTO = _mapper.Map<ItemDTO>(item);
                return Ok(itemDTO);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpPut("{itemId}")]
        public async Task<IActionResult> UpdateItem( Guid itemId, [FromBody] ItemDTO itemDTO)
        {
            // Fetch the existing item from the database
            var item =  await _marketplaceDbContext.Items.FindAsync(itemId);
            if (item == null)
            {
                return NotFound();
            }

            try
            {
                itemDTO.ItemId = itemId;

                // All of these fields below will be updated
                item.Name = itemDTO.Name;
                item.Description = itemDTO.Description;
                item.Quantity = itemDTO.Quantity;
                item.Price = itemDTO.Price;
                item.ImageUrl = itemDTO.ImageUrl;
                item.SubCategoryName = itemDTO.SubCategoryName;

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

        [HttpDelete("{itemId}")]
        public async Task<IActionResult> DeleteItem(Guid itemId)
        {
            // Fetch the existing item from the database
            var item = await _marketplaceDbContext.Items.FindAsync(itemId);
            if (item == null)
            {
                return NotFound();
            }
            try
            {
                // Remove that item from the database
                _marketplaceDbContext.Items.Remove(item);

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
