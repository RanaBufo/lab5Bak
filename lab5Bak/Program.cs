using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.UseDefaultFiles();
DefaultFilesOptions options = new DefaultFilesOptions();
options.DefaultFileNames.Clear(); // удаляем имена файлов по умолчанию
options.DefaultFileNames.Add("CatPage.html"); // добавляем новое имя файла
app.UseDefaultFiles(options); // установка параметров
app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions() // обрабатывает запросы к каталогу wwwroot/html
{
    FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\html")),
    RequestPath = new PathString("/pages")
});

app.UseStaticFiles(new StaticFileOptions() // обрабатывает запросы к каталогу wwwroot/html
{
    FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\pictures")),
    RequestPath = new PathString("/pages")
});

app.UseDirectoryBrowser(new DirectoryBrowserOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\html")),

    RequestPath = new PathString("/pages")
});
app.UseDirectoryBrowser(new DirectoryBrowserOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot")),

    RequestPath = new PathString("/static")
});
app.UseDirectoryBrowser(new DirectoryBrowserOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\pictures")),

    RequestPath = new PathString("/catImg")
});
//app.Run(async (context) => await context.Response.SendFileAsync("index.html"));

app.Map("/contact", async (context) => await context.Response.SendFileAsync(@"wwwroot\index.html"));
app.Run();
