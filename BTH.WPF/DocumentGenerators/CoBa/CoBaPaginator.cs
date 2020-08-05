using BHT.Core.Entities;
using BTH.Core.Environment;
using BTH.Core.Extensions;
using BTH.WPF.Printing.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private List<RepPage> _pages;
        private readonly FormattedText _defaultText;

        #endregion

        public override Size PageSize
        {
            get => _pageSize;
            set
            {
                _pageSize = value;
                PaginateData();
            }
        }

        public override bool IsPageCountValid => true;

        public override int PageCount => _pages.Count;

        public override IDocumentPaginatorSource Source => null;

        public CoBaPaginator(CoBaTransaction[] transactions, Typeface typeface, double fontSize, double margin, Size pageSize)
        {
            _pages = new List<RepPage>();
            _transactions = transactions;
            _typeface = typeface;
            _fontSize = fontSize;
            _margin = margin;
            _pageSize = pageSize;
            _defaultText = GetFormattedText("A");
            PaginateData();
        }

        public override DocumentPage GetPage(int pageNumber)
        {
            var current = _pages[pageNumber];
            //Создать визуальный элемент для страницы
            DrawingVisual visual = new DrawingVisual();
            using (DrawingContext dc = visual.RenderOpen())
            {
                PrintTable(dc, current.Table);
                PrintPageNumber(dc, pageNumber);
            }
            return new DocumentPage(visual, _pageSize, new Rect(_pageSize), new Rect(_pageSize));
        }

        private void PaginateData()
        {
            //Создать тесктовую строку для измерения
            double col1_X = _margin;
            double col1_width = _defaultText.Width * 12;
            double col2_X = col1_X + _defaultText.Width * 12;
            double col2_width = _defaultText.Width * 17;
            double col3_X = col2_X + _defaultText.Width * 17;
            double col4_X = _pageSize.Width - _margin - _defaultText.Width * 15;
            double col3_width = col4_X - col3_X;
            double col4_width = _defaultText.Width * 15;
            int col3_Letters = (int)Math.Floor(col3_width / _defaultText.Width);

            RepPage current = null;
            foreach (var transaction in _transactions)
            {
                if (current == null)
                    current = GetEmptyPageWithTable(
                        col1_X, col1_width,
                        col2_X, col2_width,
                        col3_X, col3_width,
                        col4_X, col4_width);

                current.Table.Body.AddLineSpacing(_defaultText.Height);
                var bookingDates = new[] { transaction.BookingDate.ToShortDateString() };
                var turnoverTypes = new[] { transaction.TurnoverType };
                var bookingTextRows = transaction.BookingText.Trim('"').SplitByLength(col3_Letters).ToList();
                var amounts = new[] { $"{transaction.Amount.ToString("N2", BTHCulture.CultureInfo)} {transaction.Currency}" };

                var rows = Enumerable.Range(0, bookingTextRows.Count)
                    .Select(i => {
                        var row = new RepRowData();
                        row.Columns.Add(new RepColumn { X = col1_X, Width = col1_width, Text = GetFormattedText(bookingDates.ElementAtOrDefault(i)) });
                        row.Columns.Add(new RepColumn { X = col2_X, Width = col2_width, Text = GetFormattedText(turnoverTypes.ElementAtOrDefault(i)) });
                        row.Columns.Add(new RepColumn { X = col3_X, Width = col3_width, Text = GetFormattedText(bookingTextRows.ElementAtOrDefault(i)) });
                        row.Columns.Add(new RepColumn { X = col4_X, Width = col4_width, Text = GetFormattedText(amounts.ElementAtOrDefault(i)), TextAlign = ColumnTextAlign.Right });
                        return row;
                    });

                foreach(var row in rows)
                {
                    if (current == null)
                        current = GetEmptyPageWithTable(
                            col1_X, col1_width,
                            col2_X, col2_width,
                            col3_X, col3_width,
                            col4_X, col4_width);
                    if(current.ContentHeight + row.Height >= current.Height - _margin * 2)
                    {
                        _pages.Add(current);
                        current = null;
                    }
                    else
                    {
                        current.Table.Body.AddRow(row);
                    }
                }
            }
            if (current != null)
                _pages.Add(current);
        }

        private RepPage GetEmptyPageWithTable(
            double col1_X, double col1_width,
            double col2_X, double col2_width,
            double col3_X, double col3_width,
            double col4_X, double col4_width)
        {
            var page = new RepPage(_pageSize.Height, _pageSize.Width);
            page.Table = new RepTable(_margin, _margin);
            //Нарисовать заголовки столбцов
            Typeface columnHeaderTypeface = new Typeface(_typeface.FontFamily, FontStyles.Normal, FontWeights.Bold, FontStretches.Normal);
            var row = new RepRowData();
            row.Columns.Add(new RepColumn { X = col1_X, Width = col1_width, Text = GetFormattedText("Buchungstag", columnHeaderTypeface) });
            row.Columns.Add(new RepColumn { X = col2_X, Width = col2_width, Text = GetFormattedText("Umsatzart", columnHeaderTypeface) });
            row.Columns.Add(new RepColumn { X = col3_X, Width = col3_width, Text = GetFormattedText("Buchungstext", columnHeaderTypeface) });
            row.Columns.Add(new RepColumn { X = col4_X, Width = col4_width, Text = GetFormattedText("Betrag", columnHeaderTypeface), TextAlign = ColumnTextAlign.Right });
            page.Table.Header.AddRow(row);

            return page;
        }

        private void PrintTable(DrawingContext dc, RepTable table)
        {
            var point = new Point(table.X, table.Y);
            //Нарисовать заголовки столбцов
            foreach (var row in table.Header.Rows)
            {
                point.Y = row.Y;
                foreach (var col in row.Columns)
                {
                    point.X = col.TextX;
                    dc.DrawText(col.Text, point);
                }
            }
            //Нарисовать линию подчеркивания
            dc.DrawLine(new Pen(Brushes.Black, 2), new Point(_margin, point.Y + _defaultText.Height), new Point(PageSize.Width - _margin, point.Y + _defaultText.Height));
            point.Y += _defaultText.Height;
            //Нарисовать значения столбцов
            foreach (var row in table.Body.Rows)
            {
                point.Y = row.Y;
                foreach (var col in row.Columns)
                {
                    point.X = col.TextX;
                    dc.DrawText(col.Text, point);
                }
            }
            if (!table.Footer.IsEmpty)
            {
                //Нарисовать линию подчеркивания
                dc.DrawLine(new Pen(Brushes.Black, 2), new Point(_margin, point.Y + _defaultText.Height), new Point(PageSize.Width - _margin, point.Y + _defaultText.Height));
                point.Y += _defaultText.Height;
                //Нарисовать значения столбцов
                foreach (var row in table.Footer.Rows)
                {
                    point.Y = row.Y;
                    foreach (var col in row.Columns)
                    {
                        point.X = col.TextX;
                        dc.DrawText(col.Text, point);
                    }
                }
            }
        }

        private void PrintPageNumber(DrawingContext dc, int pageNumber)
        {
            var point = new Point(_pageSize.Width / 2, _pageSize.Height - _margin + _defaultText.Height);
            var text = GetFormattedText($"Seite {pageNumber + 1} von {_pages.Count}");
            point.X = point.X - text.Width / 2;
            dc.DrawText(text, point);
        }

        private FormattedText GetFormattedText(string text)
        {
            return GetFormattedText(text, _typeface);
        }

        private FormattedText GetFormattedText(string text, Typeface typeface)
        {
            if (string.IsNullOrEmpty(text)) text = string.Empty;
            return new FormattedText(text, BTHCulture.CultureInfo, FlowDirection.LeftToRight, typeface, _fontSize, Brushes.Black, 1);
        }
    }
}
