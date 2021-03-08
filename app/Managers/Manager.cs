using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace app.Managers
{
    public abstract class Manager<managedType> : Page, IManager
        where managedType : class
    {
        public readonly static Type managerType = typeof(managedType);
        protected SpeedrunEntities db = new SpeedrunEntities();

        protected virtual managedType CreateManagedInstance() => Activator.CreateInstance<managedType>();

        public void Add()
        {
            DataGrid dataGrid = GetDataGrid();

            managedType newObject = CreateManagedInstance();
            bool? result = new AddEditPopUp(newObject, Constructor, Validate).ShowDialog();

            if (!(bool)result)
                return;

            var newList = dataGrid.ItemsSource.Cast<managedType>().ToList();
            newList.Insert(0, newObject);
            dataGrid.ItemsSource = newList;

            db.Set<managedType>().Add(newObject);
        }

        public void Edit()
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
                db.ChangeTracker.Entries().ToList().ForEach(ent => ent.Reload());
                dataGrid.ItemsSource = db.Set<managedType>().ToList();
                return;
            }

            db.SaveChanges();
        }

        public void Delete()
        {
            DataGrid dataGrid = GetDataGrid();
            var list = dataGrid.SelectedItems;
            if(list.Count == 0)
            {
                MessageBox.Show("Выделите строчку для удаления");
                return;
            }

            managedType objectForDeleting = (managedType)list[0];
            var validationResult = Validate(new Tuple<object, bool>(objectForDeleting, false));

            if (!validationResult.Item1)
            {
                MessageBox.Show(validationResult.Item2);
                return;
            }

            db.Set<managedType>().Remove(objectForDeleting);

            var newList = dataGrid.ItemsSource.Cast<managedType>().ToList();
            newList.Remove(objectForDeleting);
            dataGrid.ItemsSource = newList;
        }

        public abstract DataGrid GetDataGrid();
        public abstract Tuple<bool, string> Validate(Tuple<object, bool> obj);
        // TODO use validate action as parameter in Tuple<bool, string> Validate(Tuple<object, bool> obj);
        public enum ValidateAction { Add, Edit, Remove }

        protected abstract bool Constructor(StackPanel element);
    }

    public interface IManager
    {
        DataGrid GetDataGrid();
        Tuple<bool, string> Validate(Tuple<object, bool> obj);
        void Delete();
        void Edit();
        void Add();
    }
}
