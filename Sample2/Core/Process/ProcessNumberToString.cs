using System;
using System.Linq;
using Core.DBLayer;
using ChequeWriterFramework;

namespace Core.Process
{
    public class ProcessNumberToString
    {
        //public string Number { get; set; }
        INumberDataSet numberDataSet;

        public ProcessNumberToString(INumberDataSet dataSet)
        {
            numberDataSet = dataSet;
        }
        string oneStr = "", tensStr = "", hunderedsStr = ""; //, milionStr = "" , thousandsStr = ""

        public string ConvertNumberToStringProcess(string number)
        {
            try
            {
                if (Validataion(number))
                {
                    if (number.Contains("."))
                    {
                        return ConverDecimalNumber(number);
                    }
                    else
                    {
                        return convertIntegerNumber(number);
                    }
                }
                else
                {
                    return Consts.VALIDATION_ERROR_MESSAGE;
                }
            }
            catch
            {
                return Consts.EXCEPTION_MESSAGE;
            }
        }

        private string ConverDecimalNumber(string number)
        {
            var result = string.Empty;
            string milionStr = "", thousandsStr = "";

            var amountValue = number.Split('.');

            var dollar = string.Empty;
            var Number = amountValue[0];

            var sequence = Number.ToCharArray().Reverse().ToArray();

            for (int i = 0; i < sequence.Length; i += 3)
            {
                oneStr = ""; tensStr = ""; hunderedsStr = "";

                switch (i)
                {
                    case (int)NumberGroupTypeEnums.Millions:
                        milionStr = ProcessMillionPart(i, sequence);
                        break;
                    case (int)NumberGroupTypeEnums.Thousands:
                        thousandsStr = ProcessThousandsPart(i, sequence);
                        break;
                    default:
                        result = ProcessHundredPart(i, sequence);
                        break;
                }

            }

            result = string.Format("{0} {1} {2}", milionStr.Trim(), thousandsStr.Trim(), result);

            if (!string.IsNullOrWhiteSpace(result))
                dollar = string.Format("{0} {1}", result, Consts.DOLLARS);

            // calculate cent
            var cent = CalculateCents(amountValue[1]);

            if (!string.IsNullOrEmpty(dollar))
                result = string.Format("{0} AND {1}", dollar, cent);
            else
                result = cent;

            return result;
        }

        private string convertIntegerNumber(string Number)
        {
            string milionStr = "", thousandsStr = "";

            var result = string.Empty;
            var dollar = string.Empty;

            var sequence = Number.ToCharArray().Reverse().ToArray();

            for (int i = 0; i < sequence.Length; i += 3)
            {
                oneStr = ""; tensStr = ""; hunderedsStr = "";
                switch (i)
                {
                    case (int)NumberGroupTypeEnums.Millions:
                        milionStr = ProcessMillionPart(i, sequence);
                        break;
                    case (int)NumberGroupTypeEnums.Thousands:
                        thousandsStr = ProcessThousandsPart(i, sequence);
                        break;
                    default:
                        result = ProcessHundredPart(i, sequence);
                        break;
                }

            }

            result = string.Format("{0} {1} {2}", milionStr.Trim(), thousandsStr.Trim(), result.Trim());

            if (!string.IsNullOrWhiteSpace(result))
                result = string.Format("{0} {1}", result.Trim(), Consts.DOLLARS);

            return result;
        }

        private string ProcessMillionPart(int i, char[] sequence)
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
            if (!string.IsNullOrEmpty(hunderedsStr))
                return string.Format("{0} Hundred {1} {2} Million", hunderedsStr.Trim(), tensStr.Trim(), oneStr.Trim());
            else if (!string.IsNullOrEmpty(tensStr))
                return string.Format("{0} {1} Million", tensStr, oneStr.Trim());
            else if (!string.IsNullOrEmpty(oneStr))
                return string.Format("{0} Million", oneStr.Trim());
            else
                return "";
        }

        private string ProcessThousandsPart(int i, char[] sequence)
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
            if (!string.IsNullOrEmpty(hunderedsStr) && !string.IsNullOrWhiteSpace(oneStr))
                return string.Format("{0} Hundred {1} {2} Thousand", hunderedsStr.Trim(), tensStr.Trim(), oneStr.Trim());
            else if (!string.IsNullOrEmpty(hunderedsStr))
                return string.Format("{0} Hundred {1} Thousand", hunderedsStr.Trim(), tensStr.Trim());
            else if (!string.IsNullOrEmpty(tensStr))
                return string.Format("{0} {1} Thousand", tensStr.Trim(), oneStr.Trim());
            else if (!string.IsNullOrEmpty(oneStr))
                return string.Format("{0} Thousand", oneStr.Trim());
            else
                return "";
        }

        private string ProcessHundredPart(int i, char[] sequence)
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
            if (!string.IsNullOrEmpty(hunderedsStr))
                return string.Format("{0} Hundred {1} {2}", hunderedsStr.Trim(), tensStr.Trim(), oneStr.Trim()).Trim();
            else if (!string.IsNullOrEmpty(tensStr))
                return string.Format("{0} {1}", tensStr.Trim(), oneStr.Trim()).Trim();
            else if (!string.IsNullOrEmpty(oneStr))
                return string.Format("{0}", oneStr.Trim()).Trim();
            else
                return "";

        }

        private string CalculateCents(string number)
        {
            if (number.Length > 2)
                number = number.Substring(0, 2);

            var sequence = number.ToCharArray().Reverse().ToArray();

            for (int i = 0; i < sequence.Length; i++)
            {
                switch (i)
                {
                    case (int)NumberGroupTypeEnums.Ones:
                        oneStr = numberDataSet.GetBaseNumberDataSet(Char.GetNumericValue(sequence[(i)]));
                        break;
                    case (int)NumberGroupTypeEnums.Tens:
                        if (sequence[1] == '1')
                        {
                            oneStr = "";
                            string value = string.Format("{0}{1}", sequence[1], sequence[0]);

                            tensStr = numberDataSet.GetTensNumberDataSet(double.Parse(value));
                        }
                        else
                            tensStr = numberDataSet.GetTensNumberDataSet(Char.GetNumericValue(sequence[(i)]));
                        break;
                    default:
                        break;
                }
            }

            var result = string.Format("{0} {1}", tensStr, oneStr);

            if (!string.IsNullOrWhiteSpace(result))
                result = string.Format("{0} {1}", result.Trim(), Consts.CENTS);

            return result;
        }

        private bool Validataion(string number)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(number) && decimal.TryParse(number, out decimal value))
            {
                result = true;
            }

            return result;
        }
    }
}
