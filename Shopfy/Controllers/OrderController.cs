using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopfy.Models;
using Shopfy.Models.Interfaces;
using Shopfy.ViewModels.Dtos;

namespace Shopfy.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<OrderController> _logger;
        private readonly IMapper _mapper;
        public OrderController(
            ILogger<OrderController> logger,
            IMapper mapper,
            IOrderRepository orderRepository
            )
        {
            _logger = logger;
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public IActionResult GetAllOrders()
        {
            try
            {
                var orders = _orderRepository.GetAll();
                if(orders.Count() == 0)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(orders);
                }
            }catch(Exception ex)
            {
                _logger.LogError("Not found any orders : {ex}", ex);
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("order/{orderId}")]
        public IActionResult GetOrderById(Guid orderId)
        {
            try
            {
                var order = _orderRepository.GetOrderById(orderId);
                if(order!= null)
                {
                    return Ok(order);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError("Not found any orders : {ex}", ex);
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("order/user/{userId}")]
        public IActionResult GetOrdersByUser(Guid userId)
        {
            try
            {
                var orders = _orderRepository.GetOrderByCustomer(userId);
                if (orders != null)
                {
                    return Ok(orders);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError("Not found any orders : {ex}", ex);
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost]
        public IActionResult CreateOrder(OrderDto order) 
        {
            try
            {
                if(order is null)
                {
                    return BadRequest("order object is null !");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("invald order request ! ");
                }
                var OrderMap = _mapper.Map<Order>(order);
                var createdOrder = _orderRepository.CreateOrder(OrderMap);
                return Ok(createdOrder);

            }
            catch (Exception ex)
            {
                _logger.LogError("can not create order  : {ex}", ex);
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut]
        public IActionResult EditOrder(Order order) 
        {
            try
            {
                if (order is null)
                {
                    _logger.LogError("Invalid order sent from client ! ");
                    return BadRequest("Invalid order object !");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid object sent from client ! ");

                }
                var updatedOrder= _orderRepository.UpdateOrder(order);
                return Accepted(updatedOrder);
            }
            catch (Exception ex)
            {
                _logger.LogError("can not Update order  : {ex}", ex);
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete]
        public IActionResult DeleteOrder(Guid orderId)
        {
            try
            {
                var order = _orderRepository.GetOrderById(orderId);
                if(order == null)
                {
                    return NotFound($"order with id : {orderId} , hasn't been found in db !");
                }
                return Accepted("order Delete Successfully" !);
            }
            catch (Exception ex)
            {
                _logger.LogError("can not Delete order  : {ex}", ex);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
