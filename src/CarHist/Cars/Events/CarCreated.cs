using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Elders.Cronus;

namespace CarHist.Cars.Events
{
    [DataContract(Namespace = BC.CarHist, Name = "e9319351-0d47-4bfb-ac8d-be0cff365751")]
    public class CarCreated : IEvent
    {
        private CarCreated() { }

        public CarCreated(CarId id, string make, string model, string vIN, string engineType, DateTimeOffset timestamp)
        {
            Id = id;
            Make = make;
            Model = model;
            VIN = vIN;
            EngineType = engineType;
            Timestamp = timestamp;
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

        [DataMember(Order = 6)]
        public DateTimeOffset Timestamp { get; private set; }
    }
}
