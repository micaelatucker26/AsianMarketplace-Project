using AsianMarketplace_WebAPI.Controllers;
using AsianMarketplace_WebAPI.DTOs;
using AsianMarketplace_WebAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Moq;
using AsianMarketplace_WebAPI.Tests.Test_Fixures;

namespace AsianMarketplace_WebAPI.Tests.Controllers
{
    public class OrderControllerTests: IClassFixture<OrderControllerTestsFixture>
    {
        private readonly AsianMarketplaceDbContext _context;
        private readonly Mock<IMapper> _mockMapper;
        private readonly OrderController _controller;

        public OrderControllerTests(OrderControllerTestsFixture fixture)
        {
            _context = fixture.Context;
            // Mock the AutoMapper mapper to use when mapping my entities to DTOs
            _mockMapper = fixture.MockMapper;
            // Using my controller, have it use the mocked context and mocked mapper
            _controller = new OrderController(_context, _mockMapper.Object);
        }

        [Fact]
        public async Task CreateOrder_ShouldReturnCreatedAtAction()
        {
            // Creates an order DTO and an order as input
            var orderDTO = new OrderDTO { 
                OrderId = Guid.NewGuid(), 
                OrderDate = DateTime.Parse("2024-08-01"), 
                Username = "Test4"
            };
            var order = new Order {
                OrderId = orderDTO.OrderId,
                OrderDate = orderDTO.OrderDate,
                Username = orderDTO.Username    
            };

            // Sets up the mock to return the order when Map is called with any OrderDTO
            _mockMapper.Setup(m => m.Map<Order>(It.IsAny<OrderDTO>())).Returns(order);

            // Store the result after calling CreateOrder
            var result = await _controller.CreateOrder(orderDTO);

            // Store the action result
            var createdAtResult = result as CreatedAtActionResult;
            // Check if the result is not null
            createdAtResult.Should().NotBeNull();
            // Check for a 201 response
            createdAtResult.StatusCode.Should().Be(201);

            // Store the value of the created order
            var createdOrder = createdAtResult.Value as OrderDTO;
            // Check if the result is not null
            createdOrder.Should().NotBeNull();
            // Check if each of the fields match the DTO field values
            createdOrder.OrderId.Should().Be(orderDTO.OrderId);
            createdOrder.OrderDate.Should().Be(orderDTO.OrderDate);
            createdOrder.Username.Should().Be(orderDTO.Username);

            // Verify the item was added to the context
            var dbOrder = await _context.Orders.FindAsync(order.OrderId);
            dbOrder.Should().NotBeNull();
            dbOrder.OrderId.Should().Be(orderDTO.OrderId);
            dbOrder.OrderDate.Should().Be(orderDTO.OrderDate);
            dbOrder.Username.Should().Be(orderDTO.Username);
        }

        [Fact]
        public async Task GetOrders_ShouldReturnAllOrders()
        {
            // Store the result after calling GetOrders
            var result = await _controller.GetOrders();

            // Store the action result
            var okResult = result.Result as OkObjectResult;
            // Check if the result is not null
            okResult.Should().NotBeNull();
            // Check for a 200 response
            okResult.StatusCode.Should().Be(200);

            // Store the value of the list of orders
            var orders = okResult.Value as List<OrderDTO>;
            // Check if the result is not null
            orders.Should().NotBeNull();
            // Check if the first three cart items have the correct dates
            orders[0].OrderDate.Should().Be(DateTime.Parse("2024-08-04"));
            orders[1].OrderDate.Should().Be(DateTime.Parse("2024-08-03"));
            //Check if the first three cart items have the correct quantities
            orders[0].Username.Should().Be("Test1");
            orders[1].Username.Should().Be("Test2");
        }

        [Fact]
        public async Task GetOrder_ShouldReturnAnOrder()
        {
            var result = await GetFirstRecord();
            // Create an order given the information above
            var order = new Order { OrderId = result.OrderId, OrderDate = result.OrderDate, Username = result.Username };

            // Create an orderDTO with the same information
            var orderDTO = new OrderDTO {
                OrderId = result.OrderId,
                OrderDate = result.OrderDate,
                Username = result.Username
            };

            // Setup the mock to map that order to the orderDTO and return the orderDTO
            _mockMapper.Setup(m => m.Map<OrderDTO>(order)).Returns(orderDTO);

            // Store the result after calling GetOrder
            var resultingOrder = await _controller.GetOrder(orderDTO.OrderId);

            // Store the action result and check if the order is null
            var orderFound = resultingOrder.Result as OkObjectResult;
            orderFound.Should().NotBeNull();
            // Check for a 200 response
            orderFound.StatusCode.Should().Be(200);

            // Verify the order is in the database
            var dbOrder = await _context.Orders.FindAsync(order.OrderId);
            dbOrder.Should().NotBeNull();
            dbOrder.OrderId.Should().Be(orderDTO.OrderId);
            dbOrder.OrderDate.Should().Be(orderDTO.OrderDate);
            dbOrder.Username.Should().Be(orderDTO.Username);
        }


        [Fact]
        public async Task UpdateOrder_ReturnNoContent()
        {
            var result = await GetFirstRecord();

            // Create an OrderDTO with the information above
            var orderDTO = new OrderDTO {
                OrderId = result.OrderId,
                OrderDate = result.OrderDate,
                Username = result.Username
            };

            var updatedOrder = _controller.UpdateOrder(result.OrderId, orderDTO);

            var orderUpdated = updatedOrder.Result as NoContentResult;
            orderUpdated.Should().NotBeNull();
            orderUpdated.StatusCode.Should().Be(204);

            // Verify the order is in the database
            var dbOrder = await _context.Orders.FindAsync(result.OrderId);
            dbOrder.Should().NotBeNull();
            dbOrder.OrderId.Should().Be(orderDTO.OrderId);
            dbOrder.OrderDate.Should().Be(orderDTO.OrderDate);
            dbOrder.Username.Should().Be(orderDTO.Username);
        }


        [Fact]
        public async Task DeleteOrder_ReturnNoContent()
        {
            var result = await GetFirstRecord();

            var deletedOrder = _controller.DeleteOrder(result.OrderId);

            var orderDeleted = deletedOrder.Result as NoContentResult;
            orderDeleted.Should().NotBeNull();
            orderDeleted.StatusCode.Should().Be(204);

            // Verify the order is not in the database
            var dbOrder = await _context.Orders.FindAsync(result.OrderId);
            dbOrder.Should().BeNull();
        }

        public async Task<Order> GetFirstRecord()
        {
            // Store the result after calling GetOrders
            var result = await _controller.GetOrders();

            // Store the action result
            var okResult = result.Result as OkObjectResult;
            // Store the list of orders that you will pull an order from
            var orders = okResult.Value as List<OrderDTO>;

            // Store the first order information
            var orderId = orders.FirstOrDefault().OrderId;
            var orderDate = orders.FirstOrDefault().OrderDate;
            var username = orders.FirstOrDefault().Username;

            // Store the first record and return it, to test
            var firstRecord = new Order { OrderId = orderId, OrderDate = orderDate, Username = username };
            return firstRecord;
        }
    }
}
