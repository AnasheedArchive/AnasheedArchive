using AnasheedArchive;
using AnasheedArchive.Components;
using BlazorStatic;
using Markdig;
using AnasheedArchive.Models;
using AnasheedArchive.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseStaticWebAssets();

// Configure the pipeline with all advanced extensions active
var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions()
        .UseYamlFrontMatter()
        .UseCitations()
        .UseBootstrap()
        .UseBootstrapExtended()
        .UseHeadingsFormatter()
        .Build();

// Add services to the container.
builder.Services.AddRazorComponents();
builder.Services.AddBlazorStaticService(opt =>
{
    opt.MarkdownPipeline = pipeline;
    opt.OutputFolderPath = "../output";
    opt.SuppressFileGeneration = true;
});

builder.Services.AddBlogService<NasheedFrontMatter>(opt =>
{
    opt.BlogPageUrl = PagesNames.Anasheed;
    opt.ContentPath = Path.Combine("Content","Anasheed");
    ExtractTabs.ModifyFiles(opt.ContentPath);
});

builder.Services.AddBlogService<NewsFrontMatter>(opt =>
{
    opt.BlogPageUrl = PagesNames.News;
    opt.ContentPath = Path.Combine("Content","News");
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
app.UseBlog<NewsFrontMatter>();

app.UseBlazorStaticGenerator(shutdownApp: !app.Environment.IsDevelopment());

app.Run();

public static class WebsiteKeys
{
    public const string Name = "Anasheed Archive";
    public const string GitHubRepo = "https://github.com/AnasheedArchive/Site";
    public const string SiteDescription = "For the archival and translation of Anasheed";
}
