using System.Collections.Generic;
using System.Linq;

namespace BTH.WPF.Printing.Dto
{
    public abstract class RepRowsContent
    {
        private readonly RepTable _table;

        public double Height
        {
            get
            {
                return _rows.Sum(e => e.Height);
            }
        }

        public bool IsEmpty
        {
            get
            {
                return !Rows.Any();
            }
        }

        private List<IRepRowBlock> _rows;
        public IEnumerable<RepRowData> Rows { get { return _rows.OfType<RepRowData>(); } }

        public RepRowsContent(RepTable table)
        {
            _table = table;
            _rows = new List<IRepRowBlock>();
        }

        public void AddRow(RepRowData row)
        {
            row.Y = _table.Y + _table.Height;
            _rows.Add(row);
        }

        public void AddLineSpacing(double spacing)
        {
            var rowSpacing = new RepRowSpacing(spacing);
            rowSpacing.Y = _table.Y + _table.Height;
            _rows.Add(rowSpacing);
        }
    }
}
