using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HotChocolateNotCallingConfigure;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddSingleton<CustomTypeModule>()
            .AddGraphQLServer()
            .AddQueryType<Queries>()
            .AddMutationType<Mutations>()
            .AddTypeModule(sp => sp.GetRequiredService<CustomTypeModule>())
            .AddType<UserInfo>()
            .AddSorting();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app
            .UseRouting()
            .UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });

        applicationLifetime.ApplicationStopping.Register(OnShutdown);
    }

    private void OnShutdown()
    {
        //this code is called when the application stops
    }
}


