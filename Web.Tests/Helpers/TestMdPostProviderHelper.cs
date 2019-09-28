namespace t3hmun.WebLog.Web.Tests.Helpers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using t3hmun.WebLog.Web.Helpers;

    [TestClass]
    public class TestMdPostProviderHelper
    {
        private const string SimpleDescription = "This is a dsescription.";
        private const string SimpleDescriptionJson = "{\"description\":\"" + SimpleDescription + "\"}";
        private const string SimpleBody = "#h1\n\nSomeText\n";
        private const string MultilineDescriptionJson = "{\n\"description\": \"" + SimpleDescription + "\"\n}";
        private const string SimpleDescriptionSut = SimpleDescriptionJson + "\n" + SimpleBody;
        private const string MultilineJsonSimpledDescriptionSut = MultilineDescriptionJson + "\n" + SimpleBody;

        [TestMethod]
        [DataRow(SimpleDescriptionSut, SimpleDescription, SimpleBody)]
        [DataRow(MultilineJsonSimpledDescriptionSut, SimpleDescription, SimpleBody)]
        public void TestJsonPreamble(string sut, string expectedDescription, string expectedBody)
        {
            var post = new Post();
            MdPostProviderHelper.ParseAndRemoveJsonPreamble(ref sut, post);

            Assert.AreEqual(expectedDescription, post.Description);
            Assert.AreEqual(expectedBody, sut);
        }

        [TestMethod]
        [DataRow("path/2015-10-22-some post.txt")]
        [DataRow("path/readme.md")]
        [DataRow("path/README.MD")]
        [DataRow("some post.txt")]
        [DataRow("readme.md")]
        [DataRow("README.MD")]
        public void TestValidFileNames_DoesNotMatchInValidFiles(string sut)
        {
            var actual = MdPostProvider.ValidFilenames.IsMatch(sut);
            Assert.IsFalse(actual);
        }

        [TestMethod]
        [DataRow("2015-10-22-some post.md")]
        [DataRow("2015-10-22-SOME POST.MD")]
        [DataRow("path/2015-10-22-some post.md")]
        [DataRow("path/2015-10-22-SOME POST.MD")]
        public void TestValidFileNames_MatchesValidFiles(string sut)
        {
            var actual = MdPostProvider.ValidFilenames.IsMatch(sut);
            Assert.IsTrue(actual);
        }
    }
}