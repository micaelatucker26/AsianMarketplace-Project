using AsianMarketplace_WebAPI.DTOs;
using AsianMarketplace_WebAPI.Models;

namespace AsianMarketplace_WebAPI.Interfaces
{
    public interface IOrderRepo
    {
        Task<List<Order>> GetOrders();
        Task<Order> GetOrder(Guid orderId);
        Task<Order> CreateOrder(Order order);
        Task<Order> UpdateOrder(Guid orderId, OrderDTO orderDTO);
        Task<Order> DeleteOrder(Guid orderId);
    }
}
