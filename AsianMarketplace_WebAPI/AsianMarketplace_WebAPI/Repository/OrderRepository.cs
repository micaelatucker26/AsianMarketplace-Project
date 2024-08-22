using AsianMarketplace_WebAPI.DTOs;
using AsianMarketplace_WebAPI.Interfaces;
using AsianMarketplace_WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AsianMarketplace_WebAPI.Repository
{
    public class OrderRepository : IOrderRepo
    {
        private readonly AsianMarketplaceDbContext _marketplaceDbContext;
        public OrderRepository(AsianMarketplaceDbContext context)
        {
            _marketplaceDbContext = context;
        }

        public async Task<Order> CreateOrder(Order newOrder)
        {
            await _marketplaceDbContext.Orders.AddAsync(newOrder);
            await _marketplaceDbContext.SaveChangesAsync();
            return newOrder;
        }

        public async Task<Order> DeleteOrder(Guid orderId)
        {
            var order = await _marketplaceDbContext.Orders.FindAsync(orderId);
            if (order == null)
            {
                return null;
            }

            _marketplaceDbContext.Remove(order);
            await _marketplaceDbContext.SaveChangesAsync();
            return order;
        }

        public async Task<Order> GetOrder(Guid orderId)
        {
            var order = await _marketplaceDbContext.Orders.FindAsync(orderId);
            if (order == null)
            {
                return null;
            }
            return order;
        }

        public async Task<List<Order>> GetOrders()
        {
            var orders = await _marketplaceDbContext.Orders.ToListAsync();
            if (orders == null)
            {
                return null;
            }
            return orders;
        }

        public async Task<Order> UpdateOrder(Guid orderId, OrderDTO orderDTO)
        {
            var existingOrder = await _marketplaceDbContext.Orders.FindAsync(orderId);
            if (existingOrder == null)
            {
                return null;
            }

            existingOrder.OrderDate = orderDTO.OrderDate;
            existingOrder.Username = orderDTO.Username;

            await _marketplaceDbContext.SaveChangesAsync();
            return existingOrder;
        }
    }
}
