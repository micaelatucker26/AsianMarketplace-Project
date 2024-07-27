using AsianMarketplace_WebAPI.DTOs;
using AsianMarketplace_WebAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AsianMarketplace_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartItemController : Controller
    {
        private readonly AsianMarketplaceDbContext _marketplaceDbContext;
        private readonly IMapper _mapper;

        public CartItemController(AsianMarketplaceDbContext marketplaceDbContext, IMapper mapper)
        {
            _marketplaceDbContext = marketplaceDbContext;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCartItem([FromBody] CartItemDTO cartItemDTO)
        {
            // Map the DTO to the entity
            var newCartItem = _mapper.Map<CartItem>(cartItemDTO);

            // Add the new cart item to the context
            _marketplaceDbContext.CartItems.Add(newCartItem);

            // Save changes to the database
            await _marketplaceDbContext.SaveChangesAsync();

            return 
                CreatedAtAction(nameof(GetCartItem), 
                new {itemId = newCartItem.ItemId, userId = newCartItem.UserId}, cartItemDTO);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItemDTO>>> GetCartItems()
        {
            var cartItems = await _marketplaceDbContext.CartItems
                .ToListAsync();
            var cartItemDTOs = _mapper.Map<List<CartItemDTO>>(cartItems);
            return Ok(cartItemDTOs);
        }

        [HttpGet("{itemId}/{userId}")]
        public async Task<ActionResult<CartItemDTO>> GetCartItem(Guid itemId, string userId)
        {
            var cartItem = await _marketplaceDbContext.CartItems.FindAsync(itemId, userId);

            if (cartItem == null)
            {
                return NotFound();
            }

            var cartItemDTO = _mapper.Map<CartItemDTO>(cartItem);
            return Ok(cartItemDTO);
        }

        [HttpPut("{itemId}/{userId}")]
        public async Task<IActionResult> UpdateCartItem( Guid itemId, string userId, [FromBody] CartItemDTO cartItemDTO)
        {
            // Fetch the existing cart item from the database
            var cartItem =  await _marketplaceDbContext.CartItems.FindAsync(itemId, userId);
            if (cartItem == null)
            {
                return NotFound();
            }

            cartItem.Quantity = cartItemDTO.Quantity;
            cartItem.ItemId = itemId;
            cartItem.UserId = userId;

            // Save changes to the database
            await _marketplaceDbContext.SaveChangesAsync(); 
           
            // return a response
            return NoContent();
        }

        [HttpDelete("{itemId}/{userId}")]
        public async Task<IActionResult> DeleteCartItem(Guid itemId, string userId)
        {
            // Fetch the existing cart item from the database
            var cartItem = await _marketplaceDbContext.CartItems.FindAsync(itemId, userId);
            if (cartItem == null)
            {
                return NotFound();
            }
            _marketplaceDbContext.CartItems.Remove(cartItem);

            await _marketplaceDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
