﻿using AsianMarketplace_WebAPI.DTOs;
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

        [HttpPost("{userId}")]
        public async Task<IActionResult> CreateOrder(Guid userId, [FromBody] OrderDTO orderDTO)
        {
            try
            {
                // Map the DTO to the entity
                var orderDetails = _mapper.Map<Order>(orderDTO);

                var order = new Order
                {
                    UserId = userId,
                    OrderDate = orderDetails.OrderDate
                };

                // Add the new order to the context
                await _orderRepo.CreateOrder(order);

                // Return that order's details (including orderId, orderdate, and username)
                return
                    CreatedAtAction(nameof(GetOrder),
                    new { orderId = order.OrderId }, orderDTO);
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
        public async Task<ActionResult<IEnumerable<OrderResponseDTO>>> GetOrders()
        {
            try
            {
                // Gather orders into a list
                var orders = await _orderRepo.GetOrders();
                if(orders == null)
                {
                    return NotFound();
                }
                // Map the list of orders to the DTO
                var orderDTOs = _mapper.Map<List<OrderResponseDTO>>(orders);
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

        [HttpPut("{orderId}")]
        public async Task<IActionResult> UpdateOrder( Guid orderId, [FromBody] OrderDTO orderDTO)
        {
            try
            {
                // Fetch the existing order from the database
                var order =  await _orderRepo.UpdateOrder(orderId, orderDTO);
                if (order == null)
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

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(Guid orderId)
        {
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
