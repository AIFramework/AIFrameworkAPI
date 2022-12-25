using AI.DataPrepaire.NLPUtils.TextClassification;
using MainLogic.ML.Models.Classifiers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MainLogic.ML.Models.Classifiers
{
    /// <summary>
    /// Апи для классификатора текстов на правилах
    /// </summary>
    [Serializable]
    public class TextRuleClassifierAPI : ITextCL
    {
        TextRuleClassifier textRuleClassifier;

        /// <summary>
        /// Создать классификатор
        /// </summary>
        /// <param name="coc"></param>
        /// <param name="top_p"></param>
        /// <param name="max_n"></param>
        public string Create(string data) 
        {
            DataOfCreateTextRuleCl dataOfCreateTextRuleCl = 
                Newtonsoft.Json.JsonConvert.DeserializeObject<DataOfCreateTextRuleCl>(data)!;

            textRuleClassifier = new TextRuleClassifier(
                dataOfCreateTextRuleCl!.COC,
                dataOfCreateTextRuleCl.Top_p,
                dataOfCreateTextRuleCl.Max_n);

            return "Классификатор текста инициализирован. \n\nОбучите его перед использованием!";
        }

        /// <summary>
        /// Классификация
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string Predict(string data)
        {
            try
            {
                return textRuleClassifier.Predict(data) + "";
            }
            catch (Exception e)
            {
                return "Ошибка: "+e.Message;
            }
        }
    }

    public class DataOfCreateTextRuleCl 
    {
        public int COC { get; set; }
        public double Top_p { get; set; }
        public int Max_n { get; set; }
    }
}
