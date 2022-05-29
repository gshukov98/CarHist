using System.Runtime.Serialization;
using Elders.Cronus;

namespace CarHist.Cars.Events;

[DataContract(Namespace = BC.CarHist, Name = "1d016b4f-426c-4b11-a5b9-43b5432dd4f4")]
public class CarDeleted : IEvent
{
    CarDeleted() { }

    public CarDeleted(CarId id, DateTimeOffset timestamp)
    {
        Id = id;
        Timestamp = timestamp;
    }

    [DataMember(Order = 1)]
    public CarId Id { get; private set; }

    [DataMember(Order = 2)]
    public DateTimeOffset Timestamp { get; private set; }
}
