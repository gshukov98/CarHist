using System;
using System.Linq;
using CarHist.Cars;
using CarHist.Cars.Events;
using Elders.Cronus.Testing;
using Machine.Specifications;

namespace CarHist.Tests;

[Subject(nameof(Car))]
class When_append_history_v2
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

    Establish context = () =>
    {
        id = new CarId("test", "pruvit");
        make = "BMW";
        model = "M3";
        vin = "123123";
        engineType = "petrol";

        car = Aggregate<Car>
            .FromHistory(stream => stream
                .AddEvent(new CarCreated(id, make, model, vin, engineType, DateTimeOffset.UtcNow.AddHours(-1)))
                .AddEvent(new HistoryAppended(id, "service", "oil changed", "Test company", DateTimeOffset.UtcNow))
            );

        type = "repair";
        description = "Front tyres changed";
        testCompany = "Test Company";
    };

    class When_edit_a_car_with_proper_data
    {
        Because of = () => car.AppendHistory(id, type, description, testCompany);

        It should_have_a_new_event = () => car.IsEventPublished<HistoryAppended>().ShouldBeTrue();

        It should_have_proper_history_count = () => car.RootState().History.Count.ShouldEqual(2);

        It should_have_proper_type = () => car.RootState().History.Last().Type.ShouldEqual(type);

        It should_have_proper_description = () => car.RootState().History.Last().Description.ShouldEqual(description);

        It should_have_proper_company = () => car.RootState().History.Last().Company.ShouldEqual(testCompany);

        It should_have_proper_type2 = () => car.RootState().History.First().Type.ShouldEqual("service");

        It should_have_proper_description2 = () => car.RootState().History.First().Description.ShouldEqual("oil changed");

        It should_have_proper_company2 = () => car.RootState().History.First().Company.ShouldEqual("Test company");
    }
}
