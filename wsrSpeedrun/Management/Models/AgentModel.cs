using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace wsrSpeedrun.Management.Models
{
    public class AgentModel : Model
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public double Share { get; set; }


        public override void Conctruct(StackPanel panel)
        {
            panel.Children.Add(CreateBindingTextField("Name", "Name"));
            panel.Children.Add(CreateBindingTextField("MiddleName", "Middlename"));
            panel.Children.Add(CreateBindingTextField("LastName", "Lastname"));
            panel.Children.Add(CreateBindingTextField("Deal share", "Share"));
        }

        public override IModel CreateInstance()
        {
            return new AgentModel() { db = db, Id = 0, Lastname = "", Middlename = "", Name = "", Share = 0 };
        }

        public override IModel Factory(object obj)
        {
            try
            {
                Agent agent = (Agent)obj;
                return new AgentModel() { db = db, Id = agent.Id, Lastname = agent.Lastname, Middlename = agent.Middlename, Name = agent.Name, Share = agent.Dealshare };
            }
            catch (Exception)
            {
                return CreateInstance();
            }
        }

        public override void Remove()
        {
            db.Agents.Remove(db.Agents.Find(new object[] { Id }));
            db.SaveChanges();
        }

        public override void Save()
        {
            if (string.IsNullOrEmpty(Name) ||
                string.IsNullOrEmpty(Middlename) ||
                string.IsNullOrEmpty(Lastname))
                throw new Exception("Агент обязан иметь полное имя");

            if (Share > 100)
                throw new Exception("Доля не может быть больше ста");

            if (Share < 0)
                throw new Exception("Доля не может быть отрицательной");

            if (Id == 0)
            {
                Agent agent = new Agent()
                {
                    Id = 0,
                    Name = Name,
                    Middlename = Middlename,
                    Lastname = Lastname,
                    Dealshare = Share
                };

                db.Agents.Add(agent);
            }
            else
            {
                Agent agent = db.Agents.Find(new object[] { Id });
                agent.Name = Name;
                agent.Middlename = Middlename;
                agent.Lastname = Lastname;
                agent.Dealshare = Share;
            }


            db.SaveChanges();
        }
    }
}
