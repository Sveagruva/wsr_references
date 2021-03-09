using creationOfOneToOneRelationsWtihPopUp.Management;
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

namespace creationOfOneToOneRelationsWtihPopUp
{
    /// <summary>
    /// Interaction logic for PopUp.xaml
    /// </summary>
    public partial class PopUp : Window
    {
        IManagedTypeModel model;
        public PopUp(IManagedTypeModel model)
        {
            InitializeComponent();
            this.model = model;
            DataContext = model;
            model.Constructor(panelika);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var result = model.CheckItSelf();
            if (result.Item1)
            {
                model.Save();
                Close();
            }
            else
            {
                MessageBox.Show(result.Item2);
            }
        }
    }
}
