using creationOfOneToOneRelationsWtihPopUp.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace creationOfOneToOneRelationsWtihPopUp
{
    public abstract class Manager<model> : Page, IManager
        where model : IManagedTypeModel
    {

        protected testingEntities db = new testingEntities();
        public void Add()
        {
            model md = Activator.CreateInstance<model>();
            PopUp popUp = new PopUp(md.CreateNewInstance(db));

            popUp.ShowDialog();
        }
    }

    interface IManager
    {
        void Add();
        // ...
    }
}
