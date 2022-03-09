using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarHist.Cars.Events;
using Elders.Cronus;

namespace CarHist.Cars
{
    public class Car : AggregateRoot<CarState>
    {
        private Car() { }

        public Car(CarId id, string make, string model, string vIN, string engineType)
        {
            var createCar = new CarCreated(id, make, model, vIN, engineType, DateTimeOffset.UtcNow);
            Apply(createCar);
        }
    }

    public class CarState : AggregateRootState<Car, CarId>
    {
        public CarState() { }

        public override CarId Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string VIN { get; set; }

        public string EngineType { get; set; }

        public void When(CarCreated e)
        {
            Id = e.Id;
            Make = e.Make;
            Model = e.Model;
            VIN = e.VIN;
            EngineType = e.EngineType;
        }
    }
}
