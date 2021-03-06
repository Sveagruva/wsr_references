using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace converter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<place> places;
        public MainWindow()
        {
            InitializeComponent();
            places = new StateCompanyEntities().places.ToList();
            hi.ItemsSource = places;
        }
    }
    


    class conv : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                place pidar = (place)value;
                if (pidar.apartment != null)
                    return "apartment";

                if (pidar.house1 != null)
                    return "house";

                return "land";
            }
            catch
            {
                return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        static conv conv123 = new conv();

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return conv123;
        }
    }
}
