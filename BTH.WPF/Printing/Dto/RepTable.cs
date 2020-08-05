namespace BTH.WPF.Printing.Dto
{
    public class RepTable
    {
        public double X { get; }
        public double Y { get; }
        public double Height
        {
            get
            {
                return Header.Height + Body.Height + Footer.Height;
            }
        }
        public RepHeader Header { get; }
        public RepBody Body { get; }
        public RepFooter Footer { get; }

        public RepTable(double x, double y)
        {
            X = x;
            Y = y;
            Header = new RepHeader(this);
            Body = new RepBody(this);
            Footer = new RepFooter(this);
        }
    }
}
