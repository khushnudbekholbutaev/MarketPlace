using TechStation.Domain.Commons;

namespace TechStation.Domain.Entities;

public class Brend : Auditable
{
    public string BrendName { get; set; }
    public ICollection<Product> Products { get; set; }
}
