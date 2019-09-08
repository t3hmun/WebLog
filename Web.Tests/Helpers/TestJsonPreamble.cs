namespace t3hmun.WebLog.Web.Tests.Helpers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using t3hmun.WebLog.Web.Helpers;

    [TestClass]
    public class TestPreambleProcessor
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
            PreambleProcessor.JsonPreamble(ref sut, post);

            Assert.AreEqual(expectedDescription, post.Description);
            Assert.AreEqual(expectedBody, sut);
        }
    }
}