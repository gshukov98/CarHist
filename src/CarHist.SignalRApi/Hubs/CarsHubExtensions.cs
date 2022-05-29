using Microsoft.AspNetCore.SignalR;

namespace CarHist.SignalRApi.Hubs;

internal static class CarsHubExtensions
{
    internal static Task AnnounceThatCarIsCreated(this IHubContext<CarsHub> hub, CarStateModel model)
    {
        if (hub?.Clients?.All is null == false)
        {
            hub.Clients.All.SendCoreAsync("CarCreation", new object[] { model.Id.ToString(), model.Name }).GetAwaiter().GetResult();
        }

        return Task.CompletedTask;
    }

    internal static Task AnnounceThatCarIsEdited(this IHubContext<CarsHub> hub, CarStateModel model)
    {
        if (hub?.Clients?.All is null == false)
        {
            hub.Clients.All.SendCoreAsync("CarEdit", new object[] { model.Id.ToString(), model.Name }).GetAwaiter().GetResult();
        }

        return Task.CompletedTask;
    }

    internal static Task AnnounceThatCarIsDeleted(this IHubContext<CarsHub> hub, CarStateModel model)
    {
        if (hub?.Clients?.All is null == false)
        {
            hub.Clients.All.SendCoreAsync("CarDeleted", new object[] { model.Id.ToString(), model.Name }).GetAwaiter().GetResult();
        }

        return Task.CompletedTask;
    }
}
