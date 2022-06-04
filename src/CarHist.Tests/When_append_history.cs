using System;
using CarHist.Cars;
using CarHist.Cars.Events;
using Elders.Cronus.Testing;
using Machine.Specifications;

namespace CarHist.Tests;

[Subject(nameof(Car))]
class When_append_history
{
    static CarId id;
    static string make;
    static string model;
    static string vin;
    static string engineType;
    static string type;
    static string description;
    static string testCompany;
    static Car car;
    static Exception exception;

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

        type = "repair";
        description = "Front tyres changed";
        testCompany = "Test Company";
    };

    class When_edit_a_car_with_proper_data
    {
        Because of = () => car.AppendHistory(id, type, description, testCompany);

        It should_have_a_new_event = () => car.IsEventPublished<HistoryAppended>().ShouldBeTrue();

        It should_have_proper_history_count = () => car.RootState().History.Count.ShouldEqual(1);
    }

    class When_edit_a_car_with_carId_null
    {
        Because of = () => exception = Catch.Exception(() => car.AppendHistory(null, type, description, testCompany));

        It should_fail = () => exception.ShouldBeOfExactType<ArgumentNullException>();

        It should_have_a_specific_reason = () => exception.Message.ShouldContain("Value cannot be null. (Parameter 'id')");
    }

    class When_edit_a_car_with_type_null
    {
        Because of = () => exception = Catch.Exception(() => car.AppendHistory(id, null, description, testCompany));

        It should_fail = () => exception.ShouldBeOfExactType<ArgumentNullException>();

        It should_have_a_specific_reason = () => exception.Message.ShouldContain("Value cannot be null. (Parameter 'type')");
    }

    class When_edit_a_car_with_description_null
    {
        Because of = () => exception = Catch.Exception(() => car.AppendHistory(id, type, null, testCompany));

        It should_fail = () => exception.ShouldBeOfExactType<ArgumentNullException>();

        It should_have_a_specific_reason = () => exception.Message.ShouldContain("Value cannot be null. (Parameter 'description')");
    }

    class When_edit_a_car_with_company_null
    {
        Because of = () => exception = Catch.Exception(() => car.AppendHistory(id, type, description, null));

        It should_fail = () => exception.ShouldBeOfExactType<ArgumentNullException>();

        It should_have_a_specific_reason = () => exception.Message.ShouldContain("Value cannot be null. (Parameter 'company')");
    }
}
