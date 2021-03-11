using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace wsrSpeedrun.Management.Models
{
    public class ClientModel : Model
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }


        public override void Conctruct(StackPanel panel)
        {
            panel.Children.Add(CreateBindingTextField("Name", "Name"));
            panel.Children.Add(CreateBindingTextField("MiddleName", "Middlename"));
            panel.Children.Add(CreateBindingTextField("LastName", "Lastname"));
            panel.Children.Add(CreateBindingTextField("Email", "Email"));
            panel.Children.Add(CreateBindingTextField("Phone", "Phone"));
        }

        public override IModel CreateInstance()
        {
            return new ClientModel() { db = db, Id = 0, Lastname = "", Middlename = "", Name = "", Email = "", Phone = "" };
        }

        public override IModel Factory(object obj)
        {
            try
            {
                Client client = (Client)obj;
                return new ClientModel() { db = db, Id = client.Id, Lastname = client.Lastname, Middlename = client.Middlename, Name = client.Name, Email = client.Email, Phone = client.Phone };
            }
            catch (Exception)
            {
                return CreateInstance();
            }
        }

        public override void Remove()
        {
            db.Clients.Remove(db.Clients.Find(new object[] { Id }));
            db.SaveChanges();
        }

        public override void Save()
        {
            if (string.IsNullOrEmpty(Phone) &&
                string.IsNullOrEmpty(Email))
                throw new Exception("Клиент обязан иметь телефон или почту, которая електронная btw");

            if (Id == 0)
            {
                Client client = new Client()
                {
                    Id = 0,
                    Name = Name,
                    Middlename = Middlename,
                    Lastname = Lastname,
                    Phone = Phone,
                    Email = Email
                };

                db.Clients.Add(client);
            }
            else
            {
                Client client = db.Clients.Find(new object[] { Id });
                client.Name = Name;
                client.Middlename = Middlename;
                client.Lastname = Lastname;
                client.Phone = Phone;
                client.Email = Email;
            }


            db.SaveChanges();
        }
    }
}
