﻿using CarHist.Projections.AllCarsTenant;
using CarHist.UI.Models;
using Elders.Cronus.Projections;

namespace CarHist.UI.Services;

public class CarsProvider
{
    private readonly IProjectionReader _projections;
    private readonly ILogger<CarsProvider> _logger;

    public CarsProvider(IProjectionReader projections, ILogger<CarsProvider> logger)
    {
        _projections = projections;
        _logger = logger;
    }

    public IEnumerable<CarStateUI> GetCars()
    {
        var allCars = _projections.Get<AllCarsTenantProjection>(new AllCarsByTenantId("pruvit"));// hello

        if (allCars.IsSuccess)
        {
            foreach (var car in allCars.Data.State.Cars)
            {
                yield return new CarStateUI(car.Make, car.Model, car.VIN, car.EngineType);
            }
        }

        yield break;
    }
}
