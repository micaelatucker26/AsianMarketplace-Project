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
            try
            {
                // Map the DTO to the entity
                var newCartItem = _mapper.Map<CartItem>(cartItemDTO);

                // Add the new cart item to the context
                _marketplaceDbContext.CartItems.Add(newCartItem);

                // Save changes to the database
                await _marketplaceDbContext.SaveChangesAsync();

                // Return that cart item's details (including quantity, itemId, and userId)
                return
                    CreatedAtAction(nameof(GetCartItem),
                    new { itemId = newCartItem.ItemId, userId = newCartItem.UserId }, cartItemDTO);
            }
            catch(DbUpdateException ex)
            {
                // Handle database update exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred while creating a cart item.", Details = ex.Message });
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItemDTO>>> GetCartItems()
        {
            try
            {
                // Gather cart items into a list
                var cartItems = await _marketplaceDbContext.CartItems.ToListAsync();
                if(cartItems == null)
                {
                    return NotFound();
                }
                // Map the list of cart items to the DTO
                var cartItemDTOs = _mapper.Map<List<CartItemDTO>>(cartItems);
                return Ok(cartItemDTOs);
            }
            catch(Exception ex)
            {
                // Handle other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpGet("{itemId}/{userId}")]
        public async Task<ActionResult<CartItemDTO>> GetCartItem(Guid itemId, string userId)
        {
            // Fetch the existing cart item from the database
            var cartItem = await _marketplaceDbContext.CartItems.FindAsync(itemId, userId);

            if (cartItem == null)
            {
                return NotFound();
            }
            try
            {
                // Map that cart item to the DTO
                var cartItemDTO = _mapper.Map<CartItemDTO>(cartItem);
                return Ok(cartItemDTO);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
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

            try
            {
                // The quantity will be updated
                cartItem.Quantity = cartItemDTO.Quantity;
                cartItemDTO.ItemId = itemId;
                cartItemDTO.UserId = userId;

                // Save changes to the database
                await _marketplaceDbContext.SaveChangesAsync();

                // Return a response
                return NoContent();
            }
            catch(Exception ex)
            {
                // Handle other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
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
            try
            {
                // Remove that cart item from the database
                _marketplaceDbContext.CartItems.Remove(cartItem);

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
