using ChequeWriterFramework;
using Core.DBLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Process
{
    public class ProcessNumberToString
    {
        public string Number { get; set; }
        INumberDataSet numberDataSet;

        public ProcessNumberToString(INumberDataSet dataSet)
        {
            numberDataSet = dataSet;
        }

        public string ConvertNumberToStringProcess(string number)
        {
            this.Number = number;

            string result = "", oneStr = "", tensStr = "", hunderedsStr = "", thousandsStr = "", milionStr = "";
            try
            {
                if (Validataion())
                {
                    if (this.Number.Contains("."))
                    {
                        var amountValue = this.Number.Split('.');

                        var dollar = string.Empty;
                        this.Number = amountValue[0];

                        var sequence = this.Number.ToCharArray().Reverse().ToArray();

                        for (int i = 0; i < sequence.Length; i += 3)
                        {
                            oneStr = ""; tensStr = ""; hunderedsStr = "";

                            switch (i)
                            {
                                case (int)NumberGroupTypeEnums.Millions:
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
                                        milionStr = string.Format("{0} Hundred {1} {2} Million", hunderedsStr, tensStr, oneStr);
                                    else if (!string.IsNullOrEmpty(tensStr))
                                        milionStr = string.Format("{0} {1} Million", tensStr, oneStr);
                                    else if (!string.IsNullOrEmpty(oneStr))
                                        milionStr = string.Format("{0} Million", oneStr);
                                    break;
                                case (int)NumberGroupTypeEnums.Thousands:
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
                                        thousandsStr = string.Format("{0} Hundred {1} {2} Thousand", hunderedsStr, tensStr, oneStr);
                                    else if (!string.IsNullOrEmpty(tensStr))
                                        thousandsStr = string.Format("{0} {1} Thousand", tensStr, oneStr);
                                    else if (!string.IsNullOrEmpty(oneStr))
                                        thousandsStr = string.Format("{0} Thousand", oneStr);

                                    break;
                                default:
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
                                        result = string.Format("{0} Hundred {1} {2}", hunderedsStr, tensStr, oneStr);
                                    else if (!string.IsNullOrEmpty(tensStr))
                                        result = string.Format("{0} {1}", tensStr, oneStr);
                                    else if (!string.IsNullOrEmpty(oneStr))
                                        result = string.Format("{0}", oneStr);
                                    break;
                            }

                        }

                        result = string.Format("{0} {1} {2}", milionStr, thousandsStr, result);

                        if (!string.IsNullOrWhiteSpace(result))
                            dollar = string.Format("{0} {1}", result, Consts.DOLLARS);

                        // calculate cent
                        var cent = string.Empty;
                        this.Number = amountValue[1];

                        if (this.Number.Length > 2)
                            this.Number = this.Number.Substring(0, 2);

                        sequence = this.Number.ToCharArray().Reverse().ToArray();

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
                                        string value = string.Format("{0}{1}", sequence[0], sequence[1]);

                                        tensStr = numberDataSet.GetTensNumberDataSet(double.Parse(value));
                                    }
                                    else
                                        tensStr = numberDataSet.GetTensNumberDataSet(Char.GetNumericValue(sequence[(i)]));
                                    break;
                                default:
                                    break;
                            }
                        }

                        result = string.Format("{0} {1}", tensStr, oneStr);

                        if (!string.IsNullOrWhiteSpace(result))
                            cent = string.Format("{0} {1}", result.Trim(), Consts.CENTS);

                        result = string.Format("{0} AND {1}", dollar, cent);
                    }
                    else
                    {
                        var dollar = string.Empty;

                        var sequence = this.Number.ToCharArray().Reverse().ToArray();

                        for (int i = 0; i < sequence.Length; i += 3)
                        {
                            oneStr = ""; tensStr = ""; hunderedsStr = "";
                            switch (i)
                            {
                                case (int)NumberGroupTypeEnums.Millions:
                                    for (int j = 0; j < 3; j++)
                                    {
                                        if ((i + j) < sequence.Length)
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
                                        milionStr = string.Format("{0} Hundred {1} {2} Million", hunderedsStr.Trim(), tensStr.Trim(), oneStr.Trim());
                                    else if (!string.IsNullOrEmpty(tensStr))
                                        milionStr = string.Format("{0} {1} Million", tensStr.Trim(), oneStr.Trim());
                                    else if (!string.IsNullOrEmpty(oneStr))
                                        milionStr = string.Format("{0} Million", oneStr.Trim());
                                    break;
                                case (int)NumberGroupTypeEnums.Thousands:
                                    for (int j = 0; j < 3; j++)
                                    {
                                        if ((i + j) < sequence.Length)
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
                                        thousandsStr = string.Format("{0} Hundred {1} {2} Thousand", hunderedsStr, tensStr, oneStr);
                                    else if (!string.IsNullOrEmpty(tensStr))
                                        thousandsStr = string.Format("{0} {1} Thousand", tensStr, oneStr);
                                    else if (!string.IsNullOrEmpty(oneStr))
                                        thousandsStr = string.Format("{0} Thousand", oneStr);

                                    break;
                                default:
                                    for (int j = 0; j < 3; j++)
                                    {
                                        if ((i + j) < sequence.Length)
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
                                        result = string.Format("{0} Hundred {1} {2}", hunderedsStr, tensStr, oneStr);
                                    else if (!string.IsNullOrEmpty(tensStr))
                                        result = string.Format("{0} {1}", tensStr, oneStr);
                                    else if (!string.IsNullOrEmpty(oneStr))
                                        result = string.Format("{0}", oneStr);
                                    break;
                            }

                        }

                        result = string.Format("{0} {1} {2}", milionStr.Trim(), thousandsStr.Trim(), result.Trim());

                        if (!string.IsNullOrWhiteSpace(result))
                            dollar = string.Format("{0} {1}", result.Trim(), Consts.DOLLARS);
                    }
                }
                else
                {
                    result = Consts.VALIDATION_ERROR_MESSAGE;
                }
            }
            catch
            {
                result = Consts.EXCEPTION_MESSAGE;
            }
            return result;
        }

        private bool Validataion()
        {
            bool result = false;

            if (!string.IsNullOrEmpty(this.Number) && decimal.TryParse(this.Number, out decimal value))
            {
                result = true;
            }

            return result;
        }
    }
}
