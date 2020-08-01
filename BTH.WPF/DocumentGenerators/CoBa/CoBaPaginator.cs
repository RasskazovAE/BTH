using BHT.Core.Entities;
using BTH.Core.Environment;
using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace BTH.WPF.DocumentGenerators.CoBa
{
    public class CoBaPaginator : DocumentPaginator
    {
        #region Fields

        private CoBaTransaction[] _transactions;
        private Typeface _typeface;
        private double _fontSize;
        private double _margin;
        private Size _pageSize;


        #endregion

        public override Size PageSize
        {
            get => _pageSize;
            set {
                _pageSize = value;
                PaginateData();
            }
        }

        public override bool IsPageCountValid => true;

        public override int PageCount => _pageCount;

        public override IDocumentPaginatorSource Source => null;

        public CoBaPaginator(CoBaTransaction[] transactions, Typeface typeface, double fontSize, double margin, Size pageSize)
        {
            _transactions = transactions;
            _typeface = typeface;
            _fontSize = fontSize;
            _margin = margin;
            _pageSize = pageSize;
            PaginateData();
        }

        public override DocumentPage GetPage(int pageNumber)
        {
            //Создать тесктовую строку для измерения
            FormattedText text = GetFormattedText("A");
            double col1_X = _margin;
            double col2_X = col1_X + text.Width * 12;
            double col3_X = col2_X + text.Width * 17;
            double col4_X = _pageSize.Width - _margin - text.Width * 15;
            double col4_end = _pageSize.Width - _margin;
            double col3_Width = col4_X - col3_X;
            int countLetters = (int)Math.Floor(col3_Width / text.Width);

            //Вычислить диапазон строк, которые попадают в эту страницу
            int minRow = pageNumber * _rowsPerPage;
            int maxRow = minRow + _rowsPerPage;
            //Создать визуальный элемент для страницы
            DrawingVisual visual = new DrawingVisual();
            //Установить позицию в верхний левый угол печатаемой обдасти
            Point point = new Point(_margin, _margin);
            using (DrawingContext dc = visual.RenderOpen())
            {
                //Нарисовать заголовки столбцов
                Typeface columnHeaderTypeface = new Typeface(_typeface.FontFamily, FontStyles.Normal, FontWeights.Bold, FontStretches.Normal);
                point.X = col1_X;
                text = GetFormattedText("Buchungstag", columnHeaderTypeface);
                dc.DrawText(text, point);
                point.X = col2_X;
                text = GetFormattedText("Umsatzart", columnHeaderTypeface);
                dc.DrawText(text, point);
                point.X = col3_X;
                text = GetFormattedText("Buchungstext", columnHeaderTypeface);
                dc.DrawText(text, point);
                text = GetFormattedText("Betrag", columnHeaderTypeface);
                point.X = col4_end - text.Width;
                dc.DrawText(text, point);
                //Нарисовать линию подчеркивания
                dc.DrawLine(new Pen(Brushes.Black, 2), new Point(_margin, _margin + text.Height), new Point(PageSize.Width - _margin, _margin + text.Height));
                point.Y += text.Height;
                //Нарисовать значения столбцов
                for (int i = minRow; i < maxRow; i++)
                {
                    //Проверить конец последней (частично заполненной) страницы
                    if (i > (_transactions.Length - 1)) break;
                    //Первый столбец
                    point.X = col1_X;
                    text = GetFormattedText(_transactions[i].BookingDate.ToShortDateString());
                    dc.DrawText(text, point);
                    //Второй столбец
                    point.X = col2_X;
                    text = GetFormattedText(_transactions[i].TurnoverType);
                    dc.DrawText(text, point);
                    //Третий столбец
                    point.X = col3_X;
                    text = GetFormattedText(_transactions[i].BookingText.Trim('"').Substring(0, countLetters));
                    dc.DrawText(text, point);
                    //Четверый столбец
                    text = GetFormattedText($"{_transactions[i].Amount.ToString("N2", BTHCulture.CultureInfo)} {_transactions[i].Currency}");
                    point.X = col4_end - text.Width;
                    dc.DrawText(text, point);
                    point.Y += text.Height;
                }
            }
            return new DocumentPage(visual, _pageSize, new Rect(_pageSize), new Rect(_pageSize));
        }

        private int _rowsPerPage;
        private int _pageCount;
        private void PaginateData()
        {
            FormattedText text = GetFormattedText("A");
            _rowsPerPage = (int)((_pageSize.Height - _margin * 2) / text.Height);
            _rowsPerPage -= 1;
            _pageCount = (int)Math.Ceiling((double)_transactions.Length / _rowsPerPage);
        }

        private FormattedText GetFormattedText(string text)
        {
            return GetFormattedText(text, _typeface);
        }

        private FormattedText GetFormattedText(string text, Typeface typeface)
        {
            return new FormattedText(text, BTHCulture.CultureInfo, FlowDirection.LeftToRight, typeface, _fontSize, Brushes.Black, 1);
        }
    }
}
