using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace app.Managers
{
    public abstract class Manager : Page
    {
        abstract public Type GetManagedType();
        abstract protected DataGrid GetDataGrid();
        abstract public Tuple<bool, string> Validate(Tuple<object, bool> obj);
        abstract public bool Constructor(StackPanel element);

        virtual public void Add()
        {
            DataGrid dataGrid = GetDataGrid();

            object newObject = Activator.CreateInstance(GetManagedType());
            bool? result = new AddEditPopUp(newObject, Constructor, Validate).ShowDialog();

            if (!(bool)result)
                return;

            var newList = dataGrid.ItemsSource.Cast<object>().ToList();
            newList.Insert(0, newObject);
            dataGrid.ItemsSource = newList;
            db.GetAccessPoint().Add(newObject);
        }

        virtual public void Edit()
        {
            DataGrid dataGrid = GetDataGrid();
            var list = dataGrid.SelectedItems;
            if (list.Count == 0)
            {
                MessageBox.Show("Выделите строчку для изменения");
                return;
            }

            bool? result = new AddEditPopUp(list[0], Constructor, Validate).ShowDialog();


            if (!(bool)result)
            {
                db.GetAccessPoint().ChangeTracker.Entries().ToList().ForEach(ent => ent.Reload());
                return;
            }
                

           // var newList = dataGrid.ItemsSource.Cast<object>().ToList();
            //newList.Insert(0, newObject);
            //..dataGrid.ItemsSource = newList;
            db.GetAccessPoint().SaveChanges();
        }

        virtual public void Delete()
        {
            DataGrid dataGrid = GetDataGrid();
            var list = dataGrid.SelectedItems;
            if(list.Count == 0)
            {
                MessageBox.Show("Выделите строчку для удаления");
                return;
            }

            var objectForDeleting = list[0];
            var validationResult = Validate(new Tuple<object, bool>(objectForDeleting, false));

            if (!validationResult.Item1)
            {
                MessageBox.Show(validationResult.Item2);
                return;
            }

            db.GetAccessPoint().Remove(objectForDeleting);

            var newList = dataGrid.ItemsSource.Cast<object>().ToList();
            newList.Remove(objectForDeleting);
            dataGrid.ItemsSource = newList;
        }
    }
}
