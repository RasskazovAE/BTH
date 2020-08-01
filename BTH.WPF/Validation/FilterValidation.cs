using System;
using System.Windows;
using System.Windows.Controls;

namespace BTH.WPF.Validation
{
    public class AttachedProperties : DependencyObject
    {
        #region RegisterBlackoutDates

        public static DependencyProperty RegisterStartDateProperty = DependencyProperty.RegisterAttached("RegisterStartDate", typeof(DateTime?), typeof(AttachedProperties), new PropertyMetadata(null, OnRegisterStartDateCommandBindingChanged));
        public static DependencyProperty RegisterEndDateProperty = DependencyProperty.RegisterAttached("RegisterEndDate", typeof(DateTime?), typeof(AttachedProperties), new PropertyMetadata(null, OnRegisterEndDateCommandBindingChanged));

        public static void SetRegisterStartDate(UIElement element, DateTime? value)
        {
            if (element != null)
                element.SetValue(RegisterStartDateProperty, value);
        }

        public static void SetRegisterEndDate(UIElement element, DateTime? value)
        {
            if (element != null)
                element.SetValue(RegisterEndDateProperty, value);
        }

        public static DateTime? GetRegisterStartDate(UIElement element)
        {
            return element != null ? (DateTime?)element.GetValue(RegisterStartDateProperty) : null;
        }

        public static DateTime? GetRegisterEndDate(UIElement element)
        {
            return element != null ? (DateTime?)element.GetValue(RegisterEndDateProperty) : null;
        }

        private static void OnRegisterStartDateCommandBindingChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            DatePicker element = sender as DatePicker;
            if (element != null)
            {
                DateTime? binding = e.NewValue as DateTime?;
                if (binding != null)
                {
                    var date = (DateTime)binding;
                    element.BlackoutDates.Clear();
                    element.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue.Date, date.Date));
                }
            }
        }

        private static void OnRegisterEndDateCommandBindingChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            DatePicker element = sender as DatePicker;
            if (element != null)
            {
                DateTime? binding = e.NewValue as DateTime?;
                if (binding != null)
                {
                    var date = (DateTime)binding;
                    element.BlackoutDates.Clear();
                    element.BlackoutDates.Add(new CalendarDateRange(date.Date, DateTime.MaxValue.Date));
                }
            }
        }
        #endregion
    }
}
