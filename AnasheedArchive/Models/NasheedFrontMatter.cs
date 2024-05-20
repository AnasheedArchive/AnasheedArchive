using System.Globalization;

namespace AnasheedArchive.Models;

/// <summary>
/// Front Matter class for anasheed.
/// </summary>
public class NasheedFrontMatter : BaseFrontMatterModel
{
    /// <summary>
    /// Translated title of the nasheed.
    /// </summary>
    public string TitleEn { get; set; } = "";
    /// <summary>
    /// Duration of the nasheed in seconds.
    /// </summary>
    public int Duration { get; set; } = 0;

    /// <summary>
    /// The producer of the nasheed.
    /// </summary>
    public string Producer { get; set; } = "";
    /// <summary>
    /// Is the nasheed featured on the home page?
    /// </summary>
    /// <value></value>
    public bool Featured { get; set; } = false;

    public string GetFormattedDurration() => TimeSpan.FromSeconds(Duration).ToString("m':'ss' min'");
}
