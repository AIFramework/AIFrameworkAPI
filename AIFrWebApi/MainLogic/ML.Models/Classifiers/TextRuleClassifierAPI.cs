using AI.DataPrepaire.DataLoader.Formats;
using AI.DataPrepaire.NLPUtils.TextClassification;
using MainLogic.ML.Models.Classifiers.Interfaces;
using Newtonsoft.Json;
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
        TextRuleClassifier textRuleClassifier = new TextRuleClassifier(1,0.9,1);
        CSVSchem schem = new CSVSchem(); // Схема csv

        /// <summary>
        /// Число правил
        /// </summary>
        public int CountRules => textRuleClassifier.CountRules;

        /// <summary>
        /// Создать классификатор
        /// </summary>
        /// <param name="coc"></param>
        /// <param name="top_p"></param>
        /// <param name="max_n"></param>
        public string Create(string data) 
        {
            DataOfCreateTextRuleCl dataOfCreateTextRuleCl = 
                 JsonConvert.DeserializeObject<DataOfCreateTextRuleCl>(data)!;

            textRuleClassifier = new TextRuleClassifier(
                dataOfCreateTextRuleCl!.CountOfClasses,
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

        /// <summary>
        /// Статус классификатора
        /// </summary>
        /// <returns></returns>
        public string Status() 
        {
            DataOfCreateTextRuleCl data = new DataOfCreateTextRuleCl()
            {
                CountOfClasses = textRuleClassifier.classifier.NumCl,
                Top_p = textRuleClassifier.classifier.States2Vector.TopP,
                Max_n = textRuleClassifier.classifier.States2Vector.MaxNGramm
            };

            return Newtonsoft.Json.JsonConvert.SerializeObject(data);
        }

        /// <summary>
        /// Обучение классификатора с помощью csv
        /// </summary>
        /// <param name="streamCSV"></param>
        /// <returns></returns>
        public string Train(Stream streamCSV)
        {
            using (StreamReader reader = new StreamReader(streamCSV)) 
            {
                var csv =  CSVLoader.Read(reader, schem.Separator);
                var texts = csv[schem.DataColumn].ToType<string>();

                var trg = csv[schem.ClassColumn].Data;
                var trg_final = new int[trg.Count];

                for (int i = 0; i < trg.Count; i++) 
                    trg_final[i] = (int)trg[i] - 1;

                textRuleClassifier.Train(texts.ToArray(), trg_final);


                var true_cl = 0.0;

                for (int i = 0; i < trg_final.Length; i++)
                    if (trg_final[i] == textRuleClassifier.Predict(texts[i]))
                        true_cl++;

                return $"Точность: {Math.Round(true_cl / trg_final.Length * 100, 1)}";
            }
        }

        /// <summary>
        /// Получить схему csv
        /// </summary>
        /// <returns></returns>
        public string GetCSVSchem()
        {
            return JsonConvert.SerializeObject(schem);
        }

        /// <summary>
        /// Установить схему csv
        /// </summary>
        /// <returns></returns>
        public string SetCSVSchem(string json)
        {
            schem = JsonConvert.DeserializeObject<CSVSchem>(json)!;
            return "Схема установлена";
        }
    }

    public class DataOfCreateTextRuleCl 
    {
        public int CountOfClasses { get; set; }
        public double Top_p { get; set; }
        public int Max_n { get; set; }
    }

    /// <summary>
    /// Схема csv
    /// </summary>
    public class CSVSchem 
    {
        public string Separator { get; set; } = ",";

        public string DataColumn { get; set; } = "A";

        public string ClassColumn { get; set; } = "Class";
    }
}
