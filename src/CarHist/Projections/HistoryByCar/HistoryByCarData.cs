using System.Runtime.Serialization;

namespace CarHist.Projections.HistoryByCar;

[DataContract(Namespace = BC.CarHist, Name = "d6aa9f35-5d72-4ab8-b920-e45b045f63aa")]
public class HistoryByCarData
{
    public HistoryByCarData()
    {
        History = new List<CarHistoryPojectionModel>();
    }

    public List<CarHistoryPojectionModel> History { get; set; }

    public bool IsDeleted { get; set; }
}

