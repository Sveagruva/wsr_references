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
using wsrSpeedrun.Management.Models;

namespace wsrSpeedrun.Management.Managers
{
    public abstract class DemandManagerBase : Manager<DemandModel> { }
    public partial class DemandManager : DemandManagerBase
    {
        public DemandManager()
        {
            InitializeComponent();
            UpdateDispayable();
        }

        public override void UpdateDispayable()
        {
            grid.ItemsSource = db.Demands.ToList();
        }

        protected override DataGrid GetDataGrid() => grid;
    }

    public class DemandTypesConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";

            try
            {
                Demand demand = (Demand)value;
                if (demand.Flats.Count != 0)
                    return "Квартира";

                if (demand.Houses.Count != 0)
                    return "Дом";

                if (demand.Lands.Count != 0)
                    return "Земля";
            }
            catch (Exception) { }

            return "неизвестный";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static DemandTypesConverter instance = new DemandTypesConverter();
        public override object ProvideValue(IServiceProvider serviceProvider) => instance;
    }
}
