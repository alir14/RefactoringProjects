using System;
using System.Linq;
using Core.DBLayer;
using Core.Factory;
using ChequeWriterFramework;
using System.Collections.Generic;

namespace Core.Process.Parts
{
    public class ProcessDollarPart
    {
        private readonly ConverterFactory _numberConverterFactory;

        public ProcessDollarPart(INumberDataSet dataSet)
        {
            _numberConverterFactory = new NumberConverterFactory(dataSet);
        }

        public string ConvertDollar(string Number)
        {
            var value = string.Empty;
            List<string> parts = new List<string>();

            var sequence = Number.ToCharArray().Reverse().ToArray();

            for (int i = 0; i < sequence.Length; i += 3)
            {
                var converter = _numberConverterFactory.GetNumberConverter(i);

                parts.Add(converter.ProcessPart(i, sequence));
            }

            for (int i = parts.Count; i > 0; i--)
            {
                value += string.Format("{0} ", parts[i - 1]);
            }

            if (!string.IsNullOrWhiteSpace(value))
                return string.Format("{0} {1}", value.Trim(), Consts.DOLLARS);

            return string.Empty;
        }
    }
}
