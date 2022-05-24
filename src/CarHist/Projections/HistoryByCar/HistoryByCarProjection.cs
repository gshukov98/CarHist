using System.Runtime.Serialization;
using CarHist.Cars;
using CarHist.Cars.Events;
using Elders.Cronus;
using Elders.Cronus.Projections;

namespace CarHist.Projections.HistoryByCar;

[DataContract(Namespace = BC.CarHist, Name = "884ab5ce-0fd9-4c32-bbc1-bce40386a230")]
public class HistoryByCarProjection : ProjectionDefinition<HistoryByCarData, CarId>,
    IEventHandler<HistoryAppended>
{
    public HistoryByCarProjection()
    {
        Subscribe<HistoryAppended>(x => x.Id);
    }

    //TODO: Think about the case with replaying events, maybe we should have id of event
    public void Handle(HistoryAppended @event)
    {
        if (State.History.Any(x => x.Timestamp == @event.Timestamp)) return;

        CarHistoryPojectionModel history = ToCarHistoryProjectionModel(@event);
        State.History.Add(history);
    }

    private CarHistoryPojectionModel ToCarHistoryProjectionModel(HistoryAppended @event)
    {
        return new CarHistoryPojectionModel(@event.Type, @event.Description, @event.Company, @event.Timestamp);
    }
}
