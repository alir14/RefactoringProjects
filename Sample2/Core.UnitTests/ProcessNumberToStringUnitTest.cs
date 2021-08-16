using Core.DBLayer;
using Core.Process;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.UnitTests
{
    [TestClass]
    public class ProcessNumberToStringUnitTest
    {
        private INumberDataSet _numberDataSet;

        [TestInitialize]
        public void Initialize()
        {
            _numberDataSet = new NumberDataSet();
        }

        [TestMethod]
        public void ConvertNumberToStringProcess_Successful_ReturnedStringValue()
        {
            var core = new ProcessNumberToString(_numberDataSet);

            var result = core.ConvertNumberToStringProcess("0.11").ToLowerInvariant().Trim();
            Assert.AreEqual(result, "eleven cents");

            result = core.ConvertNumberToStringProcess("7").ToLowerInvariant().Trim();
            Assert.AreEqual(result, "seven dollars");

            result = core.ConvertNumberToStringProcess("6.44").ToLowerInvariant().Trim();
            Assert.AreEqual(result, "six dollars and fourty four cents");

            result = core.ConvertNumberToStringProcess("17.12").ToLowerInvariant().Trim();
            Assert.AreEqual(result, "seventeen dollars and twelve cents");

            result = core.ConvertNumberToStringProcess("16.44").ToLowerInvariant().Trim();
            Assert.AreEqual(result, "sixteen dollars and fourty four cents");

            result = core.ConvertNumberToStringProcess("26.44").ToLowerInvariant().Trim();
            Assert.AreEqual(result, "twenty six dollars and fourty four cents");

            result = core.ConvertNumberToStringProcess("260.44").ToLowerInvariant().Trim();
            Assert.AreEqual(result, "two hundred sixty dollars and fourty four cents");

            result = core.ConvertNumberToStringProcess("2610.44").ToLowerInvariant().Trim();
            Assert.AreEqual(result, "two thousand six hundred ten dollars and fourty four cents");

            result = core.ConvertNumberToStringProcess("52615.44").ToLowerInvariant().Trim();
            Assert.AreEqual(result, "fifty two thousand six hundred fifteen dollars and fourty four cents");

            result = core.ConvertNumberToStringProcess("5520610.44").ToLowerInvariant().Trim();
            Assert.AreEqual(result, "five million five hundred twenty thousand six hundred ten dollars and fourty four cents");
        }
    }
}
