namespace KonusarakOgrenCase.Domain.Entities;

public class Cart : BaseEntity
{
    public User User { get; set; } = null!;
    public int UserId { get; set; }
    public virtual List<CartProduct>? Products { get; set; }
}