using BHT.Core.Readers.CoBa;
using BTH.Core;
using NUnit.Framework;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BTH.Tests
{
    public class CoBaReaderTests : TestBase
    {
        [SetUp]
        public new void Setup()
        {
            base.Setup();
            Ioc.Install();
        }

        [Test]
        public async Task ReadCsvTest()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "testdata\\data.csv");

            var reader = Ioc.Resolve<ICoBaReader>();
            var result = await reader.ParseCsvFileAsync(filePath);

            Assert.NotNull(result);
            Assert.AreEqual(7, result.Count());
            Assert.IsTrue(result.All(e => e.IBAN.Equals("DE64642525252525646425")));
            Assert.IsTrue(result.All(e => e.BIC.Equals("25252525")));
            Assert.IsTrue(result.All(e => e.ClientAccount.Equals("123456700")));
        }
    }
}