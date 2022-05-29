using System.Runtime.Serialization;
using Elders.Cronus;

namespace CarHist.Cars.Commands;

[DataContract(Namespace = BC.CarHist, Name = "b718eb5b-c135-48a5-943e-e5b5f24a6527")]
public class DeleteCar : ICommand
{
    DeleteCar() { }

    public DeleteCar(CarId id)
    {
        Id = id;
    }

    [DataMember(Order = 1)]
    public CarId Id { get; private set; }

}
