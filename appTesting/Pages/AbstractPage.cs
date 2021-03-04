using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace appTesting.Pages
{
    public abstract class AbstractPage : Page
    {
        public abstract bool isCool();

        // to create cool page you need:
        // first to create abstract class that inherits Page
        // second create page and change chema root element to local:AbstractClassName
        // third inherit page generated class from your abstract class and not from Page

    }
}
