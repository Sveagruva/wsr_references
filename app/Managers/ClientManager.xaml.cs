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

namespace app.Managers
{
    public abstract class ClientManagerBase : Manager<Client> { }
    public partial class ClientManager : ClientManagerBase
    {
        public override DataGrid GetDataGrid() => display;

        public ClientManager()
        {
            InitializeComponent();
            display.ItemsSource = db.Client.ToArray();
        }

        public override Tuple<bool, string> Validate(Tuple<object, bool> obj)
        {
            return new Tuple<bool, string>(true, "");
        }

        protected override bool Constructor(StackPanel element)
        {
            TextBox textbox = new TextBox();

            textbox.SetBinding(TextBox.TextProperty, new Binding("Name"));
            element.Children.Add(Helpers.LabelElement(textbox, "Имя"));

            textbox = new TextBox();
            textbox.SetBinding(TextBox.TextProperty, new Binding("Surname"));
            element.Children.Add(Helpers.LabelElement(textbox, "Фамилия"));

            textbox = new TextBox();
            textbox.SetBinding(TextBox.TextProperty, new Binding("Middlename"));
            element.Children.Add(Helpers.LabelElement(textbox, "Отчество"));

            textbox = new TextBox();
            textbox.SetBinding(TextBox.TextProperty, new Binding("Phone"));
            element.Children.Add(Helpers.LabelElement(textbox, "Телефон"));

            textbox = new TextBox();
            textbox.SetBinding(TextBox.TextProperty, new Binding("Email"));
            element.Children.Add(Helpers.LabelElement(textbox, "Email"));


            return true;
        }
    }
}
