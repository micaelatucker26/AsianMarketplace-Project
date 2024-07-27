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
            // Map the DTO to the entity
            var newItem = _mapper.Map<Item>(itemDTO);

            // Add the new cart item to the context
            _marketplaceDbContext.Items.Add(newItem);

            // Save changes to the database
            await _marketplaceDbContext.SaveChangesAsync();

            return 
                CreatedAtAction(nameof(GetItem), 
                new {itemId = newItem.ItemId}, itemDTO);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDTO>>> GetItems()
        {
            var items = await _marketplaceDbContext.Items
                .Include(i => i.CartItems)
                .Include(i => i.OrderItems)
                .Include(i => i.ShoppingListItems)
                .ToListAsync();
            var itemDTOs = _mapper.Map<List<ItemDTO>>(items);
            return Ok(itemDTOs);
        }

        [HttpGet("{itemId}")]
        public async Task<ActionResult<ItemDTO>> GetItem(Guid itemId)
        {
            var item = await _marketplaceDbContext.Items
                .Include(i => i.CartItems)
                .Include(i => i.OrderItems)
                .Include(i => i.ShoppingListItems)
                .FirstOrDefaultAsync(i => i.ItemId == itemId);
            if (item == null)
            {
                return NotFound();
            }

            var itemDTO = _mapper.Map<ItemDTO>(item);
            return Ok(itemDTO);
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

            item.ItemId = itemId;
            item.Name = itemDTO.Name;
            item.Description = itemDTO.Description;
            item.Quantity = itemDTO.Quantity;
            item.Price = itemDTO.Price;
            item.ImageUrl = itemDTO.ImageUrl;
            item.SubCategoryName = itemDTO.SubCategoryName;

            // Save changes to the database
            await _marketplaceDbContext.SaveChangesAsync(); 
           
            // return a response
            return NoContent();
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
            _marketplaceDbContext.Items.Remove(item);

            await _marketplaceDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
