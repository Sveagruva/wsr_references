using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace wsrSpeedrun.Management.Models
{
    /// <summary>
    /// a mess
    /// </summary>
    public class DemandModel : Model
        // there is a lot of copy pasted code that I didn't feel like making new function but I should, sorry
    {
        public int Id { get; set; }
        public double? Longtitude { get; set; }
        public double? Lantitude { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Adress { get; set; }
        public int? FlatNumber { get; set; }

        public enum DemandType { land, house, flat}
        public DemandType demandType { get; set; }

        public int referenceId { get; set; }

        public int? FlatFloors { get; set; }
        public int? FlatRooms { get; set; }
        public double? FlatArea { get; set; }

        public double? LandArea { get; set; }

        public int? HouseFloors { get; set; }
        public int? HouseRooms { get; set; }
        public double? HouseArea { get; set; }

        public override void Conctruct(StackPanel panel)
        {
            panel.Children.Add(CreateBindingTextField("Широта", "Longtitude"));
            panel.Children.Add(CreateBindingTextField("Долгота", "Lantitude"));
            panel.Children.Add(CreateBindingTextField("Город", "City"));
            panel.Children.Add(CreateBindingTextField("Улица", "Street"));
            panel.Children.Add(CreateBindingTextField("Номер дома", "Adress"));
            panel.Children.Add(CreateBindingTextField("Номер квартиры", "FlatNumber"));



            List<StackPanel> panels = new List<StackPanel>();

            StackPanel housePanel = new StackPanel();
            housePanel.Children.Add(CreateBindingTextField("Количество этажей", "HouseFloors"));
            housePanel.Children.Add(CreateBindingTextField("Количество комнат", "HouseRooms"));
            housePanel.Children.Add(CreateBindingTextField("Площадь", "HouseArea"));
            panels.Add(housePanel);

            StackPanel flatPanel = new StackPanel();
            flatPanel.Children.Add(CreateBindingTextField("Этаж", "FlatFloors"));
            flatPanel.Children.Add(CreateBindingTextField("Количество комнат", "FlatRooms"));
            flatPanel.Children.Add(CreateBindingTextField("Площадь", "FlatArea"));
            panels.Add(flatPanel);

            StackPanel landPanel = new StackPanel();
            landPanel.Children.Add(CreateBindingTextField("Площадь", "LandArea"));
            panels.Add(landPanel);

            ComboBox types = new ComboBox();
            types.ItemsSource = new List<string>() { "Земля", "Дом", "Квартира" };
            types.Tag = panels;
            types.SelectedIndex = 0;
            types.SelectionChanged += ChangeType;

            switch (demandType)
            {
                case DemandType.house:
                    types.SelectedIndex = 1;
                    landPanel.Visibility = Visibility.Collapsed;
                    flatPanel.Visibility = Visibility.Collapsed;
                    break;
                case DemandType.flat:
                    types.SelectedIndex = 2;
                    housePanel.Visibility = Visibility.Collapsed;
                    landPanel.Visibility = Visibility.Collapsed;
                    break;
                case DemandType.land:
                    types.SelectedIndex = 0;
                    housePanel.Visibility = Visibility.Collapsed;
                    flatPanel.Visibility = Visibility.Collapsed;
                    break;
            }


            panel.Children.Add(types);

            panel.Children.Add(housePanel);
            panel.Children.Add(flatPanel);
            panel.Children.Add(landPanel);
        }

        private void ChangeType(object sender, RoutedEventArgs e)
        {
            ComboBox tubox = (ComboBox)sender;
            List<StackPanel> panels = (List<StackPanel>)tubox.Tag;
            StackPanel housePanel = panels[0];
            StackPanel flatPanel = panels[1];
            StackPanel landPanel = panels[2];

            switch (tubox.SelectedItem as string)
            {
                case "Земля":
                    demandType = DemandType.land;
                    housePanel.Visibility = Visibility.Collapsed;
                    flatPanel.Visibility = Visibility.Collapsed;
                    landPanel.Visibility = Visibility.Visible;
                    break;
                case "Дом":
                    demandType = DemandType.house;
                    landPanel.Visibility = Visibility.Collapsed;
                    flatPanel.Visibility = Visibility.Collapsed;
                     housePanel.Visibility = Visibility.Visible;
                    break;
                case "Квартира":
                    demandType = DemandType.flat;
                    housePanel.Visibility = Visibility.Collapsed;
                     landPanel.Visibility = Visibility.Collapsed;
                    flatPanel.Visibility = Visibility.Visible;
                    break;
            }
        }

        public override IModel CreateInstance()
        {
            return new DemandModel() {
                Id = 0,
                db = db,
                Longtitude = null,
                Lantitude = null,
                City = null,
                Street = null,
                Adress = null,
                FlatNumber = null,
                demandType = DemandType.flat,
                FlatFloors = null,
                FlatRooms = null,
                FlatArea = null,
                LandArea = null,
                HouseFloors = null,
                HouseRooms = null,
                HouseArea = null
            };
        }

        public override IModel Factory(object obj)
        {
            try
            {
                Demand demand = (Demand)obj;
                DemandType type = DemandType.house;
                int rid = 0;

                if (demand.Lands.Count != 0)
                {
                    rid = demand.Lands.First().Id;
                    type = DemandType.land;
                }
                    

                if (demand.Flats.Count != 0)
                {
                    rid = demand.Flats.First().Id;
                    type = DemandType.flat;
                }
                    

                if (demand.Houses.Count != 0)
                {
                    rid = demand.Houses.First().Id;
                    type = DemandType.house;
                }
                    

                return new DemandModel()
                {
                    Id = demand.Id,
                    db = db,
                    Longtitude = demand.Longutude,
                    Lantitude = demand.Latitude,
                    City = demand.City,
                    Street = demand.Street,
                    Adress = demand.Adress,
                    FlatNumber = demand.Flatnumber,
                    demandType = type,
                    FlatFloors = demand.Flats.Count == 0 ? null : demand.Flats.First().Floor,
                    FlatRooms = demand.Flats.Count == 0 ? null : demand.Flats.First().Roomscount,
                    FlatArea = demand.Flats.Count == 0 ? null : demand.Flats.First().Area,
                    LandArea = demand.Lands.Count == 0 ? null : demand.Lands.First().Area,
                    HouseFloors = demand.Houses.Count == 0 ? null : demand.Houses.First().Floorscount,
                    HouseRooms = demand.Houses.Count == 0 ? null : demand.Houses.First().Roomscount,
                    HouseArea = demand.Houses.Count == 0 ? null : demand.Houses.First().Area,
                    referenceId = rid
                };

            }
            catch (Exception)
            {
                return CreateInstance();
                throw;
            }
        }

        public override void Remove()
        {
            Demand demand = db.Demands.Find(new object[] { Id });
            DemandType? type = null;
            if (demand.Lands.Count != 0)
                type = DemandType.land;

            if (demand.Flats.Count != 0)
                type = DemandType.flat;

            if (demand.Houses.Count != 0)
                type = DemandType.house;

            if (type == null)
                return;


            switch (type)
            {
                case DemandType.flat:
                    db.Flats.Remove(demand.Flats.First());
                    break;
                case DemandType.house:
                    db.Houses.Remove(demand.Houses.First());
                    break;
                case DemandType.land:
                    db.Lands.Remove(demand.Lands.First());
                    break;
            }

            db.Demands.Remove(demand);
            db.SaveChanges();
        }

        public override void Save()
        {
            if(Longtitude > 90)
            {
                MessageBox.Show("Широта не может быть больше 90");
                return;
            }

            if (Longtitude < -90)
            {
                MessageBox.Show("Широта не может быть меньше -90");
                return;
            }

            if (Lantitude > 180)
            {
                MessageBox.Show("Долгота не может быть больше 180");
                return;
            }

            if (Lantitude < -180)
            {
                MessageBox.Show("Долгота не может быть меньше -180");
                return;
            }


            if(Id == 0)
            {
                Demand demand = new Demand()
                {
                    Id = Id,
                    Longutude = Longtitude,
                    Latitude = Lantitude,
                    City = City,
                    Street = Street,
                    Adress = Adress,
                    Flatnumber = FlatNumber
                };

                db.Demands.Add(demand);
                db.SaveChanges();

                switch (demandType)
                {
                    case DemandType.flat:
                        Flat flat = new Flat()
                        {
                            Area = FlatArea,
                            DemandId = demand.Id,
                            Roomscount = FlatRooms,
                            Floor = FlatFloors,
                            Id = 0
                        };

                        db.Flats.Add(flat);
                        break;
                    case DemandType.house:
                        House house = new House()
                        {
                            Id = 0,
                            Area = HouseArea,
                            DemandId = demand.Id,
                            Floorscount = HouseFloors,
                            Roomscount = HouseRooms
                        };

                        db.Houses.Add(house);
                        break;
                    case DemandType.land:
                        Land land = new Land()
                        {
                            Area = LandArea,
                            DemandId = demand.Id,
                            Id = 0
                        };

                        db.Lands.Add(land);
                        break;
                }
            }
            else
            {
                Demand demand = db.Demands.Find(new object[] { Id });
                DemandType? old = null;
                if (demand.Lands.Count != 0)
                    old = DemandType.land;

                if (demand.Flats.Count != 0)
                    old = DemandType.flat;

                if (demand.Houses.Count != 0)
                    old = DemandType.house;

                if (old == null)
                    return;

                demand.Adress = Adress;
                demand.City = City;
                demand.Flatnumber = FlatNumber;
                demand.Latitude = Lantitude;
                demand.Longutude = Longtitude;
                demand.Street = Street;

                if(old != demandType)
                {
                    switch (old)
                    {
                        case DemandType.flat:
                            db.Flats.Remove(demand.Flats.First());
                            break;
                        case DemandType.house:
                            db.Houses.Remove(demand.Houses.First());
                            break;
                        case DemandType.land:
                            db.Lands.Remove(demand.Lands.First());
                            break;
                    }

                    switch (demandType)
                    {
                        case DemandType.flat:
                            Flat flat = new Flat()
                            {
                                Id = 0,
                                DemandId = demand.Id,
                                Area = FlatArea,
                                Floor = FlatFloors,
                                Roomscount = FlatRooms
                            };

                            db.Flats.Add(flat);
                            break;
                        case DemandType.house:
                            House house = new House()
                            {
                                Id = 0,
                                DemandId = demand.Id,
                                Area = HouseArea,
                                Roomscount = HouseRooms,
                                Floorscount = HouseFloors
                            };

                            db.Houses.Add(house);
                            break;
                        case DemandType.land:
                            Land land = new Land()
                            {
                                Id = 0,
                                DemandId = demand.Id,
                                Area = LandArea
                            };

                            db.Lands.Add(land);
                            break;
                    }
                }
                else
                {
                    switch (demandType)
                    {
                        case DemandType.flat:
                            demand.Flats.First().Area = FlatArea;
                            demand.Flats.First().Floor = FlatFloors;
                            demand.Flats.First().Roomscount = FlatRooms;
                            break;
                        case DemandType.house:
                            demand.Houses.First().Area = HouseArea;
                            demand.Houses.First().Floorscount = HouseFloors;
                            demand.Houses.First().Roomscount = HouseRooms;
                            break;
                        case DemandType.land:
                            demand.Lands.First().Area = LandArea;
                            break;
                    }
                }

                db.SaveChanges();
            }
        }
    }
}
