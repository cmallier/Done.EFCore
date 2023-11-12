
namespace SandboxSqlServerApp.Entities;

public class Blog
{
    public int BlogId { get; set; }

    public Guid PublicId { get; set; }

    public string Url { get; set; } = null!;

    public string? A { get; set; }

    public string? B { get; set; }

    public SomeEnum SomeEnum { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedOnDateTime { get; set; }

    //public DateOnly PublishedOnDateOnly { get; set; }

    public List<Post> Posts { get; } = new();
}