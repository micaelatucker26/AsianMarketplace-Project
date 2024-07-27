namespace AsianMarketplace_WebAPI.DTOs
{
    public class OrderDTO
    {
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Username { get; set; }
    }
}
