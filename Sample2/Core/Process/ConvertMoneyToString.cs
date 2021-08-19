using Core.DBLayer;
using Core.Process.Parts;
using ChequeWriterFramework;

namespace Core.Process
{
    public class ConvertMoneyToString
    {
        private readonly ProcessCentsPart _processCentsPart;
        private readonly ProcessDollarPart _processDoallar;

        public ConvertMoneyToString(INumberDataSet dataSet)
        {
            _processCentsPart = new ProcessCentsPart(dataSet);
            _processDoallar = new ProcessDollarPart(dataSet);
        }

        public string ConvertMoneyToStringProcess(string number)
        {
            try
            {
                if (Validataion(number))
                {
                    if (number.Contains("."))
                        return ConverDallorAndCents(number);
                    else
                        return _processDoallar.ConvertDollar(number);
                }
                else
                    return Consts.VALIDATION_ERROR_MESSAGE;
            }
            catch
            {
                return Consts.EXCEPTION_MESSAGE;
            }
        }

        private string ConverDallorAndCents(string number)
        {
            var amountValue = number.Split('.');

            var dollar = _processDoallar.ConvertDollar(amountValue[0]);
            var cent = _processCentsPart.ConvertCents(amountValue[1]);

            if (!string.IsNullOrEmpty(dollar))
                return string.Format("{0} AND {1}", dollar, cent);
            else
                return cent;
        }

        private bool Validataion(string number)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(number) && decimal.TryParse(number, out decimal value))
                result = true;

            return result;
        }
    }
}
