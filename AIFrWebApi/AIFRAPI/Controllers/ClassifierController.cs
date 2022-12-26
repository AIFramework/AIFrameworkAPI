using MainLogic.ML.Models.Classifiers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AIFRAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClassifierController : ControllerBase
    {
        private readonly ITextCL _textCL;
        private readonly ILogger<ClassifierController> _logger;

        public ClassifierController(ILogger<ClassifierController> logger, ITextCL textCL)
        {
            _textCL = textCL;
            _logger = logger;
        }

        [HttpGet()]
        [Route("predict")]
        public IActionResult Predict(string json) =>
            Ok(_textCL.Predict(json));

        [HttpGet()]
        [Route("status")]
        public IActionResult Status() =>
            Ok(_textCL.Status());

        [HttpGet()]
        [Route("create")]
        public IActionResult Create(string json) =>
            Ok(_textCL.Create(json));

        [HttpPost()]
        [Route("upload")]
        public IActionResult Upload(IFormFile file) =>
            throw new NotImplementedException();
    }
}