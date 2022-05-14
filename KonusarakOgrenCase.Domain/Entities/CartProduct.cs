namespace KonusarakOgrenCase.Domain.Entities;

public class CartProduct : BaseEntity
{
    public Product Product { get; set; } = null!;
    public int ProductId { get; set; }
    public Cart Cart { get; set; } = null!;
    public int CartId { get; set; }
    public int Quantity { get; set; }
}