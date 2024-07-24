﻿using NetTopologySuite.Geometries;
using System;

namespace BackEnd.Models
{
    public class Delivery
    {
        public int DeliveryId { get; set; }
        public int OrderId { get; set; }
        public Point PickupLocation { get; set; } = new Point(5.00, -0.18);
        public Point DeliveryLocation { get; set; } = new Point(5.00, -0.18);
        public double DeliveryDistance {  get; set; }
        public DateTime ScheduledTime { get; set; }
        public DateTime OrderPlaced { get; set; }
        public DateTime? OrderFulfilled { get; set; }
        public string? CustomerId { get; set; }
        public bool IsOrderFulfilled { get; set; }
        public string? DeliveryStatus {  get; set; }
        public double Fare { get; set; } 
    }
}



