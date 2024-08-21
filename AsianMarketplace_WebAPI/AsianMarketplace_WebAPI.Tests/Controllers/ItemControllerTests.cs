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
    public class ItemControllerTests: IClassFixture<ItemControllerTestsFixture>
    {
        //private readonly AsianMarketplaceDbContext _context;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ItemController _controller;

        public ItemControllerTests(ItemControllerTestsFixture fixture)
        {
            //_context = fixture.Context;
            //// Mock the AutoMapper mapper to use when mapping my entities to DTOs
            //_mockMapper = fixture.MockMapper;
            //// Using my controller, have it use the mocked context and mocked mapper
            //_controller = new ItemController(_context, _mockMapper.Object);
        }

        [Fact]
        public async Task CreateItem_ShouldReturnCreatedAtAction()
        {
            // Creates an item DTO and an item as input
            var itemDTO = new ItemDTO { ItemId = Guid.NewGuid(), Name = "Test Item", Description = "Test Description", 
                Quantity = 4, Price = 4.00M, ImageUrl = "Test URL", SubCategoryName = "Test SubCategory"};
            var item = new Item { ItemId = itemDTO.ItemId, Name = itemDTO.Name, Description = itemDTO.Description, 
                Quantity = itemDTO.Quantity, Price = itemDTO.Price, ImageUrl = itemDTO.ImageUrl, SubCategoryName = itemDTO.SubCategoryName };

            // Sets up the mock to return the item when Map is called with any ItemDTO
            _mockMapper.Setup(m => m.Map<Item>(It.IsAny<ItemDTO>())).Returns(item);

            // Store the result after calling CreateItem
            //var result = await _controller.CreateItem(itemDTO);

            // Store the action result
            //var createdAtResult = result as CreatedAtActionResult;
            //// Check if the result is not null
            //createdAtResult.Should().NotBeNull();
            //// Check for a 201 response
            //createdAtResult.StatusCode.Should().Be(201);

            //// Store the value of the created item
            //var createdItem = createdAtResult.Value as ItemDTO;
            // Check if the result is not null
            //createdItem.Should().NotBeNull();
            //// Check if each of the fields match the DTO field values
            //createdItem.ItemId.Should().Be(itemDTO.ItemId);
            //createdItem.Name.Should().Be(itemDTO.Name);
            //createdItem.Description.Should().Be(itemDTO.Description);
            //createdItem.Quantity.Should().Be(itemDTO.Quantity);
            //createdItem.Price.Should().Be(itemDTO.Price);
            //createdItem.ImageUrl.Should().Be(itemDTO.ImageUrl);
            //createdItem.SubCategoryName.Should().Be(itemDTO.SubCategoryName);

            // Verify the item was added to the context
            //var dbItem = await _context.Items.FindAsync(item.ItemId);
            //dbItem.Should().NotBeNull();
            //dbItem.ItemId.Should().Be(itemDTO.ItemId);
            //dbItem.Name.Should().Be(itemDTO.Name);
            //dbItem.Description.Should().Be(itemDTO.Description);
            //dbItem.Quantity.Should().Be(itemDTO.Quantity);
            //dbItem.Price.Should().Be(itemDTO.Price);
            //dbItem.ImageUrl.Should().Be(itemDTO.ImageUrl);
            //dbItem.SubCategoryName.Should().Be(itemDTO.SubCategoryName);
        }

        [Fact]
        public async Task GetItems_ShouldReturnAllItems()
        {
            // Store the result after calling GetItems
            //var result = await _controller.GetItems();

            //// Store the action result
            //var okResult = result.Result as OkObjectResult;
            //// Check if the result is not null
            //okResult.Should().NotBeNull();
            //// Check for a 200 response
            //okResult.StatusCode.Should().Be(200);

            //// Store the value of the list of cart items
            //var items = okResult.Value as List<ItemDTO>;
            //// Check if the result is not null
            //items.Should().NotBeNull();
            //// Check if the first three cart items have the correct names
            //items[0].Name.Should().Be("testItem1");
            //items[1].Name.Should().Be("testItem2");
            //items[2].Name.Should().Be("testItem3");
            ////Check if the first three cart items have the correct quantities
            //items[0].Quantity.Should().Be(1);
            //items[1].Quantity.Should().Be(2);
            //items[2].Quantity.Should().Be(3);
        }

        //[Fact]
        //public async Task GetItem_ShouldReturnAnItem()
        //{
        //    var result = await GetFirstRecord();
            // Create an item given the information above
        //    var item = new Item { ItemId = result.ItemId, Name = result.Name, Description = result.Description, 
        //        Quantity = result.Quantity, Price = result.Price, ImageUrl = result.ImageUrl, SubCategoryName = result.SubCategoryName };

        //    // Create an itemDTO with the same information
        //    var itemDTO = new ItemDTO {
        //        ItemId = result.ItemId,
        //        Name = result.Name,
        //        Description = result.Description,
        //        Quantity = result.Quantity,
        //        Price = result.Price,
        //        ImageUrl = result.ImageUrl,
        //        SubCategoryName = result.SubCategoryName
        //    };

        //    // Setup the mock to map that item to the itemDTO and return the itemDTO
        //    _mockMapper.Setup(m => m.Map<ItemDTO>(item)).Returns(itemDTO);

        //    // Store the result after calling GetItem
        //    //var resultingItem = await _controller.GetItem(itemDTO.ItemId);

        //    //// Store the action result and check if the item is null
        //    //var itemFound = resultingItem.Result as OkObjectResult;
        //    //itemFound.Should().NotBeNull();
        //    //// Check for a 200 response
        //    //itemFound.StatusCode.Should().Be(200);

        //    //// Verify the item is in the database
        //    //var dbItem = await _context.Items.FindAsync(item.ItemId);
        //    //dbItem.Should().NotBeNull();
        //    //dbItem.ItemId.Should().Be(itemDTO.ItemId);
        //    //dbItem.Name.Should().Be(itemDTO.Name);
        //    //dbItem.Description.Should().Be(itemDTO.Description);
        //    //dbItem.Quantity.Should().Be(itemDTO.Quantity);
        //}


        //[Fact]
        //public async Task UpdateItem_ReturnNoContent()
        //{
        //    var result = await GetFirstRecord();

        //    // Create an ItemDTO with the information above
        //    var itemDTO = new ItemDTO {
        //        ItemId = result.ItemId,
        //        Name = result.Name,
        //        Description = result.Description,
        //        Quantity = result.Quantity,
        //        Price = result.Price,
        //        ImageUrl = result.ImageUrl,
        //        SubCategoryName = result.SubCategoryName
        //    };

        //    //var updatedItem = _controller.UpdateItem(result.ItemId, itemDTO);

        //    //var itemUpdated = updatedItem.Result as NoContentResult;
        //    //itemUpdated.Should().NotBeNull();
        //    //itemUpdated.StatusCode.Should().Be(204);

        //    //// Verify the item is in the database
        //    //var dbItem = await _context.Items.FindAsync(result.ItemId);
        //    //dbItem.Should().NotBeNull();
        //    //dbItem.ItemId.Should().Be(itemDTO.ItemId);
        //    //dbItem.Name.Should().Be(itemDTO.Name);
        //    //dbItem.Description.Should().Be(itemDTO.Description);
        //    //dbItem.Quantity.Should().Be(itemDTO.Quantity);
        //}


        //[Fact]
        //public async Task DeleteItem_ReturnNoContent()
        //{
        //    var result = await GetFirstRecord();

        //    //var deletedItem = _controller.DeleteItem(result.ItemId);

        //    //var itemDeleted = deletedItem.Result as NoContentResult;
        //    //itemDeleted.Should().NotBeNull();
        //    //itemDeleted.StatusCode.Should().Be(204);

        //    //// Verify the item is not in the database
        //    //var dbItem = await _context.Items.FindAsync(result.ItemId);
        //    //dbItem.Should().BeNull();
        //}

        //public async Task<Item> GetFirstRecord()
        //{
            // Store the result after calling GetItems
            //var result = await _controller.GetItems();

            // Store the action result
            //var okResult = result.Result as OkObjectResult;
            // Store the list of items that you will pull an item from
            //var items = okResult.Value as List<ItemDTO>;

            //// Store the first item information
            //var itemId = items.FirstOrDefault().ItemId;
            //var name = items.FirstOrDefault().Name;
            //var description = items.FirstOrDefault().Description;
            //var quantity = items.FirstOrDefault().Quantity;
            //var price = items.FirstOrDefault().Price;
            //var imageURL = items.FirstOrDefault().ImageUrl;
            //var subCategoryName = items.FirstOrDefault().SubCategoryName;

            // Store the first record and return it, to test
            //var firstRecord = new Item { ItemId = itemId, Name = name, Description = description, 
            //    Quantity = quantity, Price = price, ImageUrl = imageURL, SubCategoryName = subCategoryName };
            //return firstRecord;
        //}
    }
}
