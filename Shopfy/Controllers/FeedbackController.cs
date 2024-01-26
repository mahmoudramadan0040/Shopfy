using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shopfy.Controllers
{
    [Route("api/feedback")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        
        private readonly ILogger<FeedbackController> _logger;
        private readonly IMapper _mapper;
        public FeedbackController(
            ILogger<FeedbackController> logger,
            IMapper mapper
            )
        {
            _logger = logger;
            _mapper = mapper;
        }
    }
}
