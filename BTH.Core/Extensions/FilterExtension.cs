using BHT.Core.Entities;
using BTH.Core.Dto;
using System.Linq;

namespace BTH.Core.Extensions
{
    public static class FilterExtension
    {
        public static IQueryable<CoBaTransaction> ApplyFilter(this IQueryable<CoBaTransaction> query, Filter filter)
        {
            if (filter.StartDate != null)
                query = query.Where(t => t.BookingDate >= filter.StartDate || t.ValueDate >= filter.StartDate);

            if (filter.EndDate != null)
                query = query.Where(t => t.BookingDate <= filter.EndDate || t.ValueDate <= filter.EndDate);

            if (filter.SearchText != null)
                query = query.Where(t => t.BookingText.ToLower().Contains(filter.SearchText.ToLower()));

            return query;
        }
    }
}
