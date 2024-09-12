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
            var order = await _marketplaceDbContext.Orders
                .Include(o => o.OrderItems) // Eagerly load the related OrderItems
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
            if (order == null)
            {
                return null;
            }
            return order;
        }

        public async Task<List<Order>> GetOrders()
        {
            var orders = await _marketplaceDbContext.Orders.Include(o => o.OrderItems).ToListAsync();
            if (orders == null)
            {
                return null;
            }
            return orders;
        }

        public async Task<Order> UpdateOrder(Guid orderId, OrderDTO orderDTO)
        {
            var existingOrder = await _marketplaceDbContext.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
            if (existingOrder == null)
            {
                return null;
            }

            existingOrder.OrderDate = orderDTO.OrderDate;

            // Update the order items
            foreach(var itemDTO in orderDTO.OrderItems)
            {
                var existingOrderItem = existingOrder.OrderItems.FirstOrDefault(i => i.ItemId == itemDTO.ItemId);
                // Update the existing order item
                if(existingOrderItem != null)
                {
                    existingOrderItem.Price = itemDTO.Price;
                    existingOrderItem.Quantity = itemDTO.Quantity;
                }
                // Add new order item
                else
                {
                    var newOrderItem = new OrderItem
                    {
                        ItemId = itemDTO.ItemId,
                        Price = itemDTO.Price,
                        Quantity = itemDTO.Quantity,
                        OrderId = orderId // Ensure this matches the current order
                    };
                    existingOrder.OrderItems.Add(newOrderItem);
                }
            }

            // Remove order items that were deleted
            var removedItems = existingOrder.OrderItems
                .Where(oi => !orderDTO.OrderItems.Any(dto => dto.ItemId == oi.ItemId))
                .ToList();
            foreach (var removedItem in removedItems)
            {
                existingOrder.OrderItems.Remove(removedItem);
            }

            await _marketplaceDbContext.SaveChangesAsync();
            return existingOrder;
        }
    }
}
