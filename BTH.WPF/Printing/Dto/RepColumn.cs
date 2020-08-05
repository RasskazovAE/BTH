using System.Windows.Media;

namespace BTH.WPF.Printing.Dto
{
    public class RepColumn
    {
        public double X { get; set; }
        public double Width { get; set; }
        public FormattedText Text { get; set; }
        public ColumnTextAlign TextAlign { get; set; } = ColumnTextAlign.Left;
        public double TextX
        {
            get
            {
                switch (TextAlign)
                {
                    case ColumnTextAlign.Center:
                        return X + (Width - Text.Width) / 2;
                    case ColumnTextAlign.Right:
                        return X + Width - Text.Width;
                    default:
                        return X;
                }
            }
        }
    }

    public enum ColumnTextAlign
    {
        Left = 0,
        Center = 1,
        Right = 2
    }
}
