using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace creationOfOneToOneRelationsWtihPopUp.Management
{
    public interface IManagedTypeModel
    {
        IManagedTypeModel Factory(object obj, testingEntities db);
        IManagedTypeModel CreateNewInstance(testingEntities db);

        bool Constructor(StackPanel panel);
        Tuple<bool, string> CheckItSelf();
        int Save();
    }
}
