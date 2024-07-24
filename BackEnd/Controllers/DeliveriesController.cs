using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEnd.Data;
using BackEnd.Models;
using NetTopologySuite.Geometries;
using BackEnd.DTOs;

namespace DeliveryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveriesController : ControllerBase
    {
        private readonly DeliveryDbContext _context;

        public DeliveriesController(DeliveryDbContext context)
        {
            _context = context;
        }
        // GET: api/Deliveries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeliveryDTO>>> GetDeliveries()
        {
            return await _context.Deliveries
                .Select(d => new DeliveryDTO
                {
                    DeliveryId = d.DeliveryId,
                    OrderId = d.OrderId,
                    PickupLatitude = d.PickupLocation.Coordinate.Y,
                    PickupLongitude = d.PickupLocation.Coordinate.X,
                    DeliveryLatitude = d.DeliveryLocation.Coordinate.Y,
                    DeliveryLongitude = d.DeliveryLocation.Coordinate.X,
                    DeliveryDistance = d.DeliveryDistance,
                    ScheduledTime = d.ScheduledTime,
                    OrderPlaced = d.OrderPlaced,
                    OrderFulfilled = d.OrderFulfilled,
                    CustomerId = d.CustomerId,
                    IsOrderFulfilled = d.IsOrderFulfilled,
                    DeliveryStatus = d.DeliveryStatus,
                    Fare = d.Fare
                })
                .ToListAsync();
        }

        // GET: api/Deliveries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DeliveryDTO>> GetDelivery(int id)
        {
            var delivery = await _context.Deliveries.FindAsync(id);

            if (delivery == null)
            {
                return NotFound();
            }

            var deliveryDto = new DeliveryDTO
            {
                DeliveryId = delivery.DeliveryId,
                OrderId = delivery.OrderId,
                PickupLatitude = delivery.PickupLocation.Coordinate.Y,
                PickupLongitude = delivery.PickupLocation.Coordinate.X,
                DeliveryLatitude = delivery.DeliveryLocation.Coordinate.Y,
                DeliveryLongitude = delivery.DeliveryLocation.Coordinate.X,
                DeliveryDistance = delivery.DeliveryDistance,
                ScheduledTime = delivery.ScheduledTime,
                OrderPlaced = delivery.OrderPlaced,
                OrderFulfilled = delivery.OrderFulfilled,
                CustomerId = delivery.CustomerId,
                IsOrderFulfilled = delivery.IsOrderFulfilled,
                DeliveryStatus = delivery.DeliveryStatus,
                Fare = delivery.Fare
            };

            return deliveryDto;
        }

        // PUT: api/Deliveries/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDelivery(int id, DeliveryDTO deliveryDto)
        {
            if (id != deliveryDto.DeliveryId)
            {
                return BadRequest();
            }

            var delivery = await _context.Deliveries.FindAsync(id);
            if (delivery == null)
            {
                return NotFound();
            }

            delivery.OrderId = deliveryDto.OrderId;
            delivery.PickupLocation = new Point(deliveryDto.PickupLongitude, deliveryDto.PickupLatitude) { SRID = 4326 };
            delivery.DeliveryLocation = new Point(deliveryDto.DeliveryLongitude, deliveryDto.DeliveryLatitude) { SRID = 4326 };
            delivery.DeliveryDistance = deliveryDto.DeliveryDistance;
            delivery.ScheduledTime = deliveryDto.ScheduledTime;
            delivery.OrderPlaced = deliveryDto.OrderPlaced;
            delivery.OrderFulfilled = deliveryDto.OrderFulfilled;
            delivery.CustomerId = deliveryDto.CustomerId;
            delivery.IsOrderFulfilled = deliveryDto.IsOrderFulfilled;
            delivery.DeliveryStatus = deliveryDto.DeliveryStatus;
            delivery.Fare = deliveryDto.Fare;

            _context.Entry(delivery).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeliveryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Deliveries
        [HttpPost]
        public async Task<ActionResult<DeliveryDTO>> PostDelivery(DeliveryDTO deliveryDto)
        {
            var pickupLocation = new Point(deliveryDto.PickupLongitude, deliveryDto.PickupLatitude) { SRID = 4326 };
            var deliveryLocation = new Point(deliveryDto.DeliveryLongitude, deliveryDto.DeliveryLatitude) { SRID = 4326 };

            var delivery = new Delivery
            {
                OrderId = deliveryDto.OrderId,
                PickupLocation = pickupLocation,
                DeliveryLocation = deliveryLocation,
                DeliveryDistance = deliveryDto.DeliveryDistance,
                ScheduledTime = deliveryDto.ScheduledTime,
                OrderPlaced = deliveryDto.OrderPlaced,
                CustomerId = deliveryDto.CustomerId,
                IsOrderFulfilled = deliveryDto.IsOrderFulfilled,
                DeliveryStatus = deliveryDto.DeliveryStatus,
                Fare = deliveryDto.Fare
            };

            _context.Deliveries.Add(delivery);
            await _context.SaveChangesAsync();

            deliveryDto.DeliveryId = delivery.DeliveryId;
            return CreatedAtAction(nameof(GetDelivery), new { id = delivery.DeliveryId }, deliveryDto);
        }

        // DELETE: api/Deliveries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDelivery(int id)
        {
            var delivery = await _context.Deliveries.FindAsync(id);
            if (delivery == null)
            {
                return NotFound();
            }

            _context.Deliveries.Remove(delivery);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DeliveryExists(int id)
        {
            return _context.Deliveries.Any(e => e.DeliveryId == id);
        }
    }
}
