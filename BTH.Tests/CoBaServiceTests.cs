using BHT.Core.Entities;
using BHT.Core.Readers.CoBa;
using BHT.Core.Services.CoBa;
using BTH.Core;
using BTH.Core.Context;
using BTH.Core.Dto;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
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
            var filter = new Filter() { EndDate = DateTime.Now};
            var coBaService = Ioc.Resolve<ICoBaService>();
            var result = await coBaService.Get(filter);
            Assert.AreEqual(0, result.Count());

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "testdata\\data.csv");

            var reader = Ioc.Resolve<ICoBaReader>();
            var coBaTransactions = await reader.ParseCsvFileAsync(filePath);

            await coBaService.AddNewAsync(coBaTransactions);
            result = await coBaService.Get(filter);

            Assert.NotNull(result);
            Assert.AreEqual(7, result.Count());
            Assert.IsTrue(result.All(e => e.IBAN.Equals("DE64642525252525646425")));
            Assert.IsTrue(result.All(e => e.BIC.Equals("25252525")));
            Assert.IsTrue(result.All(e => e.ClientAccount.Equals("123456700")));

            await coBaService.AddNewAsync(coBaTransactions);
            result = await coBaService.Get(filter);

            Assert.NotNull(result);
            Assert.AreEqual(7, result.Count());
            Assert.IsTrue(result.All(e => e.IBAN.Equals("DE64642525252525646425")));
            Assert.IsTrue(result.All(e => e.BIC.Equals("25252525")));
            Assert.IsTrue(result.All(e => e.ClientAccount.Equals("123456700")));
        }

        [Test]
        public async Task LoadFileWithDuplicates_NoDuplicatesAdded()
        {
            var filter = new Filter() { EndDate = DateTime.Now };
            var coBaService = Ioc.Resolve<ICoBaService>();
            var result = await coBaService.Get(filter);
            Assert.AreEqual(0, result.Count());

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "testdata\\data_duplicates.csv");

            var reader = Ioc.Resolve<ICoBaReader>();
            var coBaTransactions = await reader.ParseCsvFileAsync(filePath);

            await coBaService.AddNewAsync(coBaTransactions);
            result = await coBaService.Get(filter);

            Assert.NotNull(result);
            Assert.AreEqual(7, result.Count());
            Assert.IsTrue(result.All(e => e.IBAN.Equals("DE64642525252525646425")));
            Assert.IsTrue(result.All(e => e.BIC.Equals("25252525")));
            Assert.IsTrue(result.All(e => e.ClientAccount.Equals("123456700")));
        }

        [Test]
        public async Task AddDuplicateTransaction_ErrorRaised()
        {
            var ctx = Ioc.Resolve<DataContext>();

            var transaction = new CoBaTransaction
            {
                BookingDate = DateTime.Parse("08.07.2019"),
                ValueDate = DateTime.Parse("08.07.2019"),
                TurnoverType = "Lastschrift",
                BookingText = "Kartenzahlung FKA UESTRA AG//Hannove/DE 2019-07-04T10:49:08 KFN 0 VJ 2112",
                Amount = -13.20m,
                Currency = "EUR",
                ClientAccount = "123456700",
                BIC = "25252525",
                IBAN = "DE64642525252525646425",
                Category = "Unkategorisierte Ausgaben"
            };

            ctx.CoBaTransactions.Add(transaction);
            await ctx.SaveChangesAsync();

            ctx.CoBaTransactions.Add(transaction);
            Assert.ThrowsAsync<DbUpdateException>(async () => await ctx.SaveChangesAsync());
        }
    }
}
