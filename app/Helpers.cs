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
            Label label = new Label
            {
                Content = labelText
            };


            DockPanel panel = new DockPanel();

            DockPanel.SetDock(label, Dock.Left);
            DockPanel.SetDock(element, Dock.Right);

            // NOTE label must be added first for element to take remaining space
            panel.Children.Add(label);
            panel.Children.Add(element);
            return panel;
        }
    }
}
