using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace convertor
{
    class Conv : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                place pl = (place)value;

                if (pl.apartment != null)
                    return "bitche's flat";

                if (pl.land != null)
                    return "land";

                return "mr mysterio mb house";

            }
            catch (Exception)
            {
                return "";
                throw;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
