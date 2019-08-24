namespace t3hmun.WebLog.Web
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using t3hmun.WebLog.Web.Models;
    using t3hmun.WebLog.Web.Pages;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages().AddRazorPagesOptions(options =>
            {
                options.Conventions.AddPageRoute($"/{BaseModel.ModelName(typeof(HomeModel))}", "");
                options.Conventions.AddPageRoute(
                    $"/{BaseModel.ModelName(typeof(PostModel))}",
                    "~/post/{" + PostModel.RawTitleRouteString + "}");
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler($"/{BaseModel.ModelName(typeof(ErrorModel))}");

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapRazorPages(); });
        }
    }
}