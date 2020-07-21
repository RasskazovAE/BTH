using System;

namespace BTH.Core.Dto
{
    public class Filter : BaseNotifyObject
    {
        private DateTime startTime;
        private DateTime endTime;
        private string searchText;

        public DateTime StartDate
        {
            get
            {
                return startTime;
            }
            set
            {
                this.startTime = value;
                base.OnPropertyRaised("StartTime");
            }
        }

        public DateTime EndDate
        {
            get
            {
                return endTime;
            }
            set
            {
                this.endTime = value;
                base.OnPropertyRaised("EndTime");
            }
        }

        public string SearchText
        {
            get
            {
                return searchText;
            }
            set
            {
                this.searchText = value;
                base.OnPropertyRaised("SearchText");
            }
        }
    }
}
