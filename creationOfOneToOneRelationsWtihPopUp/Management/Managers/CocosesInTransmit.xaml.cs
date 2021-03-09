using creationOfOneToOneRelationsWtihPopUp.Management.Models;
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

namespace creationOfOneToOneRelationsWtihPopUp
{
    public abstract class CocosesBasePP : Manager<CocosPackageModel> { }
    public partial class CocosesInTransmit : CocosesBasePP
    {
        public CocosesInTransmit()
        {
            InitializeComponent();
            PremiumGridbreakingConveNSIToNahhahahahahah.ItemsSource = new testingEntities().CocosPackages.ToList();
        }

        private void Button_Click_lol(object sender, RoutedEventArgs e)
        {
            Add();
        }
    }
}
