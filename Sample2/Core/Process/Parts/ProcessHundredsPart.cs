using Core.DBLayer;

namespace Core.Process.Parts
{
    public class ProcessHundredsPart : BaseProcess, IConvertNumber
    {
        public ProcessHundredsPart(INumberDataSet dataSet)
            : base(dataSet)
        {
        }

        public string ProcessPart(int i, char[] number)
        {
            ProcessNumberPart(i, number);

            if (!string.IsNullOrEmpty(hunderedsStr))
                return string.Format("{0} Hundred {1} {2}", hunderedsStr.Trim(), tensStr.Trim(), oneStr.Trim()).Trim();
            else if (!string.IsNullOrEmpty(tensStr))
                return string.Format("{0} {1}", tensStr.Trim(), oneStr.Trim()).Trim();
            else if (!string.IsNullOrEmpty(oneStr))
                return string.Format("{0}", oneStr.Trim()).Trim();
            else
                return "";
        }
    }
}
