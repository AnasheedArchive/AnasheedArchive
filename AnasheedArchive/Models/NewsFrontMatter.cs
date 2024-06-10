using BlazorStatic;

namespace AnasheedArchive.Models;

/// <summary>
/// Front Matter class for anasheed.
/// </summary>
public class NewsFrontMatter : BaseFrontMatterModel
{
    /// <summary>
    /// Lead or description of the blog post.
    /// </summary>
    public string Lead { get; set; } = "";
    /// <summary>
    /// time it takes to read the blog post.
    /// </summary>
    public string? ReadTime { get; set; }
    /// <summary>
    /// Date of publishing the blog post.
    /// </summary>
    public DateOnly Published { get; set; } = DateOnly.FromDateTime(DateTime.Now);
}

