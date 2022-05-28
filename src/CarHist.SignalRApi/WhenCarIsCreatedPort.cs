using System.Runtime.Serialization;
using CarHist.Cars.Events;
using CarHist.SignalRApi.Hubs;
using Elders.Cronus;
using Elders.Cronus.Discoveries;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace CarHist.SignalRApi;

[DataContract(Namespace = BC.CarHist, Name = "0ac79d7e-3680-4daa-a353-38add518ef73")]
public class WhenCarIsCreatedPort : IPort,
    IEventHandler<CarCreated>,
    IEventHandler<CarEdited>
{
    private readonly IHubContext<CarsHub> hub;

    public WhenCarIsCreatedPort(ICronusApiAccessor cronusApiAccessor)
    {
        hub = cronusApiAccessor.Provider.GetRequiredService<IHubContext<CarsHub>>();
    }

    public void Handle(CarCreated @event)
    {
        hub.AnnounceThatCarIsCreated(new CarStateModel(@event.Id, @event.Make));
    }

    public void Handle(CarEdited @event)
    {
        hub.AnnounceThatCarIsEdited(new CarStateModel(@event.Id, @event.Make));
    }
}
