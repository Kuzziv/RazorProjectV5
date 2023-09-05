using NLog;
using NLog.Extensions.Logging;
using RazorProjectV5.Models;
using RazorProjectV5.Services.Interfaces;
using RazorProjectV5.Services.ServiceClasses;


var logger = LogManager.Setup().LoadConfigurationFromFile().GetCurrentClassLogger();
logger.Debug("Hello World");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddRazorPages();
    builder.Services.AddSingleton<IItemService, ItemService>();
    builder.Services.AddTransient<IJsonFileService<Item>, JsonFileService<Item>>();
    builder.Services.AddLogging(loggingBuilder =>
    {
        loggingBuilder.ClearProviders();
        loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace); // Use Microsoft.Extensions.Logging.LogLevel
        loggingBuilder.AddNLog(); // Configure NLog
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapRazorPages();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Stopped program because of exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}
