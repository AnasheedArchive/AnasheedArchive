using AnasheedArchive;
using AnasheedArchive.Components;
using BlazorStatic;
using Markdig;
using AnasheedArchive.Models;
var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseStaticWebAssets();

// Configure the pipeline with all advanced extensions active
var pipeline = new MarkdownPipelineBuilder().Build();

// Add services to the container.
builder.Services.AddRazorComponents();
builder.Services.AddBlazorStaticService(opt => {
    opt.IgnoredPathsOnContentCopy.Add("app.css");//pre-build version for tailwind
    // opt.MarkdownPipeline = pipeline;
}
);

builder.Services.AddBlogService<NasheedFrontMatter>(opt => {
    opt.BlogPageUrl = "blog";
    opt.TagsPageUrl = "tags";
    // opt.AfterBlogParsedAndAddedAction = () => ExtractTabs.Extract(opt.Posts);
    opt.BeforeBlogParsedFunc = ExtractTabs.ModifyFiles;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>();

app.UseBlog<NasheedFrontMatter>();
app.UseBlazorStaticGenerator(shutdownApp: !app.Environment.IsDevelopment());

app.Run();

public static class WebsiteKeys
{
    public const string Name = "Anasheed Archive";
    public const string BlogPostStorageAddress = "https://github.com/cabiste69/AnasheedArchive/tree/main/BlazorStaticWebsite/Content/Blog/";

    public const string GitHubRepo = "https://github.com/cabiste69/AnasheedArchive";
    public const string SiteDescription = "For the archival and translation of anasheed";

}
