using System;
using System.Linq;
using Core.DBLayer;
using ChequeWriterFramework;

namespace Core.Process.Parts
{
    public class ProcessCentsPart : BaseProcess
    {
        public ProcessCentsPart(INumberDataSet dataSet)
            : base(dataSet)
        {
        }

        public string ConvertCents(string number)
        {
            if (number.Length > 2)
                number = number.Substring(0, 2);

            var sequence = number.ToCharArray().Reverse().ToArray();

            ProcessNumberPart(0, sequence);

            var result = string.Format("{0} {1}", tensStr, oneStr);

            if (!string.IsNullOrWhiteSpace(result))
                result = string.Format("{0} {1}", result.Trim(), Consts.CENTS);

            return result;
        }
    }
}
