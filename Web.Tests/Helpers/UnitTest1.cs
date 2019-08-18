
namespace Web.Tests.Helpers
{
    using t3hmun.WLog.Web.Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    [TestClass]
    public class UnitTest1
    {
        [DataTestMethod]
        [DataRow("Word", "Word")]
        [DataRow("TwoWords", "Two Words")]
        [DataRow("NowThreeWords", "Now Three Words")]
        [DataRow("", "")]
        [DataRow(" ", " ")]
        [DataRow("ACRONYM", "ACRONYM")]
        [DataRow("EndACRONYM", "End ACRONYM")]
        [DataRow("MidACRONYMMid", "Mid ACRONYM Mid")]
        [DataRow("ACRONYMStart", "ACRONYM Start")]
        [DataRow("A", "A")]
        [DataRow("AB", "AB")]
        [DataRow("AWord", "A Word")]
        [DataRow("ABWord", "AB Word")]
        [DataRow("MidAWord", "Mid A Word")]
        [DataRow("MidABWord", "Mid AB Word")]
        [DataRow("EndA", "End A")]
        [DataRow("EndAB", "End AB")]
        public void StringExtensions_CamelSpace(string sut, string expected)
        {
            var actual = sut.CamelSpace();
            Assert.AreEqual(expected, actual, false);
        }
    }
}
