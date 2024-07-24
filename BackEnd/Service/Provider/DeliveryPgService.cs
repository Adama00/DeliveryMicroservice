using AutoMapper;
using BackEnd.Data;
using BackEnd.DTOs;
using BackEnd.Models;
using BackEnd.Service.Interface;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using System.Net;

namespace BackEnd.Service.Provider
{
    public class DeliveryPgService : IDeliveryMicroservice
    {
        private readonly DeliveryDbContext _context;
        private readonly IMapper _mapper;
        public DeliveryPgService(DeliveryDbContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }
        //Delete
        public async Task<ApiResponse<DeliveryDTO>> DeleteDelivery(int id)
        {
            var delivery = _context.Deliveries.FirstOrDefault(x => x.DeliveryId == id);
            try
            {
                if (delivery == null)
                {
                    return new ApiResponse<DeliveryDTO>
                    {
                        Code = $"{(int)HttpStatusCode.NotFound}",
                        Message = "Record Not found!"

                    };

                }
                
                _context.Deliveries.Remove(delivery);
                await _context.SaveChangesAsync();
                return new ApiResponse<DeliveryDTO>
                {
                    Code = $"{(int)HttpStatusCode.Accepted}",
                    Message = $"Record Deleted at id: {delivery.DeliveryId}",
                    

                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<DeliveryDTO>
                {
                    Code = $"{(int)HttpStatusCode.InternalServerError}",
                    Message = $"Error getting snippet record, {ex.Message}"

                };

            }
        }
        //Get
        public async Task<ApiResponse<List<DeliveryDTO>>> GetDeliveries()
        {
            var deliveries = _context.Deliveries.Select(d => new DeliveryDTO
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
                .ToList();
            try
            {
                if (deliveries == null)
                {
                    return new ApiResponse<List<DeliveryDTO>>
                    {

                        Code = $"{(int)HttpStatusCode.NotFound}",
                        Message = "No Records Found!"
                    };
                }

                await Task.CompletedTask;
               
                return new ApiResponse<List<DeliveryDTO>>
                {
                    Code = $"{(int)HttpStatusCode.OK}",
                    Message = "Records found!",
                    Data = deliveries
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<DeliveryDTO>>
                {
                    Code = $"{(int)HttpStatusCode.InternalServerError}",
                    Message = $"Error getting delivery records, {ex.Message}"


                };

            }
        }
        //Get{id}

        public async Task<ApiResponse<DeliveryDTO>> GetDelivery(int id)
        {
            var d = await _context.Deliveries.FindAsync(id);

            try
            {
                if (d == null)
                {
                    return new ApiResponse<DeliveryDTO>
                    {
                        Code = $"{(int)HttpStatusCode.NotFound}",
                        Message = "Record Not found!"

                    };
                }
                await Task.CompletedTask;
                var deliveryDto = new DeliveryDTO
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
                };
                return new ApiResponse<DeliveryDTO>
                {
                    Code = $"{(int)HttpStatusCode.OK}",
                    Message = "Record found!",
                    Data = deliveryDto
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<DeliveryDTO>
                {
                    Code = $"{(int)HttpStatusCode.InternalServerError}",
                    Message = $"Error getting athlete record, {ex.Message}"

                };
            }
        }
        //Post
        public async Task<ApiResponse<DeliveryDTO>> PostDelivery(DeliveryDTO deliveryDto)
        {
            try {
                if (deliveryDto == null)
                {
                    return new ApiResponse<DeliveryDTO>
                    {
                        Code = $"{(int)HttpStatusCode.BadRequest}",
                        Message = "Invalid Request, Null Body!"
                    };
                }
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
                return new ApiResponse<DeliveryDTO>
                {
                    Code = $"{(int)HttpStatusCode.OK}",
                    Message = "Created Delivery Record!"
                };
        } catch (Exception ex) {
                return new ApiResponse<DeliveryDTO>
                {
                    Code = $"{(int)HttpStatusCode.InternalServerError}",
                    Message = $"Error adding Delivery Record, {ex.Message}"

                };
            } 
        }
        //Put
        public async Task<ApiResponse<DeliveryDTO>> PutDelivery(int id, DeliveryDTO deliveryDto)
        {
            if (id != deliveryDto.DeliveryId)
            {
                return new ApiResponse<DeliveryDTO>
                {
                    Code = $"{(int)HttpStatusCode.BadRequest}",
                    Message = "Invalid delivery body"
                };
            }

            var delivery = await _context.Deliveries.FindAsync(id);
            if (delivery == null)
            {
                return new ApiResponse<DeliveryDTO>
                {
                    Code = $"{(int)HttpStatusCode.BadRequest}",
                    Message = "Invalid delivery body"
                };
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
                    return new ApiResponse<DeliveryDTO>
                    {
                        Code = $"{(int)HttpStatusCode.NotFound}",
                        Message = "Record Not found"
                    };
                }
                else
                {
                    throw;
                }
            }

            return new ApiResponse<DeliveryDTO>
            {
                Code = $"{(int)HttpStatusCode.OK}",
                Message = "Record Deleted successfully"
            };
        }
        private bool DeliveryExists(int id)
        {
            return _context.Deliveries.Any(e => e.DeliveryId == id);
        }
    }
}
