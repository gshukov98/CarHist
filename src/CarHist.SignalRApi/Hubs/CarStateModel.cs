using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarHist.Cars;

namespace CarHist.SignalRApi.Hubs
{
    public class CarStateModel
    {
        public CarStateModel(CarId id, string name)
        {
            Id = id;
            Name = name;
        }

        public CarId Id { get; set; }

        public string Name { get; set; }
    }
}
