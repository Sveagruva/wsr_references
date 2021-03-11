using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace wsrSpeedrun.Management
{
    public abstract class Manager<model> : Page, IManager
        where model : Model
    {
        protected abstract DataGrid GetDataGrid();
        protected Entities db = new Entities();

        private model CreateModel()
        {
            return (model)Activator.CreateInstance<model>().CreateBase(db);
        }

        public void Add()
        {
            model instance = CreateModel();
            new ManageInstaceWindow(instance.CreateInstance()).ShowDialog();
            UpdateDispayable();
        }

        public void Edit()
        {
            object selected = GetDataGrid().SelectedItem;

            if (selected == null)
            {
                MessageBox.Show("Выделите объект");
                return;
            }

            model instance = CreateModel();
            new ManageInstaceWindow(instance.Factory(selected)).ShowDialog();
            UpdateDispayable();
        }

        public abstract void UpdateDispayable();

        public void Remove()
        {
            object selected = GetDataGrid().SelectedItem;

            if (selected == null)
            {
                MessageBox.Show("Выделите объект");
                return;
            }

            model instance = (model)Activator.CreateInstance(typeof(model), new object[] { db });
            try
            {
                instance.Remove();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }
    }

    public interface IManager
    {
        void Add();
        void Edit();
        void Remove();
    }
}
