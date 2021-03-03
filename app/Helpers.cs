using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace app
{
    static class Helpers
    {
        static public UIElement LabelElement(UIElement element, String labelText)
        {
            DockPanel panel = new DockPanel();
            Label label = new Label();
            label.Content = labelText;
            DockPanel.SetDock(label, Dock.Left);
            DockPanel.SetDock(element, Dock.Right);
            panel.Children.Add(label);
            panel.Children.Add(element);
            return panel;
        }
    }
}
