using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace wsrSpeedrun.Management
{
    public abstract class Model : IModel
    {
        protected Entities db;
        public Model CreateBase(Entities entities)
        {
            Model m = (Model)Activator.CreateInstance(GetType());
            m.db = entities;
            return m;
        }

        protected UIElement CreateBindingTextField(string label, string path)
        {
            TextBox textBox = new TextBox();
            textBox.SetBinding(TextBox.TextProperty, new Binding(path));
            return LabelElement(textBox, label);
        }

        protected UIElement LabelElement(UIElement element, string labelText)
        {
            Label label = new Label()
            {
                Content = labelText
            };

            DockPanel dockPanel = new DockPanel();

            DockPanel.SetDock(element, Dock.Right);
            DockPanel.SetDock(label, Dock.Left);

            dockPanel.Children.Add(label);
            dockPanel.Children.Add(element);

            return dockPanel;
        }

        public abstract IModel CreateInstance();
        public abstract IModel Factory(object obj);
        public abstract void Remove();
        public abstract void Save();
        public abstract void Conctruct(StackPanel panel);
    }

    public interface IModel
    {
        void Save();
        void Remove();
        void Conctruct(StackPanel panel);

        IModel CreateInstance();
        IModel Factory(object obj);
    }
}
