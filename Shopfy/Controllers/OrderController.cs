using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopfy.Models.Interfaces;

namespace Shopfy.Controllers
{
    [Route("api/order")]
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
    }
}
