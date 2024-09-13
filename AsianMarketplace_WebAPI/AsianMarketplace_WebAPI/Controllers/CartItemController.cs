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
        private readonly IShopperRepo _shopperRepo;

        public CartItemController( IMapper mapper, ICartItemRepo cartItemRepo, IShopperRepo shopperRepo)
        {
            _mapper = mapper;
            _cartItemRepo = cartItemRepo;
            _shopperRepo = shopperRepo;
        }

        [HttpPost("{itemId:guid}/{userId:guid}")]
        public async Task<IActionResult> CreateCartItem(Guid itemId, Guid userId, [FromBody] CartItemDTO cartItemDTO)
        {
            var response = new CartItemDTO();
            try
            { 
                // Check if the cart item already exists for this user
                var existingCartItem = await _cartItemRepo.GetCartItemByItemAndUser(itemId, userId);
                if (existingCartItem != null)
                {
                    // Update the quantity of the existing cart item
                    existingCartItem.Quantity += cartItemDTO.Quantity;
                    await _cartItemRepo.UpdateCartItem(itemId, userId,existingCartItem);
                    response = _mapper.Map<CartItemDTO>(existingCartItem);
                    return Ok(response);
                }
                else
                {
                    // Create a new item that will be added to that user's cart
                    var cartItem = new CartItem
                    {
                        ItemId = itemId,
                        UserId = userId,
                        Quantity = cartItemDTO.Quantity
                    };

                    await _cartItemRepo.CreateCartItem(cartItem);
                    response = _mapper.Map<CartItemDTO>(cartItem);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred while adding the item to the cart.", Details = ex.Message});
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

        [HttpGet("{cartItemId:guid}")]
        public async Task<ActionResult<CartItemDTO>> GetCartItem(Guid cartItemId)
        {
            // Fetch the existing cart item from the database
            var cartItem = await _cartItemRepo.GetCartItem(cartItemId);

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

        [HttpPut("{itemId:guid}/{userId:guid}/{adjustment:int}")]
        public async Task<IActionResult> UpdateCartItem(Guid itemId, Guid userId, int adjustment)
        {
            try
            {
                var existingCartItem = await _cartItemRepo.GetCartItemByItemAndUser(itemId, userId);
                if (existingCartItem == null)
                {
                    return NotFound(new { Message = "Cart item not found." });
                }

                // Adjust the quantity based on the user action
                existingCartItem.Quantity += adjustment;

                // Remove the item if the item quanitity is now 0
                if(existingCartItem.Quantity <= 0)
                {
                    await _cartItemRepo.RemoveCartItemFromCart(existingCartItem);
                }
                else
                {
                    await _cartItemRepo.UpdateCartItem(itemId, userId, existingCartItem);
                }

                return NoContent();
            }
            catch(Exception ex)
            {
                // Handle other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpDelete("{cartItemId:guid}")]
        public async Task<IActionResult> DeleteCartItem(Guid cartItemId)
        {
            try
            {
                // Fetch the existing cart item from the database
                var cartItem = await _cartItemRepo.DeleteRecord(cartItemId);
                if (cartItem == null)
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
