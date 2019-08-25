namespace t3hmun.WebLog.Web
{
    using JetBrains.Annotations;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.FileProviders;
    using Microsoft.Extensions.Hosting;
    using t3hmun.WebLog.Web.Helpers;
    using t3hmun.WebLog.Web.Models;
    using t3hmun.WebLog.Web.Pages;

    public class Startup
    {
        private readonly IHostEnvironment _env;

        public Startup(IHostEnvironment env)
        {
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var mvcServiceBuilder = services.AddRazorPages().AddRazorPagesOptions(options =>
            {
                options.Conventions.AddPageRoute($"/{BaseModel.ModelName(typeof(HomeModel))}", "");
                options.Conventions.AddPageRoute(
                    $"/{BaseModel.ModelName(typeof(PostModel))}",
                    "~/post/{" + PostModel.RawTitleRouteString + "}");
            });

            if (_env.IsDevelopment()) mvcServiceBuilder.AddRazorRuntimeCompilation();

            services.AddSingleton<IPostParser, PostParser>();
            services.AddSingleton<IFileProvider>(provider =>
                new PhysicalFileProvider(provider.GetService<IWebHostEnvironment>().ContentRootPath));
        }

        [UsedImplicitly]
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