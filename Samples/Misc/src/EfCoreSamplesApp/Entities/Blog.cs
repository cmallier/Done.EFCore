namespace EfCoreSamplesApp.Entities;

public class Blog
{
    public int BlogId { get; set; }

    public string Url { get; set; } = null!;



    public int Rating { get; set; } = 3;


    // Navigation property (one-to-many) Blog -> Posts
    public List<Post> Posts { get; } = new();
}
