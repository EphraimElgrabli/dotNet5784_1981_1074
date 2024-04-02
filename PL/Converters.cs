using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using BO;
using DO;

namespace PL;

class ConvertIdToContent : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (int)value == 0 ? "Add" : "Update";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

class IsVisible : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is BO.User user)
        {
            if ((int)user.Level >= 4)
            {
                return Visibility.Visible;
            }
        }
        return Visibility.Hidden;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
class NameOfViewer : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is BO.User user)
        {
             return user.Name;
        }
        return "Unkown User";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
class Color : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
       
        if (value is BO.Task)
        {
            if(((BO.Task)value).Status == BO.Status.Unscheduled )
            { 
                return "Black";
            }
            if (((BO.Task)value).Status == BO.Status.Scheduled)
            {
                return "Blue";
            }
            if (((BO.Task)value).Status == BO.Status.OnTrack)
            {
                return "Green";
            }
            if (((BO.Task)value).Status == BO.Status.Done)
            {
                return "Red";
            }
            if (((BO.Task)value).Status == BO.Status.InJeopardy)
            {
                return "Orange";
            }
        }
        return "Unkown User";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
public class IntegerToGridLengthConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int percentage)
        {
            return new GridLength(percentage, GridUnitType.Star);
        }
        return new GridLength(0);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

class IsEnabled : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        
        return (int)value == 0 ? true : false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class NameToProfilePicConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string name && !string.IsNullOrEmpty(name))
        {
            string url = $"https://api.dicebear.com/7.x/lorelei/png?seed={Uri.EscapeDataString(name)}";
            return url;
        }
        return "https://api.dicebear.com/7.x/lorelei/png?seed=default";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class AliasToBannerPicConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string Alias && !string.IsNullOrEmpty(Alias))
        {
            string url = $"https://picsum.photos/seed/Wedding{Uri.EscapeDataString(Alias)}/500/840";
            return url;
        }
        return "https://picsum.photos/seed/Wedding/500/840";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}