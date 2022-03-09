using System.Runtime.Serialization;
using CarHist.Cars.Commands;
using Elders.Cronus;

namespace CarHist.Cars
{
    [DataContract(Namespace = BC.CarHist, Name = "a4d1b415-da8f-400f-9b1d-9c92f497e9c7")]
    public class CarAppService : ApplicationService<Car>,
        ICommandHandler<CreateCar>
    {
        public CarAppService(IAggregateRepository repository) : base(repository) { }

        public void Handle(CreateCar command)
        {
            ReadResult<Car> result = repository.Load<Car>(command.Id);

            if (result.NotFound)
            {
                Car car = new Car(command.Id, command.Make, command.Model, command.VIN, command.EngineType);
                repository.Save(car);
            }
        }
    }
}
