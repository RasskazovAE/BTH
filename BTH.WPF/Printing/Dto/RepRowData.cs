using System.Collections.Generic;
using System.Linq;

namespace BTH.WPF.Printing.Dto
{
    public class RepRowData : IRepRowBlock
    {
        public double Y { get; set; }
        public double Height
        {
            get
            {
                return Columns.Max(e => e.Text?.Height) ?? 0;
            }
        }

        public List<RepColumn> Columns { get; set; }

        public RepRowData()
        {
            Columns = new List<RepColumn>();
        }
    }
}
