using FireBird;
using FireBird.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddDevExpressBlazor(configure => { 
    configure.SizeMode = DevExpress.Blazor.SizeMode.Small;
    //configure.BootstrapVersion = DevExpress.Blazor.BootstrapVersion.v5;

});

//builder.Services.Configure<DevExpress.Blazor.Configuration.GlobalOptions>(options => {
//    options.BootstrapVersion = DevExpress.Blazor.BootstrapVersion.v5;
//});

builder.Services.AddSingleton<DataAccess>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
