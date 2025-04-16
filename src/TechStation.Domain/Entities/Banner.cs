using TechStation.Domain.Commons;

namespace TechStation.Domain.Entities;

public class Banner : Auditable
{
    public string Images { get; set; }
    public long BrendId { get; set; }
    public Brend Brend { get; set; }
    public long CategoryId { get; set; }
    public Category Category { get; set; }
}
