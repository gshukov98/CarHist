using System.Runtime.Serialization;

namespace CarHist.Cars;

[DataContract(Namespace = BC.CarHist, Name = "a0cdbb91-7053-4284-9250-45c8fdfc62de")]
public class CarHistory
{
    CarHistory() { }

    public CarHistory(string type, string description, string company, DateTimeOffset timestamp)
    {
        Type = type;
        Description = description;
        Company = company;
        Timestamp = timestamp;
    }

    [DataMember(Order = 1)]
    public string Type { get; set; }

    [DataMember(Order = 2)]
    public string Description { get; set; }

    [DataMember(Order = 3)]
    public string Company { get; set; }

    [DataMember(Order = 4)]
    public DateTimeOffset Timestamp { get; set; }
}
