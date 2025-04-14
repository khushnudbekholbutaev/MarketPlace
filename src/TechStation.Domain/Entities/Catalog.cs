using System.ComponentModel.DataAnnotations;
using TechStation.Domain.Commons;

namespace TechStation.Domain.Entities;

public class Catalog : Auditable
{
    public string Name { get; set; }
    public ICollection<Category> Categories { get; set; }
}
