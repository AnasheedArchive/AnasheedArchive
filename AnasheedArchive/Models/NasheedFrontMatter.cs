using BlazorStatic;

namespace AnasheedArchive.Models;

/// <summary>
/// Showcase of a front matter class. If you have a different front matter format, implement your own class.
/// </summary>
public class NasheedFrontMatter:IFrontMatter
{
    /// <summary>
    /// Title of the blog post.
    /// </summary>
    public string Title { get; set; }
    /// <summary>
    /// Lead or description of the blog post.
    /// </summary>
    public string Lead { get; set; } = "";
    /// <summary>
    /// Date of publishing the blog post.
    /// </summary>
    public DateTime Published { get; set; } = DateTime.Now;
    /// <inheritdoc />
    public List<string> Tags { get; set; } = [];

    /// <inheritdoc />
    public bool IsDraft { get; set; }

    /// <summary>
    /// Authors of the blog post.
    /// </summary>
    public List<Author> Authors { get; set; } = [];
    public bool Featured { get; set; }
}
/// <summary>
/// Author of a blog post.
/// </summary>
public class Author
{
    /// <summary>
    /// Name of the author.
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// GitHub username of the author.
    /// </summary>
    public string? GitHubUserName { get; set; }
    /// <summary>
    /// Twitter username of the author.
    /// </summary>
    public string? TwitterUserName { get; set; }
}

