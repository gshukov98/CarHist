using CarHist.Blazor.UI.Models;
using CarHist.Cars;
using CarHist.Projections.HistoryByCar;
using Elders.Cronus.Projections;

namespace CarHist.Blazor.UI.Services;

public class CarHistoryProvider
{
    private readonly IProjectionReader _projections;
    private readonly ILogger<CarsProvider> _logger;

    public CarHistoryProvider(IProjectionReader projections, ILogger<CarsProvider> logger)
    {
        _projections = projections;
        _logger = logger;
    }

    public IEnumerable<CarHistoryUI> GetCarByVIN(CarId carId)
    {
        var carHistory = _projections.Get<HistoryByCarProjection>(carId);

        if (carHistory.IsSuccess)
        {
            if (carHistory.Data.State.IsDeleted == true) yield break;

            foreach (var car in carHistory.Data.State.History.OrderBy(x => x.Timestamp))
                yield return new CarHistoryUI(car.Type, car.Timestamp, car.Description);
        }

        yield break;
    }
}
