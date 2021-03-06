using System.Runtime.Serialization;
using CarHist.Cars;
using CarHist.Cars.Events;
using Elders.Cronus;
using Elders.Cronus.Projections;

namespace CarHist.Projections.HistoryByCar;

[DataContract(Namespace = BC.CarHist, Name = "884ab5ce-0fd9-4c32-bbc1-bce40386a230")]
public class HistoryByCarProjection : ProjectionDefinition<HistoryByCarData, CarId>,
    IEventHandler<HistoryAppended>,
    IEventHandler<CarDeleted>
{
    public HistoryByCarProjection()
    {
        Subscribe<HistoryAppended>(x => x.Id);
        Subscribe<CarDeleted>(x => x.Id);
    }

    public void Handle(HistoryAppended @event)
    {
        if (State.History.Any(x => x.Timestamp == @event.Timestamp)) return;

        CarHistoryPojectionModel history = ToCarHistoryProjectionModel(@event);
        State.History.Add(history);
    }

    public void Handle(CarDeleted @event)
    {
        State.IsDeleted = true;
    }

    private CarHistoryPojectionModel ToCarHistoryProjectionModel(HistoryAppended @event)
    {
        return new CarHistoryPojectionModel(@event.Type, @event.Description, @event.Company, @event.Timestamp);
    }
}
