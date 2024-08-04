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
    public class CategoryControllerTestsFixture: IDisposable
    {
        public AsianMarketplaceDbContext Context { get; private set; }
        public Mock<IMapper> MockMapper { get; private set; }

        public CategoryControllerTestsFixture()
        {
            var options = new DbContextOptionsBuilder<AsianMarketplaceDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            Context = new AsianMarketplaceDbContext(options);

            Context.Categories.AddRange(new List<Category>
        {
            new Category { Name = "testCategory1" },
            new Category { Name = "testCategory2" },
            new Category { Name = "testCategory3" }
        });
            Context.SaveChanges();

            MockMapper = new Mock<IMapper>();
            MockMapper.Setup(m => m.Map<List<CategoryDTO>>(It.IsAny<List<Category>>()))
                .Returns((List<Category> source) =>
                {
                    return source.Select(category => new CategoryDTO
                    { Name = category.Name }).ToList();
                });
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
