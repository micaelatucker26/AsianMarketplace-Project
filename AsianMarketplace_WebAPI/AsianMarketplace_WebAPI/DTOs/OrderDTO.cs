using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AsianMarketplace_WebAPI.DTOs
{
    public class OrderDTO
    {
        public Guid OrderId { get; set; }

        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime OrderDate { get; set; }

        public string Username { get; set; }
    }
}
