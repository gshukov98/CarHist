using CarHist.Blazor.UI.Models;
using CarHist.Projections.AllCarsTenant;
using Elders.Cronus.Projections;

namespace CarHist.Blazor.UI.Services;

public class CarsProvider
{
    private readonly IProjectionReader _projections;
    private readonly ILogger<CarsProvider> _logger;

    public CarsProvider(IProjectionReader projections, ILogger<CarsProvider> logger)
    {
        _projections = projections;
        _logger = logger;
    }

    public bool IsExistingCar(string vin)
    {
        var allCars = _projections.Get<AllCarsTenantProjection>(new AllCarsByTenantId("pruvit"));

        foreach (var car in allCars.Data.State.Cars)
        {
            if (car.VIN.Equals(vin, StringComparison.OrdinalIgnoreCase))
                return true;
        }

        return false;
    }

    public IEnumerable<CarStateUI> GetCars()
    {
        var allCars = _projections.Get<AllCarsTenantProjection>(new AllCarsByTenantId("pruvit"));

        if (allCars.IsSuccess)
        {
            foreach (var car in allCars.Data.State.Cars)
            {
                if (car.DeletedDate != DateTimeOffset.MinValue) continue;

                yield return new CarStateUI(car.Make, car.Model, car.VIN, car.EngineType);
            }
        }

        yield break;
    }

    public IEnumerable<CarStateUI> GetCarsBySearchVIN(string vin)
    {
        var allCars = _projections.Get<AllCarsTenantProjection>(new AllCarsByTenantId("pruvit"));

        if (allCars.IsSuccess)
        {
            foreach (var car in allCars.Data.State.Cars)
            {
                if (car.DeletedDate != DateTimeOffset.MinValue) continue;

                if (car.VIN.ToLowerInvariant().Contains(vin.ToLowerInvariant()))
                    yield return new CarStateUI(car.Make, car.Model, car.VIN, car.EngineType);
            }
        }

        yield break;
    }
}
