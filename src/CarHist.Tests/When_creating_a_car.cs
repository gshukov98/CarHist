using System;
using CarHist.Cars;
using CarHist.Cars.Events;
using Elders.Cronus.Testing;
using Machine.Specifications;

namespace CarHist.Tests;

[Subject(nameof(Car))]
class When_creating_a_car
{
    static CarId id;
    static string make;
    static string model;
    static string vin;
    static string engineType;
    static Car car;
    static Exception exception;

    Establish context = () =>
    {
        id = new CarId("test", "pruvit");
        make = "BMW";
        model = "M3";
        vin = "123123";
        engineType = "petrol";
    };

    class When_creating_a_car_with_proper_data
    {
        Because of = () => car = new Car(id, make, model, vin, engineType);

        It should_have_a_new_car = () => car.IsEventPublished<CarCreated>().ShouldBeTrue();

        It should_have_proper_make = () => car.RootState().Make.ShouldEqual(make);

        It should_have_proper_model = () => car.RootState().Model.ShouldEqual(model);

        It should_have_proper_VIN = () => car.RootState().VIN.ShouldEqual(vin);

        It should_have_proper_engineType = () => car.RootState().EngineType.ShouldEqual(engineType);

        It should_have_proper_id = () => car.RootState().Id.ShouldEqual(id);
    }

    class When_creating_a_car_with_carId_null
    {
        Because of = () => exception = Catch.Exception(() => new Car(null, make, model, vin, engineType));

        It should_fail = () => exception.ShouldBeOfExactType<ArgumentNullException>();

        It should_have_a_specific_reason = () => exception.Message.ShouldContain("Value cannot be null. (Parameter 'id')");
    }

    class When_creating_a_car_with_make_null
    {
        Because of = () => exception = Catch.Exception(() => new Car(id, null, model, vin, engineType));

        It should_fail = () => exception.ShouldBeOfExactType<ArgumentNullException>();

        It should_have_a_specific_reason = () => exception.Message.ShouldContain("Value cannot be null. (Parameter 'make')");
    }

    class When_creating_a_car_with_model_null
    {
        Because of = () => exception = Catch.Exception(() => new Car(id, make, null, vin, engineType));

        It should_fail = () => exception.ShouldBeOfExactType<ArgumentNullException>();

        It should_have_a_specific_reason = () => exception.Message.ShouldContain("Value cannot be null. (Parameter 'model')");
    }

    class When_creating_a_car_with_vin_null
    {
        Because of = () => exception = Catch.Exception(() => new Car(id, make, model, null, engineType));

        It should_fail = () => exception.ShouldBeOfExactType<ArgumentNullException>();

        It should_have_a_specific_reason = () => exception.Message.ShouldContain("Value cannot be null. (Parameter 'vin')");
    }

    class When_creating_a_car_with_engineType_null
    {
        Because of = () => exception = Catch.Exception(() => new Car(id, make, model, vin, null));

        It should_fail = () => exception.ShouldBeOfExactType<ArgumentNullException>();

        It should_have_a_specific_reason = () => exception.Message.ShouldContain("Value cannot be null. (Parameter 'engineType')");
    }
}
