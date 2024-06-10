using BlazorStatic;

namespace AnasheedArchive.Models;

/// <summary>
/// Front Matter class for anasheed.
/// </summary>
public class BaseFrontMatterModel : IFrontMatter
{
    /// <summary>
    /// Original title of the nasheed.
    /// </summary>
    public string Title { get; set; } = "";
    /// <summary>
    /// the cover image of the article
    /// </summary>
    /// <value></value>
    public string? Image { get; set; }
    /// <summary>
    /// the contributors to the article
    /// </summary>
    /// <value></value>
    public Contributor[]? Contributors { get; set; }
    /// <inheritdoc />
    public List<string> Tags { get; set; } = [];
    /// <inheritdoc />
    public bool IsDraft { get; set; }
}

