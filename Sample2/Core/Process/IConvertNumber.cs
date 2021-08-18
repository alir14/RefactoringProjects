using Core.DBLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Process
{
    public interface IConvertNumber
    {
        string ProcessPart(char number);
    }

    public class ProcessMillionsPart : IConvertNumber
    {
        public string ProcessPart(char number)
        {
            throw new NotImplementedException();
        }
    }

    public class ProcessThousandsPart : IConvertNumber
    {
        public string ProcessPart(char number)
        {
            throw new NotImplementedException();
        }
    }

    public class ProcessHundredsPart : IConvertNumber
    {
        public string ProcessPart(char number)
        {
            throw new NotImplementedException();
        }
    }

    public class ProcessHundred : IConvertNumber
    {
        public string ProcessPart(char number)
        {
            throw new NotImplementedException();
        }
    }

    public class ProcessTen : IConvertNumber
    {
        //INumberDataSet _numberDataSet;

        //public ProcessTen(INumberDataSet numberDataSet)
        //{
        //    _numberDataSet = numberDataSet;
        //}
        //public string ProcessPart(char number)
        //{
        //    if (sequence[i + j] == '1')
        //    {
        //        oneStr = "";
        //        string value = string.Format("{0}{1}", sequence[i + j], sequence[i]);

        //        return _numberDataSet.GetTensNumberDataSet(double.Parse(value));
        //    }
        //    else
        //        return _numberDataSet.GetTensNumberDataSet(Char.GetNumericValue(number));
        //}
        public string ProcessPart(char number)
        {
            throw new NotImplementedException();
        }
    }

    public class ProcessOne : IConvertNumber
    {
        INumberDataSet _numberDataSet;

        public ProcessOne(INumberDataSet numberDataSet)
        {
            _numberDataSet = numberDataSet;
        }

        public string ProcessPart(char number)
        {
            return _numberDataSet.GetBaseNumberDataSet(Char.GetNumericValue(number));
        }
    }
}
