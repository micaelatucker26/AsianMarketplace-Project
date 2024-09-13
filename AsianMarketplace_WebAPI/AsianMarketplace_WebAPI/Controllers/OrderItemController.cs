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
    public class OrderItemController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IOrderItemRepo _orderItemRepo;

        public OrderItemController(IMapper mapper, IOrderItemRepo orderItemRepo)
        {
            _mapper = mapper;
            _orderItemRepo = orderItemRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderItem([FromBody] OrderItemDTO orderItemDTO)
        {
            try
            {
                // Map the DTO to the entity
                var newOrderItem = _mapper.Map<OrderItem>(orderItemDTO);

                //Validate the orderItemDTO.....
                if(orderItemDTO.Price < 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Order item Price must be greater than or equal to zero." });
                }
                else if(orderItemDTO.Quantity < 1)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Order item Quantity must be greater than or equal to one." });
                }
                // Add the new order item to the context
                await _orderItemRepo.CreateOrderItem(newOrderItem);

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
                var orderItems = await _orderItemRepo.GetOrderItems();
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

        [HttpGet("{itemId:guid}/{orderId:guid}")]
        public async Task<ActionResult<OrderItemDTO>> GetOrderItem( Guid itemId, Guid orderId)
        {
            // Fetch the existing order item from the database
            var orderItem = await _orderItemRepo.GetOrderItem(itemId, orderId);
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

        [HttpPut("{itemId:guid}/{orderId:guid}")]
        public async Task<IActionResult> UpdateOrderItem( Guid itemId, Guid orderId, [FromBody] OrderItemDTO orderItemDTO)
        {
            try
            {
                //Validate the orderItemDTO.....
                if (orderItemDTO.Price < 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Order item Price must be greater than or equal to zero." });
                }
                else if (orderItemDTO.Quantity < 1)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Order item Quantity must be greater than or equal to one." });
                }
                // Fetch the existing order item from the database
                var orderItem = await _orderItemRepo.UpdateOrderItem(itemId, orderId, orderItemDTO);
                if (orderItem == null)
                {
                    return NotFound();
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

        [HttpDelete("{itemId:guid}/{orderId:guid}")]
        public async Task<IActionResult> DeleteOrderItem(Guid itemId, Guid orderId)
        {
            try
            {
                // Try to delete the order item from the database
                var orderItem = await _orderItemRepo.DeleteOrderItem(itemId, orderId);
                if (orderItem == null)
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
