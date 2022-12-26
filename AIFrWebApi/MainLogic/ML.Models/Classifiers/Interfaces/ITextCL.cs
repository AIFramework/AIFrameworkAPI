using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainLogic.ML.Models.Classifiers.Interfaces
{
    public interface ITextCL
    {
        string Predict(string text);
        string Create(string json);

        string Status();
    }
}
