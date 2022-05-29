using Microsoft.AspNetCore.Mvc;

namespace Shoppe.Api.Controllers
{
    [Route("")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// A custom error handling route to log all unhandled exceptions. This is hooked to the 
        /// builtin exception handler. See UseExceptionHandler in <see cref="Program"/>.
        /// </summary>
        /// <returns></returns>
        [Route("/error")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult HandleError()
        {
            var problem = Problem();
            _logger.LogError("An unhandled exception occured", problem);
            return problem;
        }
    }
}
