using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Elders.Cronus;

namespace CarHist.Cars.Commands
{
    [DataContract(Namespace = BC.CarHist, Name = "58e78b17-27de-498d-b37a-1699853fdae8")]
    public class CreateCar : ICommand
    {
        private CreateCar() { }

        public CreateCar(CarId id, string make, string model, string vIN, string engineType)
        {
            Id = id;
            Make = make;
            Model = model;
            VIN = vIN;
            EngineType = engineType;
        }

        [DataMember(Order = 1)]
        public CarId Id { get; private set; }

        [DataMember(Order = 2)]
        public string Make { get; private set; }

        [DataMember(Order = 3)]
        public string Model { get; private set; }

        [DataMember(Order = 4)]
        public string VIN { get; private set; }

        [DataMember(Order = 5)]
        public string EngineType { get; private set; }
    }
}
