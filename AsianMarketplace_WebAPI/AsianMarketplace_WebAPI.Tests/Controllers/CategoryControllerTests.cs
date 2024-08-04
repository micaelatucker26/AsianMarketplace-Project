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
    public class CategoryControllerTests: IClassFixture<CategoryControllerTestsFixture>
    {
        private readonly AsianMarketplaceDbContext _context;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CategoryController _controller;

        public CategoryControllerTests(CategoryControllerTestsFixture fixture)
        {
            _context = fixture.Context;
            // Mock the AutoMapper mapper to use when mapping my entities to DTOs
            _mockMapper = fixture.MockMapper;
            // Using my controller, have it use the mocked context and mocked mapper
            _controller = new CategoryController(_context, _mockMapper.Object);
        }

        [Fact]
        public async Task CreateCategory_ShouldReturnCreatedAtAction()
        {
            // Creates a category DTO and a category as input
            var categoryDTO = new CategoryDTO { Name = "Test1" };
            var category = new Category { Name = categoryDTO.Name };

            // Sets up the mock to return the category when Map is called with any CategoryDTO
            _mockMapper.Setup(m => m.Map<Category>(It.IsAny<CategoryDTO>())).Returns(category);

            // Store the result after calling CreateCategory
            var result = await _controller.CreateCategory(categoryDTO);

            // Store the action result
            var createdAtResult = result as CreatedAtActionResult;
            // Check if the result is not null
            createdAtResult.Should().NotBeNull();
            // Check for a 201 response
            createdAtResult.StatusCode.Should().Be(201);

            // Store the value of the created category
            var createdCategory = createdAtResult.Value as CategoryDTO;
            // Check if the result is not null
            createdCategory.Should().NotBeNull();
            // Check if each of the fields match the DTO field values
            createdCategory.Name.Should().Be(categoryDTO.Name);

            // Verify the category was added to the context
            var dbCategory = await _context.Categories.FindAsync(category.Name);
            dbCategory.Should().NotBeNull();
            dbCategory.Name.Should().Be(categoryDTO.Name);
        }


        [Fact]
        public async Task GetCategories_ShouldReturnAllCategories()
        {
            // Store the result after calling GetCategories
            var result = await _controller.GetCategories();

            // Store the action result
            var okResult = result.Result as OkObjectResult;
            // Check if the result is not null
            okResult.Should().NotBeNull();
            // Check for a 200 response
            okResult.StatusCode.Should().Be(200);

            // Store the value of the list of categories
            var categories = okResult.Value as List<CategoryDTO>;
            // Check if the result is not null
            categories.Should().NotBeNull();
            // Check if the first three categories have the correct names
            categories[0].Name.Should().Be("testCategory1");
            categories[1].Name.Should().Be("testCategory2");
            categories[2].Name.Should().Be("testCategory3");
        }


        [Fact]
        public async Task GetCategory_ShouldReturnACategory()
        {
            var result = await GetFirstRecord();
            // Create a category given the information above
            var category = new Category { Name = result.Name };

            // Create a CategoryDTO with the same information
            var categoryDTO = new CategoryDTO { Name = result.Name };

            // Setup the mock to map that category to the CategoryDTO and return the categoryDTO
            _mockMapper.Setup(m => m.Map<CategoryDTO>(category)).Returns(categoryDTO);

            // Store the result after calling GetCategory
            var resultingCategory = await _controller.GetCategory(categoryDTO.Name);

            // Store the action result and check if the category is null
            var categoryFound = resultingCategory.Result as OkObjectResult;
            categoryFound.Should().NotBeNull();
            // Check for a 200 response
            categoryFound.StatusCode.Should().Be(200);

            // Verify the cart item is in the database
            var dbCategory = await _context.Categories.FindAsync(category.Name);
            dbCategory.Should().NotBeNull();
            dbCategory.Name.Should().Be(categoryDTO.Name);
        }


        [Fact]
        public async Task UpdateCategory_ReturnNoContent()
        {
            var result = await GetFirstRecord();

            // Create a CategoryDTO with the information above
            var categoryDTO = new CategoryDTO { Name = result.Name };

            var updatedCategory = _controller.UpdateCategory(result.Name, categoryDTO);

            var categoryUpdated = updatedCategory.Result as NoContentResult;
            categoryUpdated.Should().NotBeNull();
            categoryUpdated.StatusCode.Should().Be(204);

            // Verify the category is in the database
            var dbCategory = await _context.Categories.FindAsync(result.Name);
            dbCategory.Should().NotBeNull();
            dbCategory.Name.Should().Be(categoryDTO.Name);
        }


        [Fact]
        public async Task DeleteCategory_ReturnNoContent()
        {
            var result = await GetFirstRecord();

            var deletedCategory = _controller.DeleteCategory(result.Name);

            var categoryDeleted = deletedCategory.Result as NoContentResult;
            categoryDeleted.Should().NotBeNull();
            categoryDeleted.StatusCode.Should().Be(204);

            // Verify the category is not in the database
            var dbCategory = await _context.Categories.FindAsync(result.Name);
            dbCategory.Should().BeNull();
        }

        public async Task<Category> GetFirstRecord()
        {
            // Store the result after calling GetCategories
            var result = await _controller.GetCategories();

            // Store the action result
            var okResult = result.Result as OkObjectResult;
            // Store the list of categories that you will pull a category from
            var categories = okResult.Value as List<CategoryDTO>;

            // Store the first category information
            var name = categories.FirstOrDefault().Name;

            // Store the first record and return it, to test
            var firstRecord = new Category { Name = name };
            return firstRecord;
        }
    }
}
