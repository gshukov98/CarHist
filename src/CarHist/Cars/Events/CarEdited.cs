using System.Runtime.Serialization;
using Elders.Cronus;

namespace CarHist.Cars.Events;

[DataContract(Namespace = BC.CarHist, Name = "7f43e019-2a33-4825-b15a-0664c75eb9bf")]
public class CarEdited : IEvent
{
    CarEdited() { }

    public CarEdited(CarId id, string make, string model, string vin, string engineType, DateTimeOffset timestamp)
    {
        Id = id;
        Make = make;
        Model = model;
        VIN = vin;
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
