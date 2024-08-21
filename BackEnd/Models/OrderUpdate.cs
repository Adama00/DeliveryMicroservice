namespace BackEnd.Models
{
    public class OrderUpdate
    {
        public int OrderId { get; set; }
        public string? DeliveryStatus { get; set; }
        public DateTime? OrderFulfilled { get; set; }
    }
}
