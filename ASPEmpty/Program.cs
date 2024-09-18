using ASPEmpty;
using ASPEmpty.Helpers;
using ASPEmpty.Middleware;
using ASPEmpty.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Text;


var builder = WebApplication.CreateBuilder(args);
string connectionString = builder.Configuration.GetConnectionString("SQLConnection");
builder.Services.AddDbContext<RealestaterentalContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddTransient<FlatService>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache();


var app = builder.Build();
app.UseSession();
app.Use(async (context, next) =>
{
    await next();
    Console.WriteLine(context.Response.StatusCode);
    Console.WriteLine(!context.Response.HasStarted);

    if (context.Response.StatusCode == 404 && !context.Response.HasStarted)
    {

        context.Response.Redirect("/");
    }
});

app.MapGet("/table", async (FlatService md) =>
{
    var html = FlatHelper.GetTable(await md.GetFlats());
    return Results.Content(html, "text/html");

});
app.MapGet("/searchform1", async (context) =>
{
    if(context.Request.Cookies.ContainsKey("city") && context.Request.Cookies.ContainsKey("avgMark") && context.Request.Cookies.ContainsKey("wifi"))
    {
        await context.Response.WriteAsync(FlatHelper.GetForm(context.Request.Cookies["city"], context.Request.Cookies["avgMark"], context.Request.Cookies["wifi"]));

    }
    else
    {
        await context.Response.WriteAsync(FlatHelper.GetForm());

    }

});
app.MapGet("/searchform2", async (context) =>
{

    if (context.Session.Keys.Contains("city") && context.Session.Keys.Contains("avgMark") && context.Session.Keys.Contains("wifi"))
    {
        await context.Response.WriteAsync(FlatHelper.GetForm(context.Session.GetString("city"), context.Session.GetString("avgMark"), context.Session.GetString("wifi")));

    }
    else
    {
        await context.Response.WriteAsync(FlatHelper.GetForm());

    }

});

app.MapPost("/getflats", async (context) =>
{
    // Ensure the request has form data
    if (context.Request.HasFormContentType)
    {
        var form = await context.Request.ReadFormAsync();

        var avgMark = form["avgmark"].ToString();
        var city = form["city"].ToString();
        var wifi = form["wifi"].ToString();

        context.Response.Cookies.Append("city", city);
        context.Response.Cookies.Append("avgMark", avgMark);
        context.Response.Cookies.Append("wifi", wifi);

        context.Session.SetString("city", city);
        context.Session.SetString("avgMark", avgMark);
        context.Session.SetString("wifi", wifi);

        var res = FlatHelper.FindFlats(city, avgMark, wifi);

        await context.Response.WriteAsync(res);
    }
    else
    {
        await context.Response.WriteAsync("Invalid form submission.");
    }
});

app.MapGet("/", () => "Hello World!");

app.Run();
