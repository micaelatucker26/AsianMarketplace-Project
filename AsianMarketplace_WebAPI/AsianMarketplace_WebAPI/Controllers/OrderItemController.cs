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
            // Map the DTO to the entity
            var newOrderItem = _mapper.Map<OrderItem>(orderItemDTO);

            // Add the new order item to the context
            _marketplaceDbContext.OrderItems.Add(newOrderItem);

            // Save changes to the database
            await _marketplaceDbContext.SaveChangesAsync();

            return 
                CreatedAtAction(nameof(GetOrderItems), 
                new {itemId = newOrderItem.ItemId, orderId = newOrderItem.OrderId}, orderItemDTO);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItemDTO>>> GetOrderItems()
        {
            var orderItems = await _marketplaceDbContext.OrderItems
                .ToListAsync();
            var orderItemDTOs = _mapper.Map<List<OrderItemDTO>>(orderItems);
            return Ok(orderItemDTOs);
        }

        [HttpGet("{itemId}/{orderId}")]
        public async Task<ActionResult<OrderItemDTO>> GetOrderItem( Guid itemId, Guid orderId)
        {
            var orderItem = await _marketplaceDbContext.OrderItems
                .FirstOrDefaultAsync(oi => oi.ItemId == itemId && oi.OrderId == orderId);
            if (orderItem == null)
            {
                return NotFound();
            }

            var orderItemDTO = _mapper.Map<OrderItemDTO>(orderItem);
            return Ok(orderItemDTO);
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

            orderItem.Price = orderItemDTO.Price;
            orderItem.Quantity = orderItemDTO.Quantity;
            orderItem.ItemId = itemId;
            orderItem.OrderId = orderId;

            // Save changes to the database
            await _marketplaceDbContext.SaveChangesAsync(); 
           
            // return a response
            return NoContent();
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
            _marketplaceDbContext.OrderItems.Remove(orderItem);

            await _marketplaceDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
