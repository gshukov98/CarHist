using System;
using CarHist.Cars;
using CarHist.Cars.Events;
using Elders.Cronus.Testing;
using Machine.Specifications;

namespace CarHist.Tests;

[Subject(nameof(Car))]
class When_creating_car
{
    static CarId id;
    static string make;
    static string model;
    static string vin;
    static string engineType;
    static Car car;

    Establish context = () =>
    {
        id = new CarId("test", "pruvit");
        make = "BMW";
        model = "M3";
        vin = "123123";
        engineType = "petrol";
    };

    Because of = () => car = new Car(id, make, model, vin, engineType);

    It should_have_a_new_car = () => car.IsEventPublished<CarCreated>().ShouldBeTrue();

    It should_have_proper_make = () => car.RootState().Make.ShouldEqual(make);

    It should_have_proper_model = () => car.RootState().Model.ShouldEqual(model);

    It should_have_proper_VIN = () => car.RootState().VIN.ShouldEqual(vin);

    It should_have_proper_engineType = () => car.RootState().EngineType.ShouldEqual(engineType);

    It should_have_proper_id = () => car.RootState().Id.ShouldEqual(id);
}

[Subject(nameof(Car))]
class When_edit_car
{
    static CarId id;
    static string make;
    static string model;
    static string vin;
    static string engineType;
    static Car car;

    static string newMake;

    Establish context = () =>
    {
        id = new CarId("test", "pruvit");
        make = "BMW";
        model = "M3";
        vin = "123123";
        engineType = "petrol";

        car = Aggregate<Car>
            .FromHistory(stream => stream
                .AddEvent(new CarCreated(id, make, model, vin, engineType, DateTimeOffset.UtcNow))
            );

        newMake = "VW";
    };

    Because of = () => car.EditCar(id, newMake, model, vin, engineType);

    It should_have_a_new_event = () => car.IsEventPublished<CarEdited>().ShouldBeTrue();

    It should_have_proper_make = () => car.RootState().Make.ShouldEqual(newMake);

    It should_have_proper_model = () => car.RootState().Model.ShouldEqual(model);

    It should_have_proper_VIN = () => car.RootState().VIN.ShouldEqual(vin);

    It should_have_proper_engineType = () => car.RootState().EngineType.ShouldEqual(engineType);

    It should_have_proper_id = () => car.RootState().Id.ShouldEqual(id);
}
