using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Controls;
using System.Windows.Markup;
namespace ProcessInvokeCapture.Converters
{
    class SelectionChangedEventArgsConverter : EventArgsConverterExtension<SelectionChangedEventArgsConverter>
    {
        public override object Convert(object value, object parameter)
        {
            var e = value as SelectionChangedEventArgs;
            if (e == null)
            {
                return null;
            }
            return e.AddedItems[0];
        }
    }

    public abstract class EventArgsConverterExtension<T> : MarkupExtension, IEventArgsConverter where T : class, new()
    {
        private static Lazy<T> _converter = new Lazy<T>(() => new T());

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _converter.Value;
        }
        public abstract object Convert(object value, object parameter);
    }
}
