using System.Runtime.Serialization;
using Elders.Cronus;

namespace CarHist.Cars.Commands;

[DataContract(Namespace = BC.CarHist, Name = "e68e2f82-cf6e-40a5-b32f-418a2114c173")]
public class EditCar : ICommand
{
    EditCar() { }

    public EditCar(CarId id, string make, string model, string vIN, string engineType)
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
