using AsianMarketplace_WebAPI.DTOs;
using AsianMarketplace_WebAPI.DTOs.Responses;
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

        [HttpPost("{itemId}/{username}")]
        public async Task<IActionResult> CreateCartItem(Guid itemId, string username, [FromBody] CartItemDTO cartItemDTO)
        {
            try
            {
                // Map the DTO to the entity
                var newCartItem = _mapper.Map<CartItem>(cartItemDTO);

                if (cartItemDTO.Quantity > 0)
                {
                    var user = await _shopperRepo.GetShopper(username);
                    if(user == null)
                    {
                        return NotFound("User not found");
                    }

                    var cartItem = new CartItem
                    {
                        ItemId = itemId,
                        UserId = user.UserId,
                        Quantity = cartItemDTO.Quantity
                    };

                    // Add the new cart item to the context
                    await _cartItemRepo.CreateCartItem(cartItem);

                    var responseDTO = new CartItemResponseDTO
                    {
                        ItemId = cartItem.ItemId,
                        UserId = cartItem.UserId,
                        Quantity = cartItem.Quantity
                    };
                    var resultDTO = _mapper.Map<CartItemDTO>(newCartItem);

                    // Return that cart item's details (including quantity, itemId, and userId)
                    return
                        CreatedAtAction(nameof(GetCartItem),
                        new { cartItemId = newCartItem.CartItemId }, responseDTO);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Message = "The cart item quantity must be greater than zero." });
                }
           
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
        public async Task<ActionResult<IEnumerable<CartItemResponseDTO>>> GetCartItems()
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
                var cartItemDTOs = _mapper.Map<List<CartItemResponseDTO>>(cartItems);
                return Ok(cartItemDTOs);
            }
            catch(Exception ex)
            {
                // Handle other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpGet("{cartItemId}")]
        public async Task<ActionResult<CartItemResponseDTO>> GetCartItem(Guid cartItemId)
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
                var cartItemDTO = _mapper.Map<CartItemResponseDTO>(cartItem);
                return Ok(cartItemDTO);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpPut("{cartItemId}")]
        public async Task<IActionResult> UpdateCartItem( Guid cartItemId, [FromBody] CartItemDTO cartItemDTO)
        {
            try
            {
                if (cartItemDTO.Quantity > 0)
                {
                    // Fetch the existing cart item from the database
                    var cartItem = await _cartItemRepo.UpdateCartItem(cartItemId, cartItemDTO);
                    if (cartItem == null)
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Message = "The cart item quantity must be greater than zero." });
                }
                // Return a response
                return NoContent();
            }
            catch(Exception ex)
            {
                // Handle other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpDelete("{cartItemId}")]
        public async Task<IActionResult> DeleteCartItem(Guid cartItemId)
        {
            try
            {
                // Fetch the existing cart item from the database
                var cartItem = await _cartItemRepo.DeleteCartItem(cartItemId);
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
