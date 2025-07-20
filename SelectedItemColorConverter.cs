using Microsoft.Maui.Controls;
using System;
using System.Globalization;

namespace CargaGestor;

public class SelectedItemColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var selectedItem = (parameter as CollectionView)?.SelectedItem;
        return object.Equals(value, selectedItem) ? Colors.LightSkyBlue : Colors.Transparent;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw new NotImplementedException();
}
