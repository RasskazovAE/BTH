using BTH.Core.Dto;
using NUnit.Framework;
using System;

namespace BTH.Tests
{
    public class DtoTests : TestBase
    {
        [Test]
        public void DtoFilterTest()
        {
            var propertyName = "";
            var filter = new Filter();
            filter.PropertyChanged += (o, e) => { propertyName = e.PropertyName; };
            var startDate = DateTime.Now.AddYears(-1);
            var endDate = DateTime.Now;
            var searchText = "search";

            filter.StartDate = startDate;
            Assert.AreEqual(nameof(Filter.StartDate), propertyName, $"{nameof(Filter.StartDate)} does not raised event.");
            Assert.AreEqual(startDate, filter.StartDate, $"{nameof(Filter.StartDate)}");

            filter.EndDate = endDate;
            Assert.AreEqual(nameof(Filter.EndDate), propertyName, $"{nameof(Filter.EndDate)} does not raised event.");
            Assert.AreEqual(endDate, filter.EndDate, $"{nameof(Filter.EndDate)}");

            filter.SearchText = searchText;
            Assert.AreEqual(nameof(Filter.SearchText), propertyName, $"{nameof(Filter.SearchText)} does not raised event.");
            Assert.AreEqual(searchText, filter.SearchText, $"{nameof(Filter.SearchText)}");

            filter.EndDate = endDate;
            Assert.AreEqual(nameof(Filter.SearchText), propertyName, $"{nameof(Filter.EndDate)} raised event, but value was the same");
        }
    }
}
