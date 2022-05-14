namespace KonusarakOgrenCase.Domain.Entities;

public class User : BaseEntity
{
    public string Mail { get; set; } = null!;
    public string HashedPassword { get; set; } = null!;
    public string Salt { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public UserRole UserRole { get; set; }
}