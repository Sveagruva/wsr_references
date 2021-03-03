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

namespace app.Managers
{
    /// <summary>
    /// Interaction logic for AgentManager.xaml
    /// </summary>
    public partial class WorkerManager : Manager
    {
        public WorkerManager()
        {
            InitializeComponent();
            display.ItemsSource = db.GetAccessPoint().Worker.ToArray();
        }

        public override bool Constructor(StackPanel element)
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
            textbox.SetBinding(TextBox.TextProperty, new Binding("Cut"));
            element.Children.Add(Helpers.LabelElement(textbox, "Доля"));


            return true;
        }

        public override Tuple<bool, string> Validate(Tuple<object, bool> obj)
        {
            if (!obj.Item2)
                return new Tuple<bool, string>(true, "");

            Worker worker = (Worker)obj.Item1;
            if(String.IsNullOrEmpty(worker.Name) || string.IsNullOrEmpty(worker.Middlename) || string.IsNullOrEmpty(worker.Surname))
                return new Tuple<bool, string>(false, "Агент должен иметь полное имя");

            return new Tuple<bool, string>(true, "");
        }

        public override Type GetManagedType() => typeof(Worker);
        protected override DataGrid GetDataGrid() => display;
    }
}
