namespace KonusarakOgrenCase.Domain.Entities;

public class ProductProperty : BaseEntity
{
    public Product Product { get; set; } = null!;
    public Property Property { get; set; } = null!;
}