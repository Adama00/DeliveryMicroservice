using BackEnd.DTOs;
using BackEnd.Models;

namespace BackEnd.Service.Interface
{
    public interface IDeliveryMicroservice
    {
        public Task<ApiResponse<List<DeliveryDTO>>> GetDeliveries();
        public Task<ApiResponse<DeliveryDTO>> PutDelivery(int id, DeliveryDTO deliveryDto);
        public Task<ApiResponse<DeliveryDTO>> PostDelivery(DeliveryDTO deliveryDto);
        public Task<ApiResponse<DeliveryDTO>> DeleteDelivery(int id);
        public Task<ApiResponse<DeliveryDTO>> GetDelivery(int id);
       
    }
}
