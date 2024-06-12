using Markdig;
using AnasheedArchive.Markdigs.Extensions;

namespace AnasheedArchive.Extensions;
public static class CustomMarkdownExtensions
{
    public static MarkdownPipelineBuilder UseBootstrapExtended(this MarkdownPipelineBuilder pipeline)
    {
        pipeline.Extensions.ReplaceOrAdd<BootstrapExtendedExtension>(new BootstrapExtendedExtension());
        return pipeline;
    }
    public static MarkdownPipelineBuilder UseHeadingsFormatter(this MarkdownPipelineBuilder pipeline)
    {
        pipeline.Extensions.ReplaceOrAdd<HeadingsFormatterExtension>(new HeadingsFormatterExtension());
        return pipeline;
    }
}