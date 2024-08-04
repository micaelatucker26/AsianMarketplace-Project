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
    public class CategoryControllerTests
    {
        private readonly AsianMarketplaceDbContext _context;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CategoryController _controller;

        public CategoryControllerTests()
        {
            var options = new DbContextOptionsBuilder<AsianMarketplaceDbContext>
                ().UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new AsianMarketplaceDbContext(options);

            // Seed the database with test data
            _context.CartItems.AddRange(new List<CartItem>
            {
               new CartItem { ItemId = Guid.NewGuid(), Quantity = 2, UserId = "user1"},
               new CartItem { ItemId = Guid.NewGuid(), Quantity = 3, UserId = "user2"},
               new CartItem { ItemId = Guid.NewGuid(), Quantity = 1, UserId = "user3" }
            });
            _context.SaveChanges();

            _mockMapper = new Mock<IMapper>();
            _mockMapper.Setup(m => m.Map<List<CartItemDTO>>(It.IsAny<List<CartItem>>()))
                .Returns((List<CartItem> source) =>
                {
                    return source.Select(item => new CartItemDTO 
                    { ItemId = item.ItemId, Quantity = item.Quantity, UserId = item.UserId }).ToList();
                });

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

            // Store the result
            var result = await _controller.CreateCartItem(cartItemDTO);

            var createdAtResult = result as CreatedAtActionResult;
            createdAtResult.Should().NotBeNull();
            createdAtResult.StatusCode.Should().Be(201);

            var createdCartItem = createdAtResult.Value as CartItemDTO;
            createdCartItem.Should().NotBeNull();
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
            var result = await _controller.GetCartItems();

            var okResult = result.Result as OkObjectResult;

            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);

            var cartItems = okResult.Value as List<CartItemDTO>;
            cartItems.Should().NotBeNull();
            //cartItems.Count.Should().Be(3);
            cartItems[0].UserId.Should().Be("user1");
            cartItems[1].UserId.Should().Be("user2");
            cartItems[2].UserId.Should().Be("user3");
        }


        [Fact]
        public async Task GetCartItem_ShouldReturnACartItem()
        {
            var result = await _controller.GetCartItems();
            var okResult = result.Result as OkObjectResult;
            var cartItems = okResult.Value as List<CartItemDTO>;

            var itemId = cartItems.FirstOrDefault().ItemId;
            var userId = cartItems.FirstOrDefault().UserId;
            var quantity = cartItems.FirstOrDefault().Quantity;

            // Create a cart item given the two pieces of information above and other details
            var cartItem = new CartItem { ItemId = itemId, UserId = userId, Quantity = quantity };

            // Create a CartDTO with the same information
            var cartItemDTO = new CartItemDTO { ItemId = itemId, UserId = userId, Quantity = quantity };

            // Setup the mock to map that cartItem to the CartItemDTO and return the cartItemDTO
            _mockMapper.Setup(m => m.Map<CartItemDTO>(cartItem)).Returns(cartItemDTO);

            // Store the result
            var resultingCartItem = await _controller.GetCartItem(cartItemDTO.ItemId, cartItemDTO.UserId);

            var cartItemFound = resultingCartItem.Result as OkObjectResult;
            cartItemFound.Should().NotBeNull();
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
            var result = await _controller.GetCartItems();
            var okResult = result.Result as OkObjectResult;
            var cartItems = okResult.Value as List<CartItemDTO>;

            var itemId = cartItems.FirstOrDefault().ItemId;
            var userId = cartItems.FirstOrDefault().UserId;
            var quantity = cartItems.FirstOrDefault().Quantity;

            var cartItemDTO = new CartItemDTO { Quantity = 3, ItemId = itemId, UserId = userId };

            var updatedCartItem = _controller.UpdateCartItem(itemId, userId, cartItemDTO);

            var cartItemUpdated = updatedCartItem.Result as NoContentResult;
            cartItemUpdated.Should().NotBeNull();
            cartItemUpdated.StatusCode.Should().Be(204);

            // Verify the cart item is in the database
            var dbCartItem = await _context.CartItems.FindAsync(itemId, userId);
            dbCartItem.Should().NotBeNull();
            dbCartItem.Quantity.Should().Be(cartItemDTO.Quantity);
            dbCartItem.ItemId.Should().Be(cartItemDTO.ItemId);
            dbCartItem.UserId.Should().Be(cartItemDTO.UserId);
        }


            [Fact]
            public async Task DeleteCartItem_ReturnNoContent()
            {
            var result = await _controller.GetCartItems();
            var okResult = result.Result as OkObjectResult;
            var cartItems = okResult.Value as List<CartItemDTO>;

            var itemId = cartItems.FirstOrDefault().ItemId;
            var userId = cartItems.FirstOrDefault().UserId;
            var quantity = cartItems.FirstOrDefault().Quantity;

            var deletedCartItem = _controller.DeleteCartItem(itemId, userId);

            var cartItemUpdated = deletedCartItem.Result as NoContentResult;
            cartItemUpdated.Should().NotBeNull();
            cartItemUpdated.StatusCode.Should().Be(204);

            // Verify the cart item is not in the database
            var dbCartItem = await _context.CartItems.FindAsync(itemId, userId);
            dbCartItem.Should().BeNull();
        }
    }
}
