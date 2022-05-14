namespace KonusarakOgrenCase.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public Brand Brand { get; set; } = null!;
    public List<ProductComment>? Comments { get; set; }
    public List<ProductProperty>? Properties { get; set; }
}