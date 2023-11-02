# Configuring Relationships (Navigations)

- Some relations are **implicitly** discovered by EF Core (Conventions)
- Can define **explicit** relationships using Fluent API
- It is **never necessary** to configure a relationship **twice**


## One-to-many

Blog - 1:n - Posts

- Required one-to-many
- Optional one-to-many
- Required one-to-many with shadow foreign key
- Optional one-to-many with shadow foreign key
- One-to-many without navigation to principal
- One-to-many without navigation to principal and with shadow foreign key
- One-to-many without navigation to dependents
- One-to-many with no navigations
- One-to-many with alternate key
- One-to-many with composite foreign key
- Required one-to-many without cascade delete
- Self-referencing one-to-many


Resources :
https://learn.microsoft.com/en-us/ef/core/modeling/relationships/one-to-many



``` csharp

// Principal (parent)
public class Blog
{
    public int Id { get; set; }         // Primary Key (Principal Key)
    public string Name { get; set; }

    public ICollection<Post> Posts { get; } // Collection navigation containing dependents
}

// Dependent (child)
public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }

    public int BlogId { get; set; }           // Foreign Key (Required)
    public Blog Blog { get; set; } = null!;   // One blog
}


modelBuilder.Entity<Blog>()
            .HasMany( x => x.Posts )
            .WithOne( x => x.Blog )
            .HasForeignKey( x => x.BlogId )
            .HasPrincipalKey( x => x.Id );

// Not needed (Already defined)
// modelBuilder.Entity<Post>()
//             .HasOne( x => x.Blog )
//             .WithMany( x => x.Posts )
//             .HasForeignKey( x => x.BlogId )
//             .IsRequired();
```

### Required one-to-many

- Every dependent entity **must** be associated with some principal entity


### Optional one-to-many

``` csharp
// Dependent (child)
public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }

    public int? BlogId { get; set; }           // Foreign Key (Optional)
    public Blog? Blog { get; set; } = null!;   // Optional reference navigation to principal
}

modelBuilder.Entity<Blog>()
            .HasMany( x => x.Posts )
            .WithOne( x => x.Blog )
            .HasForeignKey( x => x.BlogId )
            .IsRequired( false );
```

### Required one-to-many with shadow foreign key

``` csharp
// Dependent (child)
public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }

    // public int? BlogId { get; set; }       // Shadow
    public Blog Blog { get; set; } = null!;   // Optional reference navigation to principal
}

modelBuilder.Entity<Blog>()
            .HasMany( x => x.Posts )
            .WithOne( x => x.Blog )
            .HasForeignKey( "BlogId" )
            .IsRequired();
```

## One-to-many without navigation to principal

``` csharp
// Dependent (child)
public class Post
{
    public int Id { get; set; }
    public int BlogId { get; set; }              // Foreign Key (Required)
    // public Blog Blog { get; set; } = null!;   // No navigation to principal
}

modelBuilder.Entity<Blog>()
            .HasMany( x => x.Posts )
            .WithOne()                      // No navigation to principal
            .HasForeignKey( x => x.BlogId )
            .IsRequired();
```


## One-to-one

- Required one-to-one
- Optional one-to-one
- Required one-to-one with primary key to primary key relationship
- Required one-to-one with shadow foreign key
- Optional one-to-one with shadow foreign key
- One-to-one without navigation to principal
- One-to-one without navigation to principal and with shadow foreign key
- One-to-one without navigation to dependent
- One-to-one with no navigations
- One-to-one with alternate key
- One-to-one with composite foreign key
- Required one-to-one without cascade delete
- Self-referencing one-to-one

Resources:
https://learn.microsoft.com/en-us/ef/core/modeling/relationships/one-to-one


Notes

- Table with the foreign key column(s) must map to the **dependent** type.



``` csharp
// Principal (parent)
public class Blog
{
    public int Id { get; set; }
    ...

    public BlogHeader? Header { get; set; } // Reference navigation to dependent
}

// Dependent (child)
public class BlogHeader
{
    public int Id { get; set; }
    ...

    public int BlogId { get; set; }         // Required foreign key property
    public Blog Blog { get; set; } = null!; // Required reference navigation to principal
}

modelBuilder.Entity<Blog>()
            .HasOne( x => x.Header )
            .WithOne( x => x.Blog )
            .HasForeignKey<BlogHeader>( x => x.BlogId )
            .IsRequired();
```


## Many-to-many


Resources:
https://learn.microsoft.com/en-us/ef/core/modeling/relationships/many-to-many


``` csharp
public class Post
{
    public int Id { get; set; }
    public List<Tag> Tags { get; } = new();
}

public class Tag
{
    public int Id { get; set; }
    public List<Post> Posts { get; } = new();
}

// Convention

// Explicit
modelBuilder.Entity<Post>()
            .HasMany( x => x.Tags )
            .WithMany( x => x.Posts );

// Explicit
modelBuilder.Entity<Post>()
            .HasMany( x => x.Tags )
            .WithMany( x => x.Posts )
            .UsingEntity( "PostsToTagsJoinTable" )

// Explicit
modelBuilder.Entity<Post>()
            .HasMany( x => x.Tags )
            .WithMany( x => x.Posts )
            .UsingEntity(
                "PostTag",
                l => l.HasOne(typeof(Tag)).WithMany().HasForeignKey("TagsId").HasPrincipalKey(nameof(Tag.Id)),
                r => r.HasOne(typeof(Post)).WithMany().HasForeignKey("PostsId").HasPrincipalKey(nameof(Post.Id)),
                j => j.HasKey("PostsId", "TagsId"));


// Notice that in this mapping there is no many-to-many relationship,
// but rather two one-to-many relationships
public class Post
{
    public int Id { get; set; }
    public List<PostTag> PostTags { get; } = new();
}

public class Tag
{
    public int Id { get; set; }
    public List<PostTag> PostTags { get; } = new();
}

public class PostTag
{
    public int PostsId { get; set; }
    public int TagsId { get; set; }
    public Post Post { get; set; } = null!;
    public Tag Tag { get; set; } = null!;
}
```
