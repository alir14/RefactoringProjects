using Core.DBLayer;

namespace Core.Process.Parts
{
    public class ProcessMillionsPart : BaseProcess, IConvertNumber
    {
        public ProcessMillionsPart(INumberDataSet dataSet)
            : base(dataSet)
        {
        }

        public string ProcessPart(int i, char[] number)
        {
            ProcessNumberPart(i, number);

            if (!string.IsNullOrEmpty(hunderedsStr))
                return string.Format("{0} Hundred {1} {2} Million", hunderedsStr.Trim(), tensStr.Trim(), oneStr.Trim());
            else if (!string.IsNullOrEmpty(tensStr))
                return string.Format("{0} {1} Million", tensStr, oneStr.Trim());
            else if (!string.IsNullOrEmpty(oneStr))
                return string.Format("{0} Million", oneStr.Trim());
            else
                return "";
        }
    }
}
