namespace BTH.WPF.Printing.Dto
{
    public class RepPage
    {
        public double Height { get; set; }
        public double Width { get; set; }
        public double ContentHeight
        {
            get
            {
                return Table.Height;
            }
        }
        public RepTable Table { get; set; }

        public RepPage(double height, double width)
        {
            Height = height;
            Width = width;
        }
    }
}
