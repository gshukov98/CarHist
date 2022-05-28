namespace CarHist.Blazor.UI.Models;

public class CarStateUI
{
    public CarStateUI() { }

    public CarStateUI(string vin, string make)
    {
        Make = make;
        VIN = vin;
    }

    public CarStateUI(string make, string model, string vin, string engineType)
    {
        Make = make;
        Model = model;
        VIN = vin;
        EngineType = engineType;
    }

    public string Make { get; set; }
    public string Model { get; set; }
    public string VIN { get; set; }
    public string EngineType { get; set; }
}
