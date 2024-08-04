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

namespace AsianMarketplace_WebAPI.Tests.Controllers
{
    public class CartItemControllerTests
    {
        private readonly AsianMarketplaceDbContext _context;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CartItemController _controller;

        public CartItemControllerTests()
        {
            // Use EF Core built in feature to use an In-Memory database modeling my DbContext
            var options = new DbContextOptionsBuilder<AsianMarketplaceDbContext>
                ().UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new AsianMarketplaceDbContext(options);

            // Initially seed the database with test data and save these changes to the current DbContext
            _context.CartItems.AddRange(new List<CartItem>
            {
               new CartItem { ItemId = Guid.NewGuid(), Quantity = 2, UserId = "user1"},
               new CartItem { ItemId = Guid.NewGuid(), Quantity = 3, UserId = "user2"},
               new CartItem { ItemId = Guid.NewGuid(), Quantity = 1, UserId = "user3" }
            });
            _context.SaveChanges();

            // Mock the AutoMapper mapper to use when mapping my entities to DTOs
            _mockMapper = new Mock<IMapper>();
            // Setup the mapper to map any list of CartItems to a list of CartItemDTOs when Map is called
            _mockMapper.Setup(m => m.Map<List<CartItemDTO>>(It.IsAny<List<CartItem>>()))
                // Gathers the list of cart items together and returns it
                .Returns((List<CartItem> source) =>
                {
                    return source.Select(item => new CartItemDTO 
                    { ItemId = item.ItemId, Quantity = item.Quantity, UserId = item.UserId }).ToList();
                });
            // Using my controller, have it use the mocked context and mocked mapper
            _controller = new CartItemController(_context, _mockMapper.Object);
        }

        [Fact]
        public async Task CreateCartItem_ShouldReturnCreatedAtAction()
        {
            // Creates a cart item DTO and a cart item as input
            var cartItemDTO = new CartItemDTO { Quantity = 3, ItemId = Guid.NewGuid(), UserId = "User1" };
            var cartItem = new CartItem { Quantity = cartItemDTO.Quantity, ItemId = cartItemDTO.ItemId, UserId = cartItemDTO.UserId };

            // Sets up the mock to return the cartItem when Map is called with any CartItemDTO
            _mockMapper.Setup(m => m.Map<CartItem>(It.IsAny<CartItemDTO>())).Returns(cartItem);

            // Store the result after calling CreateCartItem
            var result = await _controller.CreateCartItem(cartItemDTO);

            // Store the action result
            var createdAtResult = result as CreatedAtActionResult;
            // Check if the result is not null
            createdAtResult.Should().NotBeNull();
            // Check for a 201 response
            createdAtResult.StatusCode.Should().Be(201);

            // Store the value of the created cart item
            var createdCartItem = createdAtResult.Value as CartItemDTO;
            // Check if the result is not null
            createdCartItem.Should().NotBeNull();
            // Check if each of the fields match the DTO field values
            createdCartItem.Quantity.Should().Be(cartItemDTO.Quantity);
            createdCartItem.ItemId.Should().Be(cartItemDTO.ItemId);
            createdCartItem.UserId.Should().Be(cartItemDTO.UserId);

            // Verify the cart item was added to the context
            var dbCartItem = await _context.CartItems.FindAsync(cartItem.ItemId, cartItem.UserId);
            dbCartItem.Should().NotBeNull();
            dbCartItem.Quantity.Should().Be(cartItemDTO.Quantity);
            dbCartItem.ItemId.Should().Be(cartItemDTO.ItemId);
            dbCartItem.UserId.Should().Be(cartItemDTO.UserId);
        }


        [Fact]
        public async Task GetCartItems_ShouldReturnAllCartItems()
        {
            // Store the result after calling GetCartItems
            var result = await _controller.GetCartItems();

            // Store the action result
            var okResult = result.Result as OkObjectResult;
            // Check if the result is not null
            okResult.Should().NotBeNull();
            // Check for a 200 response
            okResult.StatusCode.Should().Be(200);

            // Store the value of the list of cart items
            var cartItems = okResult.Value as List<CartItemDTO>;
            // Check if the result is not null
            cartItems.Should().NotBeNull();
            // Check if the first three cart items have the correct userIds
            cartItems[0].UserId.Should().Be("user1");
            cartItems[1].UserId.Should().Be("user2");
            cartItems[2].UserId.Should().Be("user3");
            //Check if the first three cart items have the correct quantities
            cartItems[0].Quantity.Should().Be(2);
            cartItems[1].Quantity.Should().Be(3);
            cartItems[2].Quantity.Should().Be(1);
        }


