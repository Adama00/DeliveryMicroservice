using BackEnd.Data;
using BackEnd.DTOs;
using BackEnd.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveriesServiceController : Controller
    {
        private readonly IDeliveryMicroservice _service;
        public DeliveriesServiceController(IDeliveryMicroservice service) {
            _service = service; 
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var yeti = await _service.GetDeliveries();
            return StatusCode(int.Parse(yeti.Code),yeti);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var yeti = await _service.GetDelivery(id);
            return StatusCode(int.Parse(yeti.Code),yeti);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]DeliveryDTO deliveryDto)
        {
            var yeti = await _service.PutDelivery(id, deliveryDto);
            return StatusCode(int.Parse(yeti.Code), yeti);  
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DeliveryDTO deliveryDto)
        {
            var yeti = await _service.PostDelivery(deliveryDto);
            return StatusCode(int.Parse(yeti.Code), yeti);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var yeti = await _service.DeleteDelivery(id);
            return StatusCode(int.Parse(yeti.Code), yeti);
        }
    }
}
