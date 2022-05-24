using System.Runtime.Serialization;
using Elders.Cronus;

namespace CarHist.Cars.Commands;

[DataContract(Namespace = BC.CarHist, Name = "805999fa-d6ad-490e-8b24-f803a150fab6")]
public class AppendHistory : ICommand
{
    AppendHistory() { }

    public AppendHistory(CarId id, string type, string description, string company)
    {
        Id = id;
        Type = type;
        Description = description;
        Company = company;
    }

    [DataMember(Order = 1)]
    public CarId Id { get; private set; }

    [DataMember(Order = 2)]
    public string Type { get; private set; }

    [DataMember(Order = 3)]
    public string Description { get; private set; }

    [DataMember(Order = 4)]
    public string Company { get; private set; }
}
