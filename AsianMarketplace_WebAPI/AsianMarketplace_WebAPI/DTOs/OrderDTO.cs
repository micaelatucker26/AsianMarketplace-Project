using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace AsianMarketplace_WebAPI.DTOs
{
    public class OrderDTO
    {
        [Required]
        public Guid OrderId { get; set; }

        [Required]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime OrderDate { get; set; }
        [Required]
        public List<OrderItemDTO> OrderItems { get; set; }
    }
}
