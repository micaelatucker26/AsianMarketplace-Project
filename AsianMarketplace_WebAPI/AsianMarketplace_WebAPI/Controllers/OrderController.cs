using AsianMarketplace_WebAPI.DTOs;
using AsianMarketplace_WebAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AsianMarketplace_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly AsianMarketplaceDbContext _marketplaceDbContext;
        private readonly IMapper _mapper;

        public OrderController(AsianMarketplaceDbContext marketplaceDbContext, IMapper mapper)
        {
            _marketplaceDbContext = marketplaceDbContext;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDTO orderDTO)
        {
            // Map the DTO to the entity
            var newOrder = _mapper.Map<Order>(orderDTO);

            // Add the new cart item to the context
            _marketplaceDbContext.Orders.Add(newOrder);

            // Save changes to the database
            await _marketplaceDbContext.SaveChangesAsync();

            return 
                CreatedAtAction(nameof(GetOrder), 
                new {orderId = newOrder.OrderId}, orderDTO);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
        {
            var orders = await _marketplaceDbContext.Orders
                .Include(o => o.Shopper)
                .Include(o => o.OrderItems)
                .ToListAsync();
            var orderDTOs = _mapper.Map<List<OrderDTO>>(orders);
            return Ok(orderDTOs);
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderDTO>> GetOrder(Guid orderId)
        {
            var order = await _marketplaceDbContext.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
            if (order == null)
            {
                return NotFound();
            }

            var orderDTO = _mapper.Map<OrderDTO>(order);
            return Ok(orderDTO);
        }

        [HttpPut("{orderId}")]
        public async Task<IActionResult> UpdateOrder( Guid orderId, [FromBody] OrderDTO orderDTO)
        {
            // Fetch the existing order from the database
            var order =  await _marketplaceDbContext.Orders.FindAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }

            order.OrderId = orderId;
            order.OrderDate = orderDTO.OrderDate;
            order.Username = orderDTO.Username;

            // Save changes to the database
            await _marketplaceDbContext.SaveChangesAsync(); 
           
            // return a response
            return NoContent();
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(Guid orderId)
        {
            // Fetch the existing order from the database
            var order = await _marketplaceDbContext.Orders.FindAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }
            _marketplaceDbContext.Orders.Remove(order);

            await _marketplaceDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
