using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using wsrSpeedrun.Management;
using wsrSpeedrun.Management.Managers;
using wsrSpeedrun.Management.Models;

namespace wsrSpeedrun
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            foreach(MethodInfo met in typeof(AgentModel).GetMethods())
            {
                Console.WriteLine(met.Name);
            }

            foreach(Type pageType in managers)
            {
                if (!managersLabels.TryGetValue(pageType, out string label))
                    continue;
                Button btn = new Button()
                {
                    Content = label,
                    Tag = pageType
                };

                btn.Click += Go2Page;
                pagesNavigation.Children.Add(btn);
            }

        }

        private Dictionary<Type, IManager> pages = new Dictionary<Type, IManager>();
        private readonly List<Type> managers = new List<Type>() { typeof(AgentManager), typeof(ClientManager), typeof(DemandManager) };
        private readonly Dictionary<Type, string> managersLabels = new Dictionary<Type, string>()
        {
            { typeof(AgentManager), "Агенты" },
            { typeof(ClientManager), "Клиенты" },
            { typeof(DemandManager), "Недвижимость" }
        };

        private Type _currentType;

        private void Remove(object sender, RoutedEventArgs e)
        {
            if (pages.TryGetValue(_currentType, out IManager manager))
                manager.Remove();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            if (pages.TryGetValue(_currentType, out IManager manager))
                manager.Add();
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            if (pages.TryGetValue(_currentType, out IManager manager))
                manager.Edit();
        }

        private void Go2Page(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Type reqestedManger = btn.Tag as Type;
            if (!pages.ContainsKey(reqestedManger))
                pages.Add(reqestedManger, (IManager)Activator.CreateInstance(reqestedManger));

            if (pages.TryGetValue(reqestedManger, out IManager manager))
                frame.Navigate(manager);

            _currentType = reqestedManger;
        }
    }
}
