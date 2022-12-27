using MainLogic.ML.Models.Classifiers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AIFRAPI.Controllers
{
    /// <summary>
    /// Классификатор текста на базе правил
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RuleTextClassifierController : ControllerBase
    {
        private readonly ITextCL _textCL;
        private readonly ILogger<RuleTextClassifierController> _logger;

        /// <summary>
        /// Классификатор текста на базе правил
        /// </summary>
        public RuleTextClassifierController(ILogger<RuleTextClassifierController> logger, ITextCL textCL)
        {
            _textCL = textCL;
            _logger = logger;
        }

        /// <summary>
        /// Классификация
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [HttpGet()]
        [Route("predict")]
        public IActionResult Predict(string json) =>
            Ok(_textCL.Predict(json));

        /// <summary>
        /// Установить схему csv-документа
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [HttpGet()]
        [Route("set_csv_scheme")]
        public IActionResult SetCSVSchem(string json) =>
             Ok(_textCL.SetCSVSchem(json));

        /// <summary>
        /// Создать классификатор
        /// </summary>
        /// <param name="json">Пример: 1 класс, покрытие по важности 90%, Максимальная длинна n-граммы: 1
        /// 
        /// {"CountOfClasses":1,"Top_p":0.9,"Max_n":1}</param>
        /// <returns></returns>
        [HttpGet()]
        [Route("create")]
        public IActionResult Create(string json) =>
        Ok(_textCL.Create(json));

        /// <summary>
        /// Статус классификатора
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [Route("status")]
        public IActionResult Status() =>
            Ok(_textCL.Status());

        /// <summary>
        /// Получить схему csv
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [Route("get_csv_schem")]
        public IActionResult GetCSVSchem() =>
          Ok(_textCL.GetCSVSchem());

        /// <summary>
        /// Число выученных правил
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [Route("count_rules")]
        public IActionResult CountRules() =>
            Ok(_textCL.CountRules);

    
        /// <summary>
        /// Обучение классификатора
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost()]
        [Route("train")]
        public IActionResult Train(IFormFile file) =>
            Ok(_textCL.Train(file.OpenReadStream()));



    }
}