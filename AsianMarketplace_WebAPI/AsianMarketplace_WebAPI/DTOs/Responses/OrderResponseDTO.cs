using AsianMarketplace_WebAPI.Models;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace AsianMarketplace_WebAPI.DTOs.Responses
{
    public class OrderResponseDTO
    {
        public Guid OrderId { get; set; }

        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime OrderDate { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
