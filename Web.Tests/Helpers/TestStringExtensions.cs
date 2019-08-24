namespace t3hmun.WebLog.Web.Tests.Helpers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using t3hmun.WebLog.Web.Helpers;

    [TestClass]
    public class TestStringExtensions
    {
        [DataTestMethod]
        [DataRow("Word", "Word")]
        [DataRow("W0rd", "W0rd")]
        [DataRow("TwoWords", "Two Words")]
        [DataRow("TwoW0rds", "Two W0rds")]
        [DataRow("NowThreeWords", "Now Three Words")]
        public void CamelSpace_SimpleCamelHumps(string sut, string expected)
        {
            var actual = sut.CamelSpace();
            Assert.AreEqual(expected, actual, false);
        }

        [DataTestMethod]
        [DataRow("A", "A")]
        [DataRow("AB", "AB")]
        [DataRow("AWord", "A Word")]
        [DataRow("AW0rd", "A W0rd")]
        [DataRow("ABWord", "AB Word")]
        [DataRow("ABW0rd", "AB W0rd")]
        [DataRow("MidAWord", "Mid A Word")]
        [DataRow("MidABWord", "Mid AB Word")]
        [DataRow("M1dABW0rd", "M1d AB W0rd")]
        [DataRow("EndA", "End A")]
        [DataRow("End2A", "End2 A")]
        [DataRow("EndAB", "End AB")]
        [DataRow("End2AB", "End2 AB")]
        public void CamelSpace_ShortAcronyms(string sut, string expected)
        {
            var actual = sut.CamelSpace();
            Assert.AreEqual(expected, actual, false);
        }

        [DataTestMethod]
        [DataRow("ACRONYM", "ACRONYM")]
        [DataRow("EndACRONYM", "End ACRONYM")]
        [DataRow("MidACRONYMMid", "Mid ACRONYM Mid")]
        [DataRow("ACRONYMStart", "ACRONYM Start")]
        public void CamelSpace_LongAcronyms(string sut, string expected)
        {
            var actual = sut.CamelSpace();
            Assert.AreEqual(expected, actual, false);
        }

        [DataTestMethod]
        [DataRow("A", "A")]
        [DataRow("", "")]
        [DataRow(" ", " ")]
        [DataRow("  ", "  ")]
        [DataRow(" a ", " a ")]
        [DataRow(" A ", " A ")]
        public void CamelSpace_OddStringEdgeCases(string sut, string expected)
        {
            var actual = sut.CamelSpace();
            Assert.AreEqual(expected, actual, false);
        }

        [DataRow("Class.Method", "Class.Method")]
        [DataRow(".Net", ".Net")]
        [DataRow(".net", ".net")]
        [DataRow("end.", "end.")]
        [DataRow("enD.", "en D.")]
        public void CamelSpace_Punctuation(string sut, string expected)
        {
            var actual = sut.CamelSpace();
            Assert.AreEqual(expected, actual, false);
        }
    }
}