        [Fact]
        public async Task GetCartItem_ShouldReturnACartItem()
        {
            var result = await GetFirstRecord();
            // Create a cart item given the information above
            var cartItem = new CartItem { ItemId = result.ItemId, UserId = result.UserId, Quantity = result.Quantity };

            // Create a CartDTO with the same information
            var cartItemDTO = new CartItemDTO { ItemId = result.ItemId, UserId = result.UserId, Quantity = result.Quantity };

            // Setup the mock to map that cartItem to the CartItemDTO and return the cartItemDTO
            _mockMapper.Setup(m => m.Map<CartItemDTO>(cartItem)).Returns(cartItemDTO);

            // Store the result after calling GetCartItem
            var resultingCartItem = await _controller.GetCartItem(cartItemDTO.ItemId, cartItemDTO.UserId);

            // Store the action result and check if the cart item is null
            var cartItemFound = resultingCartItem.Result as OkObjectResult;
            cartItemFound.Should().NotBeNull();
            // Check for a 200 response
            cartItemFound.StatusCode.Should().Be(200);

            // Verify the cart item is in the database
            var dbCartItem = await _context.CartItems.FindAsync(cartItem.ItemId, cartItem.UserId);
            dbCartItem.Should().NotBeNull();
            dbCartItem.Quantity.Should().Be(cartItemDTO.Quantity);
            dbCartItem.ItemId.Should().Be(cartItemDTO.ItemId);
            dbCartItem.UserId.Should().Be(cartItemDTO.UserId);
        }


        [Fact]
        public async Task UpdateCartItem_ReturnNoContent()
        {
            var result = await GetFirstRecord();

            // Create a CartDTO with the information above
            var cartItemDTO = new CartItemDTO { ItemId = result.ItemId, UserId = result.UserId, Quantity = result.Quantity };

            var updatedCartItem = _controller.UpdateCartItem(result.ItemId, result.UserId, cartItemDTO);

            var cartItemUpdated = updatedCartItem.Result as NoContentResult;
            cartItemUpdated.Should().NotBeNull();
            cartItemUpdated.StatusCode.Should().Be(204);

            // Verify the cart item is in the database
            var dbCartItem = await _context.CartItems.FindAsync(result.ItemId, result.UserId);
            dbCartItem.Should().NotBeNull();
            dbCartItem.Quantity.Should().Be(cartItemDTO.Quantity);
            dbCartItem.ItemId.Should().Be(cartItemDTO.ItemId);
            dbCartItem.UserId.Should().Be(cartItemDTO.UserId);
        }


        [Fact]
        public async Task DeleteCartItem_ReturnNoContent()
        {
            var result = await GetFirstRecord();

            var deletedCartItem = _controller.DeleteCartItem(result.ItemId, result.UserId);

            var cartItemDeleted = deletedCartItem.Result as NoContentResult;
            cartItemDeleted.Should().NotBeNull();
            cartItemDeleted.StatusCode.Should().Be(204);

            // Verify the cart item is not in the database
            var dbCartItem = await _context.CartItems.FindAsync(result.ItemId, result.UserId);
            dbCartItem.Should().BeNull();
        }

        public async Task<CartItem> GetFirstRecord()
        {
            // Store the result after calling GetCartItems
            var result = await _controller.GetCartItems();

            // Store the action result
            var okResult = result.Result as OkObjectResult;
            // Store the list of cart items that you will pull a cart item from
            var cartItems = okResult.Value as List<CartItemDTO>;

            // Store the first cart item information
            var itemId = cartItems.FirstOrDefault().ItemId;
            var userId = cartItems.FirstOrDefault().UserId;
            var quantity = cartItems.FirstOrDefault().Quantity;

            // Store the first record and return it, to test
            var firstRecord = new CartItem { ItemId = itemId, UserId = userId, Quantity = quantity };
            return firstRecord;
        }
    }
}
