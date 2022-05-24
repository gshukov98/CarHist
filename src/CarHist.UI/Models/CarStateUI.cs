namespace CarHist.UI.Models;

public class CarStateUI
{
    public CarStateUI() { }

    public CarStateUI(string vIN, string make)
    {
        Make = make;
        VIN = vIN;
    }

    public CarStateUI(string make, string model, string vIN, string engineType)
    {
        Make = make;
        Model = model;
        VIN = vIN;
        EngineType = engineType;
    }

    public string Make { get; set; }
    public string Model { get; set; }
    public string VIN { get; set; }
    public string EngineType { get; set; }
}
