using System.Runtime.Serialization;
using CarHist.Cars.Commands;
using Elders.Cronus;
using Microsoft.Extensions.Logging;

namespace CarHist.Cars;

[DataContract(Namespace = BC.CarHist, Name = "a4d1b415-da8f-400f-9b1d-9c92f497e9c7")]
public class CarAppService : ApplicationService<Car>,
    ICommandHandler<CreateCar>,
    ICommandHandler<EditCar>,
    ICommandHandler<AppendHistory>
{
    private readonly ILogger<CarAppService> _logger;

    public CarAppService(IAggregateRepository repository, ILogger<CarAppService> logger) : base(repository)
    {
        _logger = logger;
    }

    public void Handle(CreateCar command)
    {
        ReadResult<Car> result = repository.Load<Car>(command.Id);

        if (result.NotFound)
        {
            Car car = new Car(command.Id, command.Make, command.Model, command.VIN, command.EngineType);
            repository.Save(car);
        }
    }

    public void Handle(EditCar command)
    {
        ReadResult<Car> result = repository.Load<Car>(command.Id);

        if (result.IsSuccess)
        {
            Car car = result.Data;
            car.EditCar(command.Id, command.Make, command.Model, command.VIN, command.EngineType);
            repository.Save(car);
        }
        else if (result.HasError)
        {
            _logger.Error(() => $"Trying to edit missing aggregate id: {command.Id}");
        }
    }

    public void Handle(AppendHistory command)
    {
        ReadResult<Car> result = repository.Load<Car>(command.Id);

        if (result.IsSuccess)
        {
            Car car = result.Data;
            car.AppendHistory(command.Id, command.Type, command.Description, command.Company);
            repository.Save(car);
        }
        else if (result.HasError)
        {
            _logger.Error(() => $"Trying to edit missing aggregate id: {command.Id}");
        }
    }
}
