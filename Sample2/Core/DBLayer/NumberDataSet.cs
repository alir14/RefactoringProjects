using System.Collections.Generic;

namespace Core.DBLayer
{
    public class NumberDataSet : INumberDataSet
    {
        Dictionary<int, string> BaseDataSet { get; set; }
        Dictionary<int, string> TensDataSet { get; set; }

        public NumberDataSet()
        {
            this.BaseDataSet = new Dictionary<int, string>()
            {
                {0,"" },
                {1, "one" },
                {2, "two" },
                {3, "three" },
                {4, "four" },
                {5, "five" },
                {6, "six" },
                {7, "seven" },
                {8, "eight" },
                {9, "nine" }
            };

            this.TensDataSet = new Dictionary<int, string>()
            {
                {0,"" },
                {2, "twenty" },
                {3, "therty" },
                {4, "fourty" },
                {5, "fifty" },
                {6, "sixty" },
                {7, "seventyty" },
                {8, "eighty" },
                {9, "ninty" },
                {10, "ten" },
                {11, "eleven" },
                {12, "twelve" },
                {13, "therteen" },
                {14, "fourteen" },
                {15, "fifteen" },
                {16, "sixteen" },
                {17, "seventeen" },
                {18, "eighteen" },
                {19, "nineteen" }
            };
        }

        public string GetBaseNumberDataSet(double index)
        {
            return this.BaseDataSet[(int)index];
        }

        public string GetTensNumberDataSet(double index)
        {
            return this.TensDataSet[(int)index];
        }
    }
}
