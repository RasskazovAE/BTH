using BHT.Core.Readers.CoBa;
using BHT.Core.Services.CoBa;
using BTH.Core;
using NUnit.Framework;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BTH.Tests
{
    public class CoBaServiceTests : TestBase
    {
        [SetUp]
        public new void Setup()
        {
            base.Setup();
            Ioc.InstallTestDb();
            Ioc.InstallInterfaces();
        }

        [Test]
        public async Task LoadFileTwice_NoDuplicatesAdded()
        {
            var coBaService = Ioc.Resolve<ICoBaService>();
            var result = await coBaService.Get();
            Assert.AreEqual(0, result.Count());

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "testdata\\data.csv");

            var reader = Ioc.Resolve<ICoBaReader>();
            var coBaTransactions = await reader.ParseCsvFileAsync(filePath);

            await coBaService.AddNewAsync(coBaTransactions);
            result = await coBaService.Get();

            Assert.NotNull(result);
            Assert.AreEqual(7, result.Count());
            Assert.IsTrue(result.All(e => e.IBAN.Equals("DE64642525252525646425")));
            Assert.IsTrue(result.All(e => e.BIC.Equals("25252525")));
            Assert.IsTrue(result.All(e => e.ClientAccount.Equals("123456700")));

            await coBaService.AddNewAsync(coBaTransactions);
            result = await coBaService.Get();

            Assert.NotNull(result);
            Assert.AreEqual(7, result.Count());
            Assert.IsTrue(result.All(e => e.IBAN.Equals("DE64642525252525646425")));
            Assert.IsTrue(result.All(e => e.BIC.Equals("25252525")));
            Assert.IsTrue(result.All(e => e.ClientAccount.Equals("123456700")));
        }

        [Test]
        public async Task LoadFileWithDuplicates_NoDuplicatesAdded()
        {
            var coBaService = Ioc.Resolve<ICoBaService>();
            var result = await coBaService.Get();
            Assert.AreEqual(0, result.Count());

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "testdata\\data_duplicates.csv");

            var reader = Ioc.Resolve<ICoBaReader>();
            var coBaTransactions = await reader.ParseCsvFileAsync(filePath);

            await coBaService.AddNewAsync(coBaTransactions);
            result = await coBaService.Get();

            Assert.NotNull(result);
            Assert.AreEqual(7, result.Count());
            Assert.IsTrue(result.All(e => e.IBAN.Equals("DE64642525252525646425")));
            Assert.IsTrue(result.All(e => e.BIC.Equals("25252525")));
            Assert.IsTrue(result.All(e => e.ClientAccount.Equals("123456700")));
        }
    }
}
