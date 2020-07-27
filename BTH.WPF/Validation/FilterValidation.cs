using BTH.Core.Dto;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace BTH.WPF.Validation
{
    public class EndDateValidationRule : ValidationRule
    {
        public FilterWrapper FilterWrapper { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            DateTime? date = (DateTime?)value;
            if (FilterWrapper.Filter.StartDate != null)
                return new ValidationResult(date >= FilterWrapper.Filter.StartDate, "");

            return ValidationResult.ValidResult;
        }
    }
    public class StartDateValidationRule : ValidationRule
    {
        public FilterWrapper FilterWrapper { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            DateTime? date = (DateTime?)value;

            if (FilterWrapper.Filter.EndDate != null)
                return new ValidationResult(date <= FilterWrapper.Filter.EndDate, "");

            return ValidationResult.ValidResult;
        }
    }


    public class FilterWrapper : DependencyObject
    {
        public static readonly DependencyProperty StartDateProperty = DependencyProperty.Register(
            "Filter",
            typeof(Filter),
            typeof(FilterWrapper),
            new PropertyMetadata(default(object)));

        public Filter Filter
        {
            get
            {
                return (Filter)GetValue(StartDateProperty);
            }
            set
            {
                SetValue(StartDateProperty, value);
            }
        }
    }

    public class BindingProxy : Freezable
    {
        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }

        public object Data
        {
            get
            {
                return (object)GetValue(DataProperty);
            }
            set
            {
                SetValue(DataProperty, value);
            }
        }

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(object), typeof(BindingProxy), new PropertyMetadata(null));
    }
}
