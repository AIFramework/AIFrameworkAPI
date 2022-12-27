using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainLogic.ML.Models.Classifiers.Interfaces
{
    public interface ITextCL
    {
        int CountRules { get; }
        string Predict(string text);
        string Create(string json);
        string Status();
        string Train(Stream streamCSV);
        string SetCSVSchem(string json);
        string GetCSVSchem();
    }
}
