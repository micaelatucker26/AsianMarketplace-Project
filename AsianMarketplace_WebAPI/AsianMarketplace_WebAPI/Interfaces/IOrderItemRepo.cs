using AsianMarketplace_WebAPI.DTOs;
using AsianMarketplace_WebAPI.Models;

namespace AsianMarketplace_WebAPI.Interfaces
{
    public interface IOrderItemRepo
    {
        Task<List<OrderItem>> GetOrderItems();
        Task<OrderItem> GetOrderItem(Guid itemId, Guid orderId);
        Task<OrderItem> CreateOrderItem(OrderItem orderItem);
        Task<OrderItem> UpdateOrderItem(Guid itemId, Guid orderId, OrderItemDTO orderItemDTO);
        Task<OrderItem> DeleteOrderItem(Guid itemId, Guid orderId);
    }
}
