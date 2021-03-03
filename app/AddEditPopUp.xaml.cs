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

namespace app
{
    /// <summary>
    /// Interaction logic for AddEditPopUp.xaml
    /// </summary>
    public partial class AddEditPopUp : Window
    {
        private Func<Tuple<object, bool>, Tuple<bool, string>> _validator;
        public AddEditPopUp(object obj, Func<StackPanel, bool> constructor, Func<Tuple<object, bool>, Tuple<bool, string>> validator)
        {
            InitializeComponent();
            DataContext = obj;
            _validator = validator;

            constructor(space);
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }


        private void Apply(object sender, RoutedEventArgs e)
        {
            var valresult = _validator(new Tuple<object, bool>(DataContext, true));
            if (!valresult.Item1)
            {
                MessageBox.Show(valresult.Item2);
                return;
            }

            DialogResult = true;
        }
    }
}
