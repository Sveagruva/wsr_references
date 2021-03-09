using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace creationOfOneToOneRelationsWtihPopUp.Management.Models
{
    public class CocosPackageModel : IManagedTypeModel
    {
        public int age { set; get; }
        public double weight { set; get; }
        public int? id { get; set; }
        public int? cocoId { get; set; }
        private testingEntities db { set; get; }

        public bool Constructor(StackPanel panel)
        {
            // got em
            TextBox broke = new TextBox();
            broke.SetBinding(TextBox.TextProperty, new Binding("age"));
            panel.Children.Add(LabelElement(broke, "age"));

            broke = new TextBox();
            broke.SetBinding(TextBox.TextProperty, new Binding("weight"));
            panel.Children.Add(LabelElement(broke, "weighh b-word"));


            return true;
        }

        static private UIElement LabelElement(UIElement element, String labelText)
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

        public Tuple<bool, string> CheckItSelf()
        {
            if(weight < 0)
            {
                return new Tuple<bool, string>(false, "negative weight lol");
            }

            return new Tuple<bool, string>(true, "ok");
        }

        public int Save()
        {
            if(id != null)
            {

                return (int)id;
            }


            Coco co = new Coco();
            co.Weight = weight;
            db.Cocos.Add(co);
            db.SaveChanges();

            CocosPackage pc = new CocosPackage();
            pc.age = age;
            pc.CocosId = co.Id;
            pc.isFinished = false;
            db.CocosPackages.Add(pc);

            db.SaveChanges();
            return pc.Id;
        }

        public IManagedTypeModel Factory(object obj, testingEntities db)
        {
            CocosPackage f;
            try
            {
                f = obj as CocosPackage;
            }
            catch (Exception)
            {
                return CreateNewInstance(db);
            }

            if (f.Id == 0)
                return CreateNewInstance(db);

            return new CocosPackageModel { age = f.age, cocoId = f.CocosId, id = f.Id, weight = f.Coco.Weight, db = db };
        }

        public IManagedTypeModel CreateNewInstance(testingEntities db)
        {
            return new CocosPackageModel { age = 0, cocoId = null, id = null, weight = 0, db = db };
        }
    }
}
