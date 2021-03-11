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
using wsrSpeedrun.Management.Models;

namespace wsrSpeedrun.Management.Managers
{
    /// <summary>
    /// Interaction logic for AgentManager.xaml
    /// </summary>
    public abstract class AgentManagerBase : Manager<AgentModel> { };
    public partial class AgentManager : AgentManagerBase
    {
        public AgentManager()
        {
            InitializeComponent();
            UpdateDispayable();
        }

        List<Agent> agents;
        string serchangeble = "";
        public override void UpdateDispayable()
        {
            agents = new List<Agent>();
            db.Agents.ToList().ForEach(agent => AddIfOk(agent));
            grid.ItemsSource = agents;
        }

        private void AddIfOk(Agent agent)
        {
            string[] searchingArray = new string[3];

            try
            {
                var array = serchangeble.Split(' ');
                searchingArray[0] = null;
                searchingArray[1] = null;
                searchingArray[2] = null;
                searchingArray[0] = array[0];
                searchingArray[1] = array[1];
                searchingArray[2] = array[2];
            }catch (Exception){}


            if (
                Helpers.IsOkeyWithLevenstneinsLength(agent.Name, searchingArray[0], 3) &&
                Helpers.IsOkeyWithLevenstneinsLength(agent.Middlename, searchingArray[1], 3) &&
                Helpers.IsOkeyWithLevenstneinsLength(agent.Lastname, searchingArray[2], 3))
                agents.Add(agent);
        }

        protected override DataGrid GetDataGrid() => grid;

        private void searchbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            serchangeble = searchbox.Text;
            UpdateDispayable();
        }
    }
}
