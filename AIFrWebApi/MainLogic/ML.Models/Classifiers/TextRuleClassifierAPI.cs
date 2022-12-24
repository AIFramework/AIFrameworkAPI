using MainLogic.ML.Models.Classifiers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainLogic.ML.Models.Classifiers
{
    /// <summary>
    /// Апи для классификатора текстов на правилах
    /// </summary>
    [Serializable]
    public class TextRuleClassifierAPI : ITextCL
    {
        /// <summary>
        /// Классификация
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string Predict(string data)
        {
            return $"Метод не реализован, вход {data}";
        }
    }
}
