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
            try
            {
                // Map the DTO to the entity
                var newOrder = _mapper.Map<Order>(orderDTO);

                // Add the new order to the context
                _marketplaceDbContext.Orders.Add(newOrder);

                // Save changes to the database
                await _marketplaceDbContext.SaveChangesAsync();

                // Return that order's details (including orderId, orderdate, and username)
                return
                    CreatedAtAction(nameof(GetOrder),
                    new { orderId = newOrder.OrderId }, orderDTO);
            }
            catch (DbUpdateException ex)
            {
                // Handle database update exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred while creating an order.", Details = ex.Message });
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
        {
            try
            {
                // Gather orders into a list
                var orders = await _marketplaceDbContext.Orders
                .Include(o => o.Shopper)
                .Include(o => o.OrderItems)
                .ToListAsync();
                if(orders == null)
                {
                    return NotFound();
                }
                // Map the list of orders to the DTO
                var orderDTOs = _mapper.Map<List<OrderDTO>>(orders);
                return Ok(orderDTOs);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderDTO>> GetOrder(Guid orderId)
        {
            // Fetch the existing order from the database
            var order = await _marketplaceDbContext.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
            if (order == null)
            {
                return NotFound();
            }
            try
            {
                // Map that order to the DTO
                var orderDTO = _mapper.Map<OrderDTO>(order);
                return Ok(orderDTO);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
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

            try
            {
                orderDTO.OrderId = orderId;
                // The order date and username will be updated
                order.OrderDate = orderDTO.OrderDate;
                order.Username = orderDTO.Username;

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

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(Guid orderId)
        {
            // Fetch the existing order from the database
            var order = await _marketplaceDbContext.Orders.FindAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }
            try
            {
                // Remove that order from the database
                _marketplaceDbContext.Orders.Remove(order);

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
