using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using wsrSpeedrun.Management;

namespace wsrSpeedrun
{
    /// <summary>
    /// Interaction logic for ManageInstaceWindow.xaml
    /// </summary>
    public partial class ManageInstaceWindow : Window
    {
        public ManageInstaceWindow(IModel model)
        {
            InitializeComponent();
            DataContext = model;
            model.Conctruct(userInputs);
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            try
            {
                (DataContext as IModel).Save();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
