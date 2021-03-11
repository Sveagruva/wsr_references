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
    public abstract class ClientManagerBase : Manager<ClientModel> { }
    public partial class ClientManager : ClientManagerBase
    {
        public ClientManager()
        {
            InitializeComponent();
            UpdateDispayable();
        }

        List<Client> clients;
        string serchangeble = "";
        public override void UpdateDispayable()
        {
            clients = new List<Client>();
            db.Clients.ToList().ForEach(client => AddIfOk(client));
            grid.ItemsSource = clients;
        }

        private void AddIfOk(Client client)
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
            }
            catch (Exception) { }


            if (
                Helpers.IsOkeyWithLevenstneinsLength(client.Name, searchingArray[0], 3) &&
                Helpers.IsOkeyWithLevenstneinsLength(client.Middlename, searchingArray[1], 3) &&
                Helpers.IsOkeyWithLevenstneinsLength(client.Lastname, searchingArray[2], 3))
                clients.Add(client);
        }

        protected override DataGrid GetDataGrid() => grid;

        private void searchbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            serchangeble = searchbox.Text;
            UpdateDispayable();
        }
    }
}
