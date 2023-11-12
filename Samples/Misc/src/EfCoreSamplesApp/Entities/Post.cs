namespace EfCoreSamplesApp.Entities;

public class Post
{
    // Primary Key
    public int PostId { get; set; }

    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;

    // Foreign Key
    public int BlogId { get; set; }

    // Navigation property
    public Blog Blog { get; set; } = null!;
}