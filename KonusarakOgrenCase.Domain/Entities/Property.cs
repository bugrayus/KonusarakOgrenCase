namespace KonusarakOgrenCase.Domain.Entities;

public class Property : BaseEntity
{
    public string Name { get; set; } = null!;

    public string Value { get; set; } = null!;

    //Should be checked if there is other discounted properties exist.
    public int Discount { get; set; }
}