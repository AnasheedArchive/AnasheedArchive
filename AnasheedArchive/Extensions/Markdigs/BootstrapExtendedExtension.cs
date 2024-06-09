using Markdig;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;

namespace AnasheedArchive.Markdigs.Extensions;


/// <summary>
/// Extension for tagging some HTML elements with bootstrap classes (extended edition)
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
            if (node is HeadingBlock && node is not null)
            {
                var HeadingNode = node as HeadingBlock;
                
                HeadingNode?.GetAttributes().AddClass(GetClass(HeadingNode));
            }
        }
    }

    private static string GetClass(HeadingBlock emphasisInline)
    {
        // only add borders for headers with 3 or less #
        return (emphasisInline.Level < 3) && emphasisInline.HeaderChar == '#' ? "border-bottom pb-2 mb-4" : "";
    }
}
