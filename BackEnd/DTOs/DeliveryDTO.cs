namespace BackEnd.DTOs
{
    public class DeliveryDTO
    {
        public int DeliveryId { get; set; }
        public int OrderId { get; set; }
        public double PickupLatitude { get; set; }
        public double PickupLongitude { get; set; }
        public double DeliveryLatitude { get; set; }
        public double DeliveryLongitude { get; set; }
        public double DeliveryDistance { get; set; }
        public DateTime OrderPlaced { get; set; }
        public DateTime? OrderFulfilled { get; set; }
        public string? CustomerId { get; set; }
        public bool IsOrderFulfilled { get; set; }
        public string? DeliveryStatus { get; set; }
        public DateTime ScheduledTime { get; set; }
        public double Fare { get; set; }
    }
}

