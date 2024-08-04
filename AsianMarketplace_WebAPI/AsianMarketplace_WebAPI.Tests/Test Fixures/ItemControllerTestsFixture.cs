using AsianMarketplace_WebAPI.DTOs;
using AsianMarketplace_WebAPI.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsianMarketplace_WebAPI.Tests.Test_Fixures
{
    public class ItemControllerTestsFixture: IDisposable
    {
        public AsianMarketplaceDbContext Context { get; private set; }
        public Mock<IMapper> MockMapper { get; private set; }

        public ItemControllerTestsFixture()
        {
            var options = new DbContextOptionsBuilder<AsianMarketplaceDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            Context = new AsianMarketplaceDbContext(options);

            Context.Items.AddRange(new List<Item>
        {
            new Item { ItemId = new Guid(), Name = "testItem1", Description = "Test Description 1", Quantity = 1, Price = 1.00M, ImageUrl = "Test URL 1",  SubCategoryName = "Test 1" },
            new Item { ItemId = new Guid(), Name = "testItem2", Description = "Test Description 2", Quantity = 2, Price = 2.00M, ImageUrl = "Test URL 2",  SubCategoryName = "Test 2" },
            new Item { ItemId = new Guid(), Name = "testItem3", Description = "Test Description 3", Quantity = 3, Price = 3.00M, ImageUrl = "Test URL 3",  SubCategoryName = "Test 3" },
        });
            Context.SaveChanges();

            MockMapper = new Mock<IMapper>();
            MockMapper.Setup(m => m.Map<List<ItemDTO>>(It.IsAny<List<Item>>()))
                .Returns((List<Item> source) =>
                {
                    return source.Select(item => new ItemDTO
                    { 
                        ItemId = item.ItemId, Name = item.Name, Description = item.Description, 
                        Quantity = item.Quantity, Price = item.Price, ImageUrl = item.ImageUrl, 
                        SubCategoryName = item.SubCategoryName }).ToList();
                });
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
