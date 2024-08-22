using AsianMarketplace_WebAPI.DTOs;
using AsianMarketplace_WebAPI.Interfaces;
using AsianMarketplace_WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AsianMarketplace_WebAPI.Repository
{
    public class OrderItemRepository : IOrderItemRepo
    {
        private readonly AsianMarketplaceDbContext _marketplaceDbContext;
        public OrderItemRepository(AsianMarketplaceDbContext context) 
        { 
            _marketplaceDbContext = context;
        }
        public async Task<OrderItem> CreateOrderItem(OrderItem newOrderItem)
        {
            await _marketplaceDbContext.OrderItems.AddAsync(newOrderItem);
            await _marketplaceDbContext.SaveChangesAsync();
            return newOrderItem;
        }

        public async Task<OrderItem> DeleteOrderItem(Guid itemId, Guid orderId)
        {
            var orderItem = await _marketplaceDbContext.OrderItems.FindAsync(itemId, orderId);
            if (orderItem == null)
            {
                return null;
            }

            _marketplaceDbContext.Remove(orderItem);
            await _marketplaceDbContext.SaveChangesAsync();
            return orderItem;
        }

        public async Task<OrderItem> GetOrderItem(Guid itemId, Guid orderId)
        {
            var orderItem = await _marketplaceDbContext.OrderItems.FindAsync(itemId, orderId);
            if (orderItem == null)
            {
                return null;
            }
            return orderItem;
        }

        public async Task<List<OrderItem>> GetOrderItems()
        {
            var orderItems = await _marketplaceDbContext.OrderItems.ToListAsync();
            if (orderItems == null)
            {
                return null;
            }
            return orderItems;
        }

        public async Task<OrderItem> UpdateOrderItem(Guid itemId, Guid orderId, OrderItemDTO orderItemDTO)
        {
            var existingOrderItem = await _marketplaceDbContext.OrderItems.FindAsync(itemId, orderId);
            if (existingOrderItem == null)
            {
                return null;
            }
            existingOrderItem.Price = orderItemDTO.Price;
            existingOrderItem.Quantity = orderItemDTO.Quantity;

            await _marketplaceDbContext.SaveChangesAsync();
            return existingOrderItem;
        }
    }
}
