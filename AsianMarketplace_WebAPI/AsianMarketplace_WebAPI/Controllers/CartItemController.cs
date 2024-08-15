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
    public class CartItemController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICartItemRepo _cartItemRepo;

        public CartItemController( IMapper mapper, ICartItemRepo cartItemRepo)
        {
            _mapper = mapper;
            _cartItemRepo = cartItemRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCartItem([FromBody] CartItemDTO cartItemDTO)
        {
            try
            {
                // Map the DTO to the entity
                var newCartItem = _mapper.Map<CartItem>(cartItemDTO);

                // Add the new cart item to the context
                await _cartItemRepo.CreateCartItem(newCartItem);

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
                var cartItems = await _cartItemRepo.GetCartItems();
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
            var cartItem = await _cartItemRepo.GetCartItem(itemId, userId);

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
            var cartItem =  await _cartItemRepo.UpdateCartItem(itemId, userId, cartItemDTO);
            if (cartItem == null)
            {
                return NotFound();
            }

            try
            {
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
            var cartItem = await _cartItemRepo.DeleteCartItem(itemId, userId);
            if (cartItem == null)
            {
                return NotFound();
            }
            try
            {
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
