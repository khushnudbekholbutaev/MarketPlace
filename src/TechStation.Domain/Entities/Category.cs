using System.ComponentModel.DataAnnotations;
using TechStation.Domain.Commons;

namespace TechStation.Domain.Entities;

public class Category : Auditable
{
   
    public string CategoryName { get; set; }
    public string Description { get; set; }
    public long CatalogId { get; set; }
    public Catalog Catalog { get; set; }
    public ICollection<Product> Products { get; set; }
}
