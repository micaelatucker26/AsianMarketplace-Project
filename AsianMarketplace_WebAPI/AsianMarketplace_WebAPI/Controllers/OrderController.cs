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
    public class OrderController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepo _orderRepo;
        private readonly IOrderItemRepo _orderItemRepo;

        public OrderController(IMapper mapper, IOrderRepo orderRepo, IOrderItemRepo orderItemRepo)
        {
            _mapper = mapper;
            _orderRepo = orderRepo;
            _orderItemRepo = orderItemRepo;
        }

        [HttpPost("{userId:guid}")]
        public async Task<IActionResult> CreateOrder(Guid userId, [FromBody] OrderDTO orderDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Map the DTO to the entity
                var orderDetails = _mapper.Map<Order>(orderDTO);

                orderDetails.UserId = userId;
                orderDetails.OrderDate = DateTime.UtcNow;

                // Add the new order to the context
                await _orderRepo.CreateOrder(orderDetails);

                // After saving the order, map it back to DTO to return the created order with generated OrderId
                var createdOrderDTO = _mapper.Map<OrderDTO>(orderDetails);

                // Return that order's details (including orderId, orderdate, and username)
                return
                    CreatedAtAction(nameof(GetOrder),
                    new { orderId = createdOrderDTO.OrderId }, createdOrderDTO);
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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Gather orders into a list
                var orders = await _orderRepo.GetOrders();
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

        [HttpGet("{orderId:guid}")]
        public async Task<ActionResult<OrderDTO>> GetOrder(Guid orderId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Fetch the existing order from the database
            var order = await _orderRepo.GetOrder(orderId);
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

        [HttpPut("{orderId:guid}")]
        public async Task<IActionResult> UpdateOrder( Guid orderId, [FromBody] OrderDTO orderDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Fetch the existing order from the database
                var order =  await _orderRepo.UpdateOrder(orderId, orderDTO);
                if (order == null)
                {
                    return NotFound(new {Message = $"Order with ID {orderId} not found." });
                }

                // Return a response
                return NoContent();
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpDelete("{orderId:guid}")]
        public async Task<IActionResult> DeleteOrder(Guid orderId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Fetch the existing order from the database
                var order = await _orderRepo.DeleteOrder(orderId);
                if (order == null)
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
