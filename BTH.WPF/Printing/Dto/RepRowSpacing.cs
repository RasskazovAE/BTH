using System;
using System.Collections.Generic;
using System.Text;

namespace BTH.WPF.Printing.Dto
{
    public class RepRowSpacing : IRepRowBlock
    {
        private readonly double _spacing;

        public double Y { get; set; }
        public double Height
        {
            get
            {
                return _spacing;
            }
        }

        public RepRowSpacing(double spacing)
        {
            _spacing = spacing;
        }
    }
}
