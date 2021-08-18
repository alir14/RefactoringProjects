using Core.DBLayer;
using Core.Process;
using Core.Process.Parts;
using ChequeWriterFramework;

namespace Core.Factory
{
    public abstract class ConverterFactory
    {
        public abstract IConvertNumber GetNumberConverter(int i);
    }

    public class NumberConverterFactory : ConverterFactory
    {
        private readonly INumberDataSet _dataSet;
        public NumberConverterFactory(INumberDataSet dataSet)
        {
            _dataSet = dataSet;
        }

        public override IConvertNumber GetNumberConverter(int i)
        {
            switch (i)
            {
                case (int)NumberGroupTypeEnums.Millions:
                    return new ProcessMillionsPart(_dataSet);
                case (int)NumberGroupTypeEnums.Thousands:
                    return new ProcessThousandsPart(_dataSet);
                default:
                    return new ProcessHundredsPart(_dataSet);
            }
        }
    }
}
