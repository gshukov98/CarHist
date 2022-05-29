using System.Runtime.Serialization;

namespace CarHist.Projections.HistoryByCar;

[DataContract(Namespace = BC.CarHist, Name = "86cd2c09-45b9-4eed-baf7-7c8a77a70d5f")]
public class CarHistoryPojectionModel
{
    CarHistoryPojectionModel() { }

    public CarHistoryPojectionModel(string type, string description, string company, DateTimeOffset timestamp)
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

