using Serilog;

namespace VideoContentReviews.Service.IoC;

public static class SerilogConfigurator
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, configuration) =>
        {
            configuration
                .Enrich.FromLogContext()
                .Enrich.WithCorrelationId()
                .ReadFrom.Configuration(context.Configuration);
        });

        builder.Services.AddHttpContextAccessor();
    }

    public static void ConfigureApplication(IApplicationBuilder app)
    {
        app.UseSerilogRequestLogging();

    }
}