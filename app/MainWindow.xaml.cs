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

            #region init managers buttons
            foreach (KeyValuePair<Type, string> managerEntry in managersNames)
            {
                Button btn = new Button
                {
                    Tag = managerEntry.Key,
                    Content = managerEntry.Value
                };

                btn.AddHandler(Button.ClickEvent, new RoutedEventHandler(OpenPage));
                managersStackPanel.Children.Add(btn);
            }
            #endregion
        }

        private static readonly Dictionary<Type, string> managersNames = new Dictionary<Type, string>()
        {
            { typeof(ClientManager), "Клиенты" },
            { typeof(WorkerManager), "Агенты" }
        };

        private Dictionary<Type, IManager> _managers = new Dictionary<Type, IManager>();
        private Type _currentPage = typeof(ClientManager);

        #region page management stuff
        private void GoToPage(IManager manager)
        {
            mainFrame.Navigate(manager as Page);
        }
        private void OpenPage(object sender, RoutedEventArgs e)
        {
            Type reqested = ((Button)sender).Tag as Type;
            try
            {
                if (!_managers.ContainsKey(reqested))
                    _managers.Add(reqested, Activator.CreateInstance(reqested) as IManager);
            }
            catch (Exception) { }

            if (_managers.TryGetValue(reqested, out IManager manager))
                GoToPage(manager);
            else
                Console.Error.WriteLine(reqested + " manager not found");

            _currentPage = reqested;
        }
        #endregion

        #region buttons handlers
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
        #endregion
    }
}
