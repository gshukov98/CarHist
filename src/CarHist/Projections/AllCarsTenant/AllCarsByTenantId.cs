using System.Runtime.Serialization;
using Elders.Cronus;

namespace CarHist.Projections.AllCarsTenant;

[DataContract(Namespace = BC.CarHist, Name = "541e0af6-49f8-4dd1-8675-35dd75e88386")]
public class AllCarsByTenantId : Urn
{
    AllCarsByTenantId() { }

    public AllCarsByTenantId(string tenant) : base("carsbytenant", tenant) { }

}
