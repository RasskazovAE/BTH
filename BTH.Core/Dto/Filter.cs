using System;

namespace BTH.Core.Dto
{
    public class Filter : BaseNotifyObject
    {
        public DateTime? StartDate
        {
            get => Get<DateTime>();
            set => Set(value);
        }

        public DateTime? EndDate
        {
            get => Get<DateTime>();
            set => Set(value);
        }

        public string SearchText
        {
            get => Get<string>();
            set => Set(value);
        }
    }
}
