using Markdig;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;

namespace AnasheedArchive.Markdigs.Extensions;

class HeadingsFormatterExtension: IMarkdownExtension
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
            if (node is EmphasisInline && node is not null)
            {
                
                var HeadingNode = node;
                // var x = HeadingNode.GetData("live");
            }
        }
    }
}