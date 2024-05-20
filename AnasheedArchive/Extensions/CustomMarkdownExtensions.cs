using Markdig;

namespace AnasheedArchive.Extensions;
public static class CustomMarkdownExtensions
{
    public static MarkdownPipelineBuilder UseBootstrapExtended(this MarkdownPipelineBuilder pipeline)
    {
        pipeline.Extensions.ReplaceOrAdd<BootstrapExtendedExtension>(new BootstrapExtendedExtension());
        return pipeline;
    }
}