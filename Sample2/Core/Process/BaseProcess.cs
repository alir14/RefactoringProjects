using System;
using System.Linq;
using Core.DBLayer;
using ChequeWriterFramework;

namespace Core.Process
{
    public abstract class BaseProcess
    {
        public string oneStr { get; set; }
        public string tensStr { get; set; }
        public string hunderedsStr { get; set; }

        INumberDataSet numberDataSet;

        public BaseProcess(INumberDataSet dataSet)
        {
            numberDataSet = dataSet;
        }

        protected void ProcessNumberPart(int i, char[] sequence)
        {
            for (int j = 0; j < 3; j++)
            {
                if ((i + j) < sequence.Count())
                {
                    switch (j)
                    {
                        case (int)NumberGroupTypeEnums.Ones:
                            oneStr = numberDataSet.GetBaseNumberDataSet(Char.GetNumericValue(sequence[(i + j)]));
                            break;
                        case (int)NumberGroupTypeEnums.Tens:
                            if (sequence[i + j] == '1')
                            {
                                oneStr = "";
                                string value = string.Format("{0}{1}", sequence[i + j], sequence[i]);

                                tensStr = numberDataSet.GetTensNumberDataSet(double.Parse(value));
                            }
                            else
                                tensStr = numberDataSet.GetTensNumberDataSet(Char.GetNumericValue(sequence[(i + j)]));
                            break;
                        case (int)NumberGroupTypeEnums.Hundreds:
                            hunderedsStr = numberDataSet.GetBaseNumberDataSet(Char.GetNumericValue(sequence[(i + j)]));
                            break;
                        default:
                            break;
                    }
                }
            }
        }

    }
}
