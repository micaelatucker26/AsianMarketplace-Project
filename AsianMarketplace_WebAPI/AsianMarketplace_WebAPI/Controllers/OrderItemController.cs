using AsianMarketplace_WebAPI.DTOs;
using AsianMarketplace_WebAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AsianMarketplace_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemController : Controller
    {
        private readonly AsianMarketplaceDbContext _marketplaceDbContext;
        private readonly IMapper _mapper;

        public OrderItemController(AsianMarketplaceDbContext marketplaceDbContext, IMapper mapper)
        {
            _marketplaceDbContext = marketplaceDbContext;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderItem([FromBody] OrderItemDTO orderItemDTO)
        {
            try
            {
                // Map the DTO to the entity
                var newOrderItem = _mapper.Map<OrderItem>(orderItemDTO);

                // Add the new order item to the context
                _marketplaceDbContext.OrderItems.Add(newOrderItem);

                // Save changes to the database
                await _marketplaceDbContext.SaveChangesAsync();

                // Return that order item's details (including price, quantity, itemId, and orderId)
                return
                    CreatedAtAction(nameof(GetOrderItems),
                    new { itemId = newOrderItem.ItemId, orderId = newOrderItem.OrderId }, orderItemDTO);
            }
            catch (DbUpdateException ex)
            {
                // Handle database update exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred while creating an order item.", Details = ex.Message });
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItemDTO>>> GetOrderItems()
        {
            try
            {
                // Gather order items into a list
                var orderItems = await _marketplaceDbContext.OrderItems
                .ToListAsync();
                if(orderItems == null)
                {
                    return NotFound();
                }
                // Map the list of order items to the DTO
                var orderItemDTOs = _mapper.Map<List<OrderItemDTO>>(orderItems);
                return Ok(orderItemDTOs);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpGet("{itemId}/{orderId}")]
        public async Task<ActionResult<OrderItemDTO>> GetOrderItem( Guid itemId, Guid orderId)
        {
            // Fetch the existing order item from the database
            var orderItem = await _marketplaceDbContext.OrderItems
                .FirstOrDefaultAsync(oi => oi.ItemId == itemId && oi.OrderId == orderId);
            if (orderItem == null)
            {
                return NotFound();
            }
            try
            {
                // Map that order item to the DTO
                var orderItemDTO = _mapper.Map<OrderItemDTO>(orderItem);
                return Ok(orderItemDTO);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpPut("{itemId}/{orderId}")]
        public async Task<IActionResult> UpdateOrderItem( Guid itemId, Guid orderId, [FromBody] OrderItemDTO orderItemDTO)
        {
            // Fetch the existing order item from the database
            var orderItem =  await _marketplaceDbContext.OrderItems.FindAsync(itemId, orderId);
            if (orderItem == null)
            {
                return NotFound();
            }

            try
            {
                // The price and the quantity will be updated
                orderItem.Price = orderItemDTO.Price;
                orderItem.Quantity = orderItemDTO.Quantity;
                orderItemDTO.ItemId = itemId;
                orderItemDTO.OrderId = orderId;

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

        [HttpDelete("{itemId}/{orderId}")]
        public async Task<IActionResult> DeleteOrderItem(Guid itemId, Guid orderId)
        {
            // Fetch the existing order item from the database
            var orderItem = await _marketplaceDbContext.OrderItems.FindAsync(itemId, orderId);
            if (orderItem == null)
            {
                return NotFound();
            }
            try
            {
                // Remove that order item from the database
                _marketplaceDbContext.OrderItems.Remove(orderItem);

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
