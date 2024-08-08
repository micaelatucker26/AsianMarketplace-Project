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
    public class OrderControllerTestsFixture: IDisposable
    {
        public AsianMarketplaceDbContext Context { get; private set; }
        public Mock<IMapper> MockMapper { get; private set; }

        public OrderControllerTestsFixture()
        {
            var options = new DbContextOptionsBuilder<AsianMarketplaceDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            Context = new AsianMarketplaceDbContext(options);

            Context.Orders.AddRange(new List<Order>
            {
                new Order {
                    OrderId = Guid.NewGuid(),
                    OrderDate = DateTime.Parse("2024-08-04"),
                    Username = "Test1",
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem { Price = 10.99M, Quantity = 5, ItemId = Guid.NewGuid(), OrderId = Guid.NewGuid() },
                        new OrderItem { Price = 1.99M, Quantity = 10, ItemId = Guid.NewGuid(), OrderId = Guid.NewGuid() }
                    }
                   
                },
                new Order { 
                    OrderId = Guid.NewGuid(), 
                    OrderDate = DateTime.Parse("2024-08-03"), 
                    Username = "Test2",
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem { Price = 15.99M, Quantity = 1, ItemId = Guid.NewGuid(), OrderId = Guid.NewGuid() },
                        new OrderItem { Price = 20.99M, Quantity = 2, ItemId = Guid.NewGuid(), OrderId = Guid.NewGuid() }
                    }
                }
            });
            Context.SaveChanges();

            MockMapper = new Mock<IMapper>();

            MockMapper.Setup(m => m.Map<List<OrderDTO>>(It.IsAny<List<Order>>()))
                .Returns((List<Order> source) =>
                {
                    return source.Select(order => new OrderDTO
                    { 
                        OrderId = order.OrderId, 
                        OrderDate = order.OrderDate, 
                        Username = order.Username,
                    }).ToList();
                });
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
