using AsianMarketplace_WebAPI.Controllers;
using AsianMarketplace_WebAPI.DTOs;
using AsianMarketplace_WebAPI.Models;
using AsianMarketplace_WebAPI.Tests.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AsianMarketplace_WebAPI.Tests.Controllers
{
    public class CartItemControllerTests
    {
        private readonly Mock<AsianMarketplaceDbContext> _mockDbContext;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CartItemController _controller;
        private readonly Mock<DbSet<CartItem>> _mockCartItems;

        public CartItemControllerTests()
        {
            _mockCartItems = new Mock<DbSet<CartItem>>();
            _mockDbContext = new Mock<AsianMarketplaceDbContext>();
            _mockMapper = new Mock<IMapper>();
            _controller = new CartItemController(_mockDbContext.Object, _mockMapper.Object);
        }


        [Fact]
        public async Task CreateCartItem_ShouldReturnCreatedAtAction()
        {
            // Creates a cart item DTO and a cart item as input
            var cartItemDTO = new CartItemDTO { Quantity = 3, ItemId = Guid.NewGuid(), UserId = "User1" };
            var cartItem = new CartItem { Quantity = cartItemDTO.Quantity, ItemId = cartItemDTO.ItemId, UserId = cartItemDTO.UserId };

            // Sets up the mock to return the cartItem when Map is called with any CartItemDTO
            _mockMapper.Setup(m => m.Map<CartItem>(It.IsAny<CartItemDTO>())).Returns(cartItem);
            // Sets up the mock to add any CartItem to the db set of Cart Items when Add is called with any CartItem
            _mockDbContext.Setup(db => db.CartItems.Add(It.IsAny<CartItem>()));
            // Sets up the mock to save changes to the database and return 1 when SavechangesAsync is called
            _mockDbContext.Setup(db => db.SaveChangesAsync(default)).ReturnsAsync(1);

            // Store the result
            var result = await _controller.CreateCartItem(cartItemDTO);

            // Verify whether the result is a CreatedAtAction and store that in actionResult
            var actionResult = Assert.IsType<CreatedAtActionResult>(result);
            // Verify whether the CreateCartItem method is correctly configured to return the route to GetCartItem
            Assert.Equal(nameof(_controller.GetCartItem), actionResult.ActionName);
        }


        [Fact]
        public async Task GetCartItems_ShouldReturnAllCartItems()
        {
            // Create a list of cart items
            var cartItems = new List<CartItem>
            {
                new CartItem { Quantity = 2, ItemId = Guid.NewGuid(), UserId = "user1"},
                new CartItem { Quantity = 3, ItemId = Guid.NewGuid(), UserId = "user2"}
            }.AsQueryable();

            var mockSet = new Mock<DbSet<CartItem>>();

            mockSet.As<IQueryable<CartItem>>().Setup(m => m.Provider).Returns(cartItems.Provider);
            mockSet.As<IQueryable<CartItem>>().Setup(m => m.Expression).Returns(cartItems.Expression);
            mockSet.As<IQueryable<CartItem>>().Setup(m => m.ElementType).Returns(cartItems.ElementType);
            mockSet.As<IQueryable<CartItem>>().Setup(m => m.GetEnumerator()).Returns(() => cartItems.GetEnumerator());

            _mockDbContext.Setup(c => c.CartItems).Returns(mockSet.Object);

            var service = new CartItemService(_mockDbContext.Object);

            var cartItems_fromService = service.GetAllCartItems();


            // Setup the mock to return a new list of CartItemDTOs when Map is called with any list of CartItems
            _mockMapper.Setup(m => m.Map<List<CartItemDTO>>(It.IsAny<List<CartItem>>())).Returns(new List<CartItemDTO>
            {
                new CartItemDTO{Quantity = cartItems.ElementAt(0).Quantity, ItemId = cartItems.ElementAt(0).ItemId, UserId = cartItems.ElementAt(0).UserId},
                new CartItemDTO{Quantity = cartItems.ElementAt(1).Quantity, ItemId = cartItems.ElementAt(1).ItemId, UserId = cartItems.ElementAt(1).UserId}
            });

            // Verify whether 2 equals the count of items in the list returned
            Assert.Equal(2, cartItems_fromService.Count);

            // Verify the individual items
            Assert.Equal(cartItems.ElementAt(0).Quantity, cartItems_fromService.ElementAt(0).Quantity);
            Assert.Equal(cartItems.ElementAt(0).ItemId, cartItems_fromService.ElementAt(0).ItemId);
            Assert.Equal(cartItems.ElementAt(0).UserId, cartItems_fromService.ElementAt(0).UserId);

            Assert.Equal(cartItems.ElementAt(1).Quantity, cartItems_fromService.ElementAt(1).Quantity);
            Assert.Equal(cartItems.ElementAt(1).ItemId, cartItems_fromService.ElementAt(1).ItemId);
            Assert.Equal(cartItems.ElementAt(1).UserId, cartItems_fromService.ElementAt(1).UserId);
        }


        [Fact]
        public async Task GetCartItem_ShouldReturnACartItem()
        {
            // Generate a itemId and store it
            var itemId = Guid.NewGuid();

            // Store a unique userId
            var userId = "user1";

            // Create a cart item given the two pieces of information above and other details
            var cartItem = new CartItem { ItemId = itemId, UserId = userId, Quantity = 1 };

            // Create a CartDTO with the same information
            var cartItemDTO = new CartItemDTO { ItemId = itemId, UserId = userId, Quantity = 1 };

            // Setup the mock to find a cart, given itemId and userId, and return the cart item
            _mockDbContext.Setup(d => d.CartItems.FindAsync(itemId, userId)).ReturnsAsync(cartItem);

            // Setup the mock to map that cartItem to the CartItemDTO and return the cartItemDTO
            _mockMapper.Setup(m => m.Map<CartItemDTO>(cartItem)).Returns(cartItemDTO);

            // Store the result
            var result = await _controller.GetCartItem(itemId, userId);

            // Verify whether the result is of type OkObjectResult and store that
            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            // Verify whether the value returned is of type CartItemDTO and store it
            var returnValue = Assert.IsType<CartItemDTO>(okResult.Value);

            // Verify whether itemId equals the returned ItemId
            Assert.Equal(itemId, returnValue.ItemId);

            // Verify whether userId equals the returned UserId
            Assert.Equal(userId, returnValue.UserId);
        }


        [Fact]
        public async Task UpdateCartItem_ReturnNoContent()
        {
            var itemId = Guid.NewGuid();
            var userId = "user1";
            var cartItemDTO = new CartItemDTO { Quantity = 3, ItemId = itemId, UserId = userId };
            var cartItem = new CartItem { ItemId = itemId, UserId = userId, Quantity = 3 };

            _mockDbContext.Setup(d => d.CartItems.FindAsync(itemId, userId)).ReturnsAsync(cartItem);

            _mockDbContext.Setup(d => d.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var result = _controller.UpdateCartItem(itemId, userId, cartItemDTO);

            Assert.IsType<NoContentResult>(result.Result);
        }


        [Fact]
        public async Task DeleteCartItem_ReturnNoContent()
        {
            var itemId = Guid.NewGuid();
            var userId = "user1";

            var cartItem = new CartItem { ItemId = itemId, UserId = userId, Quantity = 3 };

            _mockDbContext.Setup(d => d.CartItems.FindAsync(itemId, userId)).ReturnsAsync(cartItem);

            _mockDbContext.Setup(d => d.CartItems.Remove(cartItem));

            _mockDbContext.Setup(d => d.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var result = await _controller.DeleteCartItem(itemId, userId);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
