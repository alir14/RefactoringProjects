using Core.DBLayer;

namespace Core.Process.Parts
{
    public class ProcessThousandsPart : BaseProcess, IConvertNumber
    {
        public ProcessThousandsPart(INumberDataSet dataSet)
            : base(dataSet)
        {
        }

        public string ProcessPart(int i, char[] number)
        {
            ProcessNumberPart(i, number);

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
    }
}
