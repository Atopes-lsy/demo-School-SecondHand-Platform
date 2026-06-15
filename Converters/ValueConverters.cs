using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using School二手Platform.Models;

namespace School二手Platform.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool flag = value is true;
            bool invert = parameter is string s && s == "Invert";
            return (flag ^ invert) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class ProductConditionToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                ProductCondition.New => "全新",
                ProductCondition.NearExpiry => "临期",
                ProductCondition.MoreThanHalf => "余量>半",
                ProductCondition.LessThanHalf => "余量<半",
                _ => "未知"
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class PriceStrategyToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                PriceStrategy.Fixed => "不砍价",
                PriceStrategy.HighestBidder => "价高者得",
                _ => "未知"
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class ConditionToBadgeColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                ProductCondition.New => new SolidColorBrush(Color.FromRgb(39, 174, 96)),       // Green
                ProductCondition.NearExpiry => new SolidColorBrush(Color.FromRgb(231, 76, 60)), // Red
                ProductCondition.MoreThanHalf => new SolidColorBrush(Color.FromRgb(52, 152, 219)), // Blue
                ProductCondition.LessThanHalf => new SolidColorBrush(Color.FromRgb(243, 156, 18)), // Orange
                _ => new SolidColorBrush(Colors.Gray)
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class StrategyToBadgeColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                PriceStrategy.Fixed => new SolidColorBrush(Color.FromRgb(39, 174, 96)),          // Green
                PriceStrategy.HighestBidder => new SolidColorBrush(Color.FromRgb(230, 126, 34)), // Orange
                _ => new SolidColorBrush(Colors.Gray)
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class PriceToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal price)
                return $"¥{price:F2}";
            return "¥0.00";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class CategoryToGradientConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as string) switch
            {
                "课本图书" => new LinearGradientBrush(
                    Color.FromRgb(0x66, 0x7E, 0xEA),
                    Color.FromRgb(0x76, 0x4B, 0xA2), 45),
                "生活用品" => new LinearGradientBrush(
                    Color.FromRgb(0x43, 0xE9, 0x7B),
                    Color.FromRgb(0x38, 0xF9, 0xD7), 45),
                "电子产品" => new LinearGradientBrush(
                    Color.FromRgb(0x4F, 0xAC, 0xFE),
                    Color.FromRgb(0x00, 0xF2, 0xFE), 45),
                "食品" => new LinearGradientBrush(
                    Color.FromRgb(0xFA, 0x70, 0x9A),
                    Color.FromRgb(0xFE, 0xE1, 0x40), 45),
                "虚拟产品" => new LinearGradientBrush(
                    Color.FromRgb(0xA1, 0x8C, 0xD1),
                    Color.FromRgb(0xFB, 0xC2, 0xEB), 45),
                _ => new LinearGradientBrush(Colors.SlateGray, Colors.DimGray, 45)
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class BoolToFavoriteIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is true ? "♥" : "♡";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
