namespace KonusarakOgrenCase.Domain.Entities;

public class Brand : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public User User { get; set; } = null!;
    public List<Product>? Products { get; set; }
}