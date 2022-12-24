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
        }

        [HttpGet(Name = "Get")]
        public IActionResult Get(string text)
        {
            return Ok(_textCL.Predict(text));
        }
    }
}