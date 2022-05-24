using System.Runtime.Serialization;
using Elders.Cronus;

namespace CarHist.Cars.Events;

[DataContract(Namespace = BC.CarHist, Name = "10351a6b-7bf8-44e1-9abf-16d3fa3199bd")]
public class HistoryAdded : IEvent
{
    HistoryAdded() { }

    public HistoryAdded(CarId id, string type, string description, DateTimeOffset timestamp)
    {
        Id = id;
        Type = type;
        Description = description;
        Timestamp = timestamp;
    }

    [DataMember(Order = 1)]
    public CarId Id { get; private set; }

    [DataMember(Order = 2)]
    public string Type { get; private set; }

    [DataMember(Order = 3)]
    public string Description { get; private set; }

    [DataMember(Order = 4)]
    public DateTimeOffset Timestamp { get; private set; }
}
