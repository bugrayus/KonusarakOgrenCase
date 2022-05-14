namespace KonusarakOgrenCase.Domain.Entities;

public class ProductComment : BaseEntity
{
    public Product Product { get; set; } = null!;
    public string Comment { get; set; } = null!;
    public User User { get; set; } = null!;
}