using Microsoft.AspNetCore.Mvc;

namespace DDOSGuardService.Controllers
{
    [ApiController]
    [Route("")]
    public class BasicController(ILogger<BasicController> logger) : ControllerBase
    {
        #region Fields

        private readonly ILogger<BasicController> _logger = logger;

        #endregion

        #region Public Methods

        [HttpGet]
        public void BaseRoute(string clientId)
        {
            _logger.LogInformation($"Request received from {clientId}");
        }

        #endregion
    }
}