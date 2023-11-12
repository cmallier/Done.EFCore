namespace EfCoreSamplesApp.Entities;
public class SomeEntity
{
    public int SomeEntityId { get; set; }

    public string SocialSecurityNumber { get; set; } = null!;

    public string SomeProperty { get; set; } = null!;

    public string Url { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string State { get; set; } = null!;
}
