using app.Managers;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace app
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            db.GetAccessPoint();
        }

        private Dictionary<string, IManager> _managers = new Dictionary<string, IManager>();
        private String _currentPage = "";

        private void GoToPage(Page page)
        {
            mainFrame.Navigate(page);
        }

        private void OpenPage(object sender, RoutedEventArgs e)
        {
            string reqested = ((Button)sender).Tag as string;
            try
            {
                if (!_managers.ContainsKey(reqested))
                    _managers.Add(reqested, Activator.CreateInstance(Type.GetType("app.Managers." + reqested + "Manager")) as IManager);
            }
            catch (Exception) { }

            if (_managers.TryGetValue(reqested, out IManager manager))
                GoToPage(manager.GetPage());
            else
                Console.Error.WriteLine(reqested + " manager not found");

            _currentPage = reqested;
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            if (_managers.TryGetValue(_currentPage, out IManager manager))
                manager.Add();
        }

        private void Remove(object sender, RoutedEventArgs e)
        {
            if (_managers.TryGetValue(_currentPage, out IManager manager))
                manager.Delete();
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            if (_managers.TryGetValue(_currentPage, out IManager manager))
                manager.Edit();
        }
    }
}
