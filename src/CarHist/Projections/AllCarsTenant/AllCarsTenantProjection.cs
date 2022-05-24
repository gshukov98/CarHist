using System.Runtime.Serialization;
using CarHist.Cars;
using CarHist.Cars.Events;
using Elders.Cronus;
using Elders.Cronus.Projections;

namespace CarHist.Projections.AllCarsTenant;

[DataContract(Namespace = BC.CarHist, Name = "3b4f8226-8dfc-4b4e-9ff2-96b2f1b3a300")]
public class AllCarsTenantProjection : ProjectionDefinition<AllCarsTenantProjection.Data, AllCarsByTenantId>, IProjection,
    IEventHandler<CarCreated>
{
    public AllCarsTenantProjection()
    {
        Subscribe<CarCreated>(x => new AllCarsByTenantId(x.Id.Tenant));
    }

    public void Handle(CarCreated @event)
    {
        State.Cars.Add(new CarData(@event.Id, @event.Make, @event.Model, @event.VIN, @event.EngineType));
    }

    [DataContract(Namespace = BC.CarHist, Name = "3c9d5503-1914-4a56-8018-edb8e3a97494")]
    public class Data
    {
        public Data()
        {
            Cars = new HashSet<CarData>();
        }

        [DataMember(Order = 1)]
        public HashSet<CarData> Cars { get; private set; }
    }

    [DataContract(Namespace = BC.CarHist, Name = "e177295a-a6b9-4129-8ee0-3bc16bee2c19")]
    public class CarData
    {
        private CarData() { }

        public CarData(CarId id, string make, string model, string vIN, string engineType)
        {
            Id = id;
            Make = make;
            Model = model;
            VIN = vIN;
            EngineType = engineType;
        }

        [DataMember(Order = 1)]
        public CarId Id { get; set; }

        [DataMember(Order = 2)]
        public string Make { get; set; }

        [DataMember(Order = 3)]
        public string Model { get; set; }

        [DataMember(Order = 4)]
        public string VIN { get; set; }

        [DataMember(Order = 5)]
        public string EngineType { get; set; }
    }
}
