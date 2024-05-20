
using Markdig;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;

namespace AnasheedArchive.Extensions;


/// <summary>
/// Extension for tagging some HTML elements with bootstrap classes.
/// </summary>
/// <seealso cref="IMarkdownExtension" />
public class BootstrapExtendedExtension : IMarkdownExtension
{
    public void Setup(MarkdownPipelineBuilder pipeline)
    {
        // Make sure we don't have a delegate twice
        pipeline.DocumentProcessed -= PipelineOnDocumentProcessed;
        pipeline.DocumentProcessed += PipelineOnDocumentProcessed;
    }


    public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
    {
    }
    private void PipelineOnDocumentProcessed(MarkdownDocument document)
    {
        foreach (var node in document.Descendants())
        {
            if (node is HeadingBlock)
            {
                node.GetAttributes().AddClass("border-bottom pb-2 mb-4");
            }
        }
    }

    private static string? GetClass(EmphasisInline emphasisInline)
    {
        return (emphasisInline.DelimiterCount < 3) && emphasisInline.DelimiterChar == '#' ? "border-bottom " : null;
    }
}