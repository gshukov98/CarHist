﻿using CarHist.Cars.Events;
using Elders.Cronus;

namespace CarHist.Cars;

//TODO: Should add more info about car (make, model, body type, production year, engine volume, engine power, fuel type, transmision type)
public class Car : AggregateRoot<CarState>
{
    Car() { }

    public Car(CarId id, string make, string model, string vin, string engineType)
    {
        Guard(id, make, model, vin, engineType);

        var createCar = new CarCreated(id, make, model, vin, engineType, DateTimeOffset.UtcNow);
        Apply(createCar);
    }

    public void EditCar(CarId id, string make, string model, string vin, string engineType)
    {
        Guard(id, make, model, vin, engineType);

        if (state.Make.IsDifferentFrom(make))
            Apply(new CarEdited(id, make, model, vin, engineType, DateTimeOffset.UtcNow));
        else if (state.Model.IsDifferentFrom(model))
            Apply(new CarEdited(id, make, model, vin, engineType, DateTimeOffset.UtcNow));
        else if (state.VIN.IsDifferentFrom(vin))
            Apply(new CarEdited(id, make, model, vin, engineType, DateTimeOffset.UtcNow));
        else if (state.EngineType.IsDifferentFrom(engineType))
            Apply(new CarEdited(id, make, model, vin, engineType, DateTimeOffset.UtcNow));
    }

    public void AppendHistory(CarId id, string type, string description)
    {

    }

    private static void Guard(CarId id, string make, string model, string vin, string engineType)
    {
        if (id is null) throw new ArgumentNullException(nameof(id));
        if (make is null) throw new ArgumentNullException(nameof(make));
        if (model is null) throw new ArgumentNullException(nameof(model));
        if (vin is null) throw new ArgumentNullException(nameof(vin));
        if (engineType is null) throw new ArgumentNullException(nameof(engineType));
    }
}

static class CarStateExtensions
{
    public static bool IsDifferentFrom(this string currentValue, string newValue)
    {
        if (string.IsNullOrEmpty(currentValue))
            return string.IsNullOrEmpty(newValue) == false;

        return currentValue.Equals(newValue, StringComparison.OrdinalIgnoreCase) == false;
    }
}

public class CarState : AggregateRootState<Car, CarId>
{
    public CarState()
    {
        History = new List<CarHistory>();
    }

    public override CarId Id { get; set; }

    public string Make { get; set; }

    public string Model { get; set; }

    public string VIN { get; set; }

    public string EngineType { get; set; }

    public List<CarHistory> History { get; set; }

    public void When(CarCreated e)
    {
        Id = e.Id;
        Make = e.Make;
        Model = e.Model;
        VIN = e.VIN;
        EngineType = e.EngineType;
    }

    public void When(CarEdited e)
    {
        Id = e.Id;
        Make = e.Make;
        Model = e.Model;
        VIN = e.VIN;
        EngineType = e.EngineType;
    }
}
