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
            var eventCalled = false;
            var filter = new Filter();
            filter.PropertyChanged += (o, e) => { eventCalled = true; };
            var startDate = DateTime.Now.AddYears(-1);
            var endDate = DateTime.Now;
            var searchText = "search";

            filter.StartDate = startDate;
            Assert.IsTrue(eventCalled, $"{nameof(Filter.StartDate)} does not raised event.");
            Assert.AreEqual(startDate, filter.StartDate, $"{nameof(Filter.StartDate)}");

            eventCalled = false;
            filter.EndDate = endDate;
            Assert.IsTrue(eventCalled, $"{nameof(Filter.EndDate)} does not raised event.");
            Assert.AreEqual(endDate, filter.EndDate, $"{nameof(Filter.EndDate)}");

            eventCalled = false;
            filter.SearchText = searchText;
            Assert.IsTrue(eventCalled, $"{nameof(Filter.SearchText)} does not raised event.");
            Assert.AreEqual(searchText, filter.SearchText, $"{nameof(Filter.SearchText)}");
        }
    }
}
