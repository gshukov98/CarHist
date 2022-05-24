using System.Runtime.Serialization;
using Elders.Cronus;

namespace CarHist.Cars;

[DataContract(Namespace = BC.CarHist, Name = "1ba0cfe7-8f82-4b7f-9b03-c9b39fd3d01b")]
public class CarId : AggregateRootId<CarId>
{
    private CarId() { }

    public CarId(string id, string tenant) : base(id, "car", tenant) { }

    protected override CarId Construct(string id, string tenant) => new CarId(id, tenant);
}
