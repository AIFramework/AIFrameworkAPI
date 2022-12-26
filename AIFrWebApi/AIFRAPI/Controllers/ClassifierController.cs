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

        [HttpGet(Name = "GetPredict")]
        public IActionResult GetPredict(string text)
        {
            string[] strs = text.Split(new[] { " json: " }, StringSplitOptions.None);
            string command = strs[0];
            string json = strs.Length > 1? strs[1]:String.Empty;

            switch (command)
            {
                // predict json: Привет
                case "predict":
                    return Ok(_textCL.Predict(json));

                // create json: {COC:10, Top_p:0.9, Max_n:4}
                case "create":
                    return Ok(_textCL.Create(json));

                case "status":
                    return Ok(_textCL.Status());
            }

            return Ok("Неизвестная команда");
        }
    }
}