namespace BackEnd.Models
{
    public class OrderRequest
    {
        public Location? PickupLocation { get; set; }
        public Location? DeliveryLocation { get; set;}
        public double Fare { get; set; }
    }
}
