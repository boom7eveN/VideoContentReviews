using Microsoft.OpenApi.Models;

namespace VideoContentReviews.Service.IoC;

public static class SwaggerConfigurator
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "VideoContentReviews API",
                Version = "v1",
                Description = "API для управления видео контентом и отзывами"
            });
            
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Введите JWT токен(без кавычек):"
            });
            
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

           
            
        });
    }
    
    public static void ConfigureApplication(IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "VideoContentReviews API v1");
            options.RoutePrefix = string.Empty;
            options.DisplayRequestDuration();
            options.EnableDeepLinking();
            options.DefaultModelsExpandDepth(2);
            options.DefaultModelExpandDepth(2);
        });
    }
}