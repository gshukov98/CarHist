using System.Runtime.Serialization;
using Elders.Cronus;

namespace CarHist.Cars.Events;

[DataContract(Namespace = BC.CarHist, Name = "10351a6b-7bf8-44e1-9abf-16d3fa3199bd")]
public class HistoryAppended : IEvent
{
    HistoryAppended() { }

    public HistoryAppended(CarId id, string type, string description, string company, DateTimeOffset timestamp)
    {
        Id = id;
        Type = type;
        Description = description;
        Company = company;
        Timestamp = timestamp;
    }

    [DataMember(Order = 1)]
    public CarId Id { get; private set; }

    [DataMember(Order = 2)]
    public string Type { get; private set; }

    [DataMember(Order = 3)]
    public string Description { get; private set; }

    [DataMember(Order = 4)]
    public string Company { get; private set; }

    [DataMember(Order = 5)]
    public DateTimeOffset Timestamp { get; private set; }
}
