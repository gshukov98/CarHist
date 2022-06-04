using System;
using CarHist.Cars;
using CarHist.Cars.Events;
using Elders.Cronus.Testing;
using Machine.Specifications;

namespace CarHist.Tests;

[Subject(nameof(Car))]
class When_edit_a_car
{
    static CarId id;
    static string make;
    static string model;
    static string vin;
    static string engineType;
    static Car car;
    static Exception exception;

    static string newMake;
    static string newModel;
    static string newVIN;
    static string newEngineType;


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
        newModel = "M5";
        newVIN = "312";
        newEngineType = "diesel";
    };

    class When_edit_a_car_with_proper_data
    {
        Because of = () => car.EditCar(id, newMake, model, vin, engineType);

        It should_have_a_new_event = () => car.IsEventPublished<CarEdited>().ShouldBeTrue();

        It should_have_proper_make = () => car.RootState().Make.ShouldEqual(newMake);

        It should_have_proper_model = () => car.RootState().Model.ShouldEqual(model);

        It should_have_proper_VIN = () => car.RootState().VIN.ShouldEqual(vin);

        It should_have_proper_engineType = () => car.RootState().EngineType.ShouldEqual(engineType);

        It should_have_proper_id = () => car.RootState().Id.ShouldEqual(id);
    }

    class When_edit_a_car_with_proper_data_v2
    {
        Because of = () => car.EditCar(id, make, newModel, vin, engineType);

        It should_have_a_new_event = () => car.IsEventPublished<CarEdited>().ShouldBeTrue();

        It should_have_proper_make = () => car.RootState().Make.ShouldEqual(make);

        It should_have_proper_model = () => car.RootState().Model.ShouldEqual(newModel);

        It should_have_proper_VIN = () => car.RootState().VIN.ShouldEqual(vin);

        It should_have_proper_engineType = () => car.RootState().EngineType.ShouldEqual(engineType);

        It should_have_proper_id = () => car.RootState().Id.ShouldEqual(id);
    }

    class When_edit_a_car_with_proper_data_v3
    {
        Because of = () => car.EditCar(id, make, model, newVIN, engineType);

        It should_have_a_new_event = () => car.IsEventPublished<CarEdited>().ShouldBeTrue();

        It should_have_proper_make = () => car.RootState().Make.ShouldEqual(make);

        It should_have_proper_model = () => car.RootState().Model.ShouldEqual(model);

        It should_have_proper_VIN = () => car.RootState().VIN.ShouldEqual(vin);

        It should_have_proper_engineType = () => car.RootState().EngineType.ShouldEqual(engineType);

        It should_have_proper_id = () => car.RootState().Id.ShouldEqual(id);
    }

    class When_edit_a_car_with_proper_data_v4
    {
        Because of = () => car.EditCar(id, make, model, vin, newEngineType);

        It should_have_a_new_event = () => car.IsEventPublished<CarEdited>().ShouldBeTrue();

        It should_have_proper_make = () => car.RootState().Make.ShouldEqual(make);

        It should_have_proper_model = () => car.RootState().Model.ShouldEqual(model);

        It should_have_proper_VIN = () => car.RootState().VIN.ShouldEqual(vin);

        It should_have_proper_engineType = () => car.RootState().EngineType.ShouldEqual(newEngineType);

        It should_have_proper_id = () => car.RootState().Id.ShouldEqual(id);
    }

    class When_edit_a_car_with_proper_data_v5
    {
        Because of = () => car.EditCar(id, newMake, newModel, newVIN, newEngineType);

        It should_have_a_new_event = () => car.IsEventPublished<CarEdited>().ShouldBeTrue();

        It should_have_proper_make = () => car.RootState().Make.ShouldEqual(newMake);

        It should_have_proper_model = () => car.RootState().Model.ShouldEqual(newModel);

        It should_have_proper_VIN = () => car.RootState().VIN.ShouldEqual(vin);

        It should_have_proper_engineType = () => car.RootState().EngineType.ShouldEqual(newEngineType);

        It should_have_proper_id = () => car.RootState().Id.ShouldEqual(id);
    }

    class When_edit_a_car_with_carId_null
    {
        Because of = () => exception = Catch.Exception(() => car.EditCar(null, make, model, vin, engineType));

        It should_fail = () => exception.ShouldBeOfExactType<ArgumentNullException>();

        It should_have_a_specific_reason = () => exception.Message.ShouldContain("Value cannot be null. (Parameter 'id')");
    }

    class When_edit_a_car_with_make_null
    {
        Because of = () => exception = Catch.Exception(() => car.EditCar(id, null, model, vin, engineType));

        It should_fail = () => exception.ShouldBeOfExactType<ArgumentNullException>();

        It should_have_a_specific_reason = () => exception.Message.ShouldContain("Value cannot be null. (Parameter 'make')");
    }

    class When_edit_a_car_with_model_null
    {
        Because of = () => exception = Catch.Exception(() => car.EditCar(id, newMake, null, vin, engineType));

        It should_fail = () => exception.ShouldBeOfExactType<ArgumentNullException>();

        It should_have_a_specific_reason = () => exception.Message.ShouldContain("Value cannot be null. (Parameter 'model')");
    }

    class When_edit_a_car_with_vin_null
    {
        Because of = () => exception = Catch.Exception(() => car.EditCar(id, newMake, model, null, engineType));

        It should_fail = () => exception.ShouldBeOfExactType<ArgumentNullException>();

        It should_have_a_specific_reason = () => exception.Message.ShouldContain("Value cannot be null. (Parameter 'vin')");
    }

    class When_edit_a_car_with_engineType_null
    {
        Because of = () => exception = Catch.Exception(() => car.EditCar(id, newMake, model, vin, null));

        It should_fail = () => exception.ShouldBeOfExactType<ArgumentNullException>();

        It should_have_a_specific_reason = () => exception.Message.ShouldContain("Value cannot be null. (Parameter 'engineType')");
    }
}